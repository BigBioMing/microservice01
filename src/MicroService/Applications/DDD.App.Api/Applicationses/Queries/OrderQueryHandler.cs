using MediatR;

namespace DDD.App.Api.Applicationses.Queries
{
    public class OrderQueryHandler : IRequestHandler<OrderQuery, List<string>>
    {
        public Task<List<string>> Handle(OrderQuery request, CancellationToken cancellationToken)
        {
            return Task.FromResult(new List<string>());
        }
    }
}
