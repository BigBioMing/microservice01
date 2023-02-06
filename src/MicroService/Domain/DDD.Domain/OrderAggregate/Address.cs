using DDD.Shared.Domain.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DDD.Domain.OrderAggregate
{
    public class Address : ValueObject
    {
        public string Street { get; private set; }
        public string City { get; private set; }
        public string ZipCode { get; private set; }

        protected Address() { }

        public Address(string street, string city, string zipCode)
        {
            this.Street = street;
            this.City = city;
            this.ZipCode = zipCode;
        }

        protected override IEnumerable<object> GetAtomicValues()
        {
            yield return Street;
            yield return City;
            yield return ZipCode;
        }
    }
}
