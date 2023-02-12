using DDD.Domain.OrderAggregate;
using DDD.Shared.Domain.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DDD.Domain.Events
{
    public class OrderAddressChangedDomainEvent : IDomainEvent
    {
        public Order Order { get; private set; }
        public OrderAddressChangedDomainEvent(Order order)
        {
            Order = order;
        }
    }
}
