namespace DDD.App.Api.Applicationses.IntegrationEvents
{
    public class OrderPaymentSucceededIntegrationEvent
    {
        public OrderPaymentSucceededIntegrationEvent(long orderId) => OrderId = orderId;

        public long OrderId { get; }
    }
}
