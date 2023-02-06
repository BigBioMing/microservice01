using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DDD.Shared.Domain.Abstractions
{
    public interface IEntity
    {
        object[] GetKeys();
    }

    public interface IEntity<TKey> : IEntity
    {
        TKey Id { get; }
    }
}
