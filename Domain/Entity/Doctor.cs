using Domain.Common;
using Domain.Entity.PatientAggregate;
using Domain.Interfaces;
using Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entity
{
    public class Doctor : BaseEntity, IAggregateRoot
    {
        // Required by EF Core
        private Doctor() { }
        public Doctor(string personalNumber, string firstName, string lastName, string specialization, string email, 
            string phoneNumber, Address address)
        {
            PersonalNumber = personalNumber;
            FirstName = firstName;
            LastName = lastName;
            Specialization = specialization;
            Email = email;
            PhoneNumber = phoneNumber;
            Address = address;
        }

        public string PersonalNumber { get; private set; }
        public string FirstName { get; private set; }
        public string LastName { get; private set; }
        public string Specialization { get; private set; }
        public string Email { get; private set; }
        public string PhoneNumber { get; private set; }
        public Address Address { get; private set; }
        public List<Patient> Patients { get; private set; }


        public void UpdateDoctor(string email, string phoneNumber, string state, string city, string country,
            string streetName, string postalCode)
        {
            var address = new Address(state, city, country, streetName, postalCode);

            Email = email;
            PhoneNumber = phoneNumber;
            Address = address;
        }
    }
}
