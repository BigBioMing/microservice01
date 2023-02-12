using DDD.Domain.OrderAggregate;
using DDD.Domain.UserAggregate;
using DDD.Shared.Infrastructure.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DDD.Infrastructure.Repositories
{
    public interface IUserRepository : IRepository<User, long>
    {
    }
}
