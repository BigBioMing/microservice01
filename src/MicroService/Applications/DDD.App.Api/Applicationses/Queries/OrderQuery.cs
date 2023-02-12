using MediatR;

namespace DDD.App.Api.Applicationses.Queries
{
    public class OrderQuery : IRequest<List<string>>
    {
        public OrderQuery(string userName)
        {
            UserName = userName;   
        }
        public string UserName { get; set; }
    }
}
