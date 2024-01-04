using Stripe.Checkout;

namespace Eshop.Server.Services.Payment;

public interface IPaymentService
{
    Task<Session> CreateCheckoutSession();
}