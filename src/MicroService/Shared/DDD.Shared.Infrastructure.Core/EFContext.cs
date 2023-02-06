using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DDD.Shared.Infrastructure.Core
{
    public class EFContext : DbContext, IUnitOfWork, ITransaction
    {
        public EFContext(DbContextOptions options)
        {

        }

        #region IUnitOfWork
        public async Task<bool> SaveEntitiesAsync(CancellationToken cancellationToken)
        {
            var result = await base.SaveChangesAsync(cancellationToken);
            return true;
        }
        #endregion

        #region ITransaction
        private IDbContextTransaction _currentTransaction;

        public IDbContextTransaction GetCurrentTransaction() => this._currentTransaction;
        public bool HasActiveTransaction => _currentTransaction != null;
        public async Task<IDbContextTransaction> BeginTransactionAsync()
        {
            if (_currentTransaction == null) return null;
            _currentTransaction = await Database.BeginTransactionAsync();
            return _currentTransaction;
        }
        public async Task CommitTransactionAsync(IDbContextTransaction transaction)
        {
            if (transaction == null) throw new ArgumentNullException(nameof(transaction));
            if (_currentTransaction != transaction) throw new InvalidOperationException($"Transaction {transaction.TransactionId}不正确");

            try
            {
                await SaveChangesAsync();
                transaction.Commit();
            }
            catch (Exception ex)
            {
                RollbackTransaction();
                throw ex;
            }
            finally
            {
                if (_currentTransaction != null)
                {
                    _currentTransaction.Dispose();
                    _currentTransaction = null;
                }
            }
        }

        public void RollbackTransaction()
        {
            try
            {
                _currentTransaction?.Rollback();
            }
            finally
            {
                if (_currentTransaction != null)
                {
                    _currentTransaction.Dispose();
                    _currentTransaction = null;
                }
            }
        }
        #endregion
    }
}
