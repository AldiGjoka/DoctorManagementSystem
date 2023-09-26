using Domain.Common;
using Domain.Enums;
using Domain.Interfaces;
using Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entity.PatientAggregate
{
    public class Patient : BaseEntity, IAggregateRoot
    {
        // Required by EF Core
        private Patient() { }
        public Patient(int doctorId, string personalNumber, string firstName, string lastName, string birthdate,
            Gender gender, string email, string phoneNumber, Address address)
        {
            DoctorId = doctorId;
            PersonalNumber = personalNumber;
            FirstName = firstName;
            LastName = lastName;
            Birthdate = birthdate;
            Gender = gender;
            Email = email;
            PhoneNumber = phoneNumber;
            Address = address;
        }
        public int DoctorId { get; private set; }
        public Doctor Doctor { get; private set; }
        public string PersonalNumber { get; private set; }
        public string FirstName { get; private set; }
        public string LastName { get; private set; }
        public string Birthdate { get; private set; }
        public Gender Gender { get; private set; }
        public string Email { get; private set; }
        public string PhoneNumber { get; private set; }
        public Address Address { get; private set; }
        private List<AppointmentAgg> _appointments = new List<AppointmentAgg>();
        public List<AppointmentAgg> Appointments => _appointments;
        private List<Receta> _receta = new List<Receta>();
        public List<Receta> Receta => _receta;


        public void UpdatePatient(string email, string phoneNumber)
        {
            Email = email;
            PhoneNumber = PhoneNumber;
        }

        public void CreateAppointment(DateTime date, string startTime, string endTime)
        {
            var appointment = new AppointmentAgg(this.Id, date, startTime, endTime);

            _appointments.Add(appointment);
        }

        public void CreateRecete(List<Prescriptions> prescriptions, DateTime date)
        {
            var receta = new Receta(this.Id, prescriptions, date);

            _receta.Add(receta);
        }
    }
}
