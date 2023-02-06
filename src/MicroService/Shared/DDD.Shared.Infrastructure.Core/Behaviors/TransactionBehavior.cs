using DDD.Shared.Infrastructure.Core.Extensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DDD.Shared.Infrastructure.Core.Behaviors
{
    public class TransactionBehavior<TDbContext, TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse> where TDbContext : EFContext where TRequest : IRequest<TResponse>
    {
        ILogger _logger;
        TDbContext _dbContext;

        public TransactionBehavior(ILogger logger, TDbContext dbContext)
        {
            this._logger = logger;
            this._dbContext = dbContext;
        }

        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            var response = default(TResponse);
            var typeName = request.GetGenericTypeName();

            try
            {
                if (_dbContext.HasActiveTransaction)
                {
                    return await next();
                }

                var strategy = _dbContext.Database.CreateExecutionStrategy();


                await strategy.ExecuteAsync(async () =>
                {
                    Guid transactionId;
                    using (var transaction = await _dbContext.Database.BeginTransactionAsync())
                    using (_logger.BeginScope("TransactionContext:{TransactionId}", transaction.TransactionId))
                    {
                        _logger.LogInformation("------  开始事务 {TransactionId}  {CommandName} ({@Command})  ------", transaction.TransactionId, typeName, request);

                        response = await next();

                        _logger.LogInformation("------  提交事务 {TransactionId} {CommandName}  ------", transaction.TransactionId, typeName);

                        await _dbContext.CommitTransactionAsync(transaction);

                        transactionId = transaction.TransactionId;
                    }
                });

                return response;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "处理事务出错 {CommandName} {@Command}", typeName, request);
                throw ex;
            }
        }
    }
}
