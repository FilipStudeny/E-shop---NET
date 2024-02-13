using Ecommerce.Shared;
using Stripe.Checkout;

namespace Ecommerce.Server.Services.Payment
{
    public interface IPaymentService
    {
        Task<Session> CreateCheckoutSession();
        Task<ServiceResponse<bool>> FulfillOrder(HttpRequest httpRequest);
    }
}
