using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DDD.Shared.Infrastructure.Core
{
    public interface ITransaction
    {
        IDbContextTransaction GetCurrentTransaction();
        /// <summary>
        /// 判断当前事务是否开启
        /// </summary>
        bool HasActiveTransaction { get; }
        Task<IDbContextTransaction> BeginTransactionAsync();
        Task CommitTransactionAsync(IDbContextTransaction transaction);
        void RollbackTransaction();
    }
}
