namespace DDD.App.Api.Applicationses.IntegrationEvents
{
    public class OrderCreatedIntegrationEvent
    {
        public OrderCreatedIntegrationEvent(long orderId) => OrderId = orderId;

        public long OrderId { get; }
    }
}
