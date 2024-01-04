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
        var key = _configuration?.GetSection("StripeKey:key").Value;
        StripeConfiguration.ApiKey = key;
        _orderingService = orderingService;
        _authenticationService = authenticationService;
        _cartService = cartService;
        _configuration = configuration;
    }
    
    public async Task<Session> CreateCheckoutSession()
    {
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
            PaymentMethodTypes = new List<string>
            {
                "card"
            },
            LineItems = lineItems,
            Mode = "payment",
            SuccessUrl = "http//localhost:5080/order-success",
            CancelUrl = "http//localhost:5080/cart"
        };

        var service = new SessionService();
        var session = await service.CreateAsync(options);

        return session;

    }
}