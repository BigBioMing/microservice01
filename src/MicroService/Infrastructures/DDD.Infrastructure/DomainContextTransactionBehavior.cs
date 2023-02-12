using DDD.Shared.Infrastructure.Core.Behaviors;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DDD.Infrastructure
{
    internal class DomainContextTransactionBehavior<TRequest, TResponse> : TransactionBehavior<DomainContext, TRequest, TResponse> where TRequest : IRequest<TResponse>
    {
        public DomainContextTransactionBehavior(ILogger<DomainContextTransactionBehavior<TRequest, TResponse>> logger, DomainContext dbContext) : base(logger, dbContext)
        {
        }
    }
}
