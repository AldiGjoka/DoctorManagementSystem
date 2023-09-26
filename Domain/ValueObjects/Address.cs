using Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.ValueObjects
{
    public class Address : ValueObject
    {
        // Required by EF Core
        private Address() { }
        public Address(string state, string city, string country, string streetName, string postalCode)
        {
            State = state;
            City = city;
            Country = country;
            StreetName = streetName;
            PostalCode = postalCode;
        }

        public string State { get; private set; }
        public string City { get; private set; }
        public string Country { get; private set; }
        public string StreetName { get; private set; }
        public string PostalCode { get; private set; }
        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return State;
            yield return City;
            yield return Country;
            yield return StreetName;
            yield return PostalCode;
        }
    }
}
