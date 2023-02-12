using DDD.Domain.UserAggregate;
using DDD.Shared.Infrastructure.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DDD.Infrastructure.Repositories
{
    public class UserRepository : Repository<User, long, DomainContext>, IUserRepository
    {
        public UserRepository(DomainContext dbContext) : base(dbContext)
        {
        }
    }
}
