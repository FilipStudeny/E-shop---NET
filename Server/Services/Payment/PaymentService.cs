using Ecommerce.Shared;
using Stripe.Checkout;
using Stripe;
using Ecommerce.Server.Services.Ordering;
using Ecommerce.Server.Services.UserService;

namespace Ecommerce.Server.Services.Payment
{
    public class PaymentService : IPaymentService
    {
        private readonly IOrderService orderService;
        private readonly IUserService authenticationService;
        private readonly ICartService cartService;
        private readonly IConfiguration configuration;

        public PaymentService(IOrderService orderService, IUserService authenticationService, ICartService cartService, IConfiguration configuration)
        {
            this.orderService = orderService;
            this.authenticationService = authenticationService;
            this.cartService = cartService;
            this.configuration = configuration;
        }
        public async Task<Session> CreateCheckoutSession()
        {
            var key = configuration.GetSection("AppSettings:StripeKey").Value;
            StripeConfiguration.ApiKey = key;

            var products = (await cartService.GetDbCartProducts()).Data;
            var lineItems = new List<SessionLineItemOptions>();

            products?.ForEach(product => lineItems.Add(new SessionLineItemOptions
            {
                PriceData = new SessionLineItemPriceDataOptions
                {
                    UnitAmountDecimal = product.Price * 100,
                    Currency = "czk",
                    ProductData = new SessionLineItemPriceDataProductDataOptions
                    {
                        Name = product.BookTitle,
                        Images = new List<string> { "https://dummyimage.com/400x400/000/fff" }
                    }
                },
                Quantity = product.Quantity
            }));

            var options = new SessionCreateOptions()
            {
                CustomerEmail = authenticationService.GetUserEmail(),
                ShippingAddressCollection = new SessionShippingAddressCollectionOptions()
                {
                    AllowedCountries = new List<string> { "US", "CZ" },
                },
                PaymentMethodTypes = new List<string>
            {
                "card"
            },
                LineItems = lineItems,
                Mode = "payment",
                SuccessUrl = "https://localhost:7152/order-success",
                CancelUrl = "https://localhost:7152/cart"
            };

            var service = new SessionService();
            var session = await service.CreateAsync(options);

            return session;

        }

        public async Task<ServiceResponse<bool>> FulfillOrder(HttpRequest httpRequest)
        {
            var secret = configuration.GetSection("AppSettings:StripeSecret").Value;
            var jsonObject = await new StreamReader(httpRequest.Body).ReadToEndAsync();
            try
            {
                var stripeEvent = EventUtility.ConstructEvent(
                    jsonObject, httpRequest.Headers["Stripe-Signature"], secret
                );

                if (stripeEvent.Type != Events.CheckoutSessionCompleted) return new ServiceResponse<bool> { Data = true };
                var session = stripeEvent.Data.Object as Session;
                var user = await authenticationService.GetUserByEmail(session.CustomerEmail);
                await orderService.PlaceOrder(user.Id);

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
}
