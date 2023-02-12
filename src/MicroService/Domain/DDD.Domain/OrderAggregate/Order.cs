using DDD.Domain.Events;
using DDD.Shared.Domain.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace DDD.Domain.OrderAggregate
{
    public class Order : Entity<long>, IAggregateRoot
    {
        public string UserId { get; private set; }
        public string UserName { get; private set; }
        public int TotalCount { get; private set; }
        public Address Address { get; private set; }

        protected Order() { }

        public Order(string userId, string userName, int totalCount, Address address)
        {
            this.UserId = userId;
            this.UserName = userName;
            this.TotalCount = totalCount;
            this.Address = address;

            this.AddDomainEvents(new OrderCreatedDomainEvent(this));
        }

        public void ChangeAddress(Address address)
        {
            this.Address = address;
            this.AddDomainEvents(new OrderAddressChangedDomainEvent(this));
        }
    }
}
