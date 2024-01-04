using Eshop.Server.Services.Authentication;
using Eshop.Server.Services.CartService;
using Eshop.Server.Services.Ordering;
using Stripe;
using Stripe.Checkout;

namespace Eshop.Server.Services.Payment;

public class PaymentService : IPaymentService
{
    private readonly IOrderingService _orderingService;
    private readonly IAuthenticationService _authenticationService;
    private readonly ICartService _cartService;
    private readonly IConfiguration _configuration;

    public PaymentService(IOrderingService orderingService, IAuthenticationService authenticationService, ICartService cartService, IConfiguration configuration)
    {
        _orderingService = orderingService;
        _authenticationService = authenticationService;
        _cartService = cartService;
        _configuration = configuration;
    }
    
    public async Task<Session> CreateCheckoutSession(){
        var key = _configuration.GetSection("AppSettings:Stripe").Value;
        StripeConfiguration.ApiKey = key;

        Console.WriteLine(key);

        var products = (await _cartService.GetDbCartProducts()).Data;
        var lineItems = new List<SessionLineItemOptions>();
        
        products?.ForEach(product => lineItems.Add(new SessionLineItemOptions
        {
            PriceData = new SessionLineItemPriceDataOptions
            {
                UnitAmountDecimal = product.Price * 100,
                Currency = "czk",
                ProductData = new SessionLineItemPriceDataProductDataOptions
                {
                    Name = product.ProductTitle,
                    Images = new List<string>{product.Image}
                }
            },
            Quantity = product.Quantity
        }));

        var options = new SessionCreateOptions()
        {
            CustomerEmail = _authenticationService.GetUserEmail(),
            ShippingAddressCollection = new SessionShippingAddressCollectionOptions()
            {
                AllowedCountries = new List<string>{ "US", "CZ"}
            },
            PaymentMethodTypes = new List<string>
            {
                "card"
            },
            LineItems = lineItems,
            Mode = "payment",
            SuccessUrl = "http://localhost:5080/order-success",
            CancelUrl = "http://localhost:5080/cart"
        };

        var service = new SessionService();
        var session = await service.CreateAsync(options);

        return session;

    }

    public async Task<ServiceResponse<bool>> FulfillOrder(HttpRequest httpRequest)
    {
        var secret = _configuration.GetSection("AppSettings:Secret").Value;
        var jsonObject = await new StreamReader(httpRequest.Body).ReadToEndAsync();
        try
        {
            var stripeEvent = EventUtility.ConstructEvent(
                jsonObject, httpRequest.Headers["Stripe-Signature"], secret
            );

            if (stripeEvent.Type != Events.CheckoutSessionCompleted) return new ServiceResponse<bool> { Data = true };
            var session = stripeEvent.Data.Object as Session;
            var user = await _authenticationService.GetUserByEmail(session.CustomerEmail);
            await _orderingService.PlaceOrder(user.Id);

            return new ServiceResponse<bool> { Data = true };

        }
        catch (StripeException e)
        {
            return new ServiceResponse<bool>
            {
                Data = false,
                Success = false,
                Message = e.Message
            };
        }
    }
}