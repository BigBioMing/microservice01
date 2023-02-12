using DDD.Shared.Domain.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DDD.Domain.UserAggregate
{
    public class User : Entity<long>, IAggregateRoot
    {
    }
}
