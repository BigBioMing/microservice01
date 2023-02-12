namespace DDD.App.Api.Applicationses.IntegrationEvents
{
    public interface ISubscriberService
    {
        void OrderPaymentSucceeded(OrderPaymentSucceededIntegrationEvent @event);
        void OrderCreated(OrderCreatedIntegrationEvent @event);
    }
}
