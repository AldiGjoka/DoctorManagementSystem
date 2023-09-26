using Domain.Common;
using Domain.Entity.PatientAggregate;
using Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entity
{
    public class AppointmentAgg : BaseEntity, IAggregateRoot
    {
        // Required by EF Core
        private AppointmentAgg() { }
        public AppointmentAgg(int patientId, DateTime appointmentDate, string startTime, string endTime)
        {
            PatientId = patientId;
            AppointmentDate = appointmentDate;
            StartTime = startTime;
            EndTime = endTime;
        }

        public int PatientId { get; private set; }
        public Patient Patient { get; private set; }
        public DateTime AppointmentDate { get; private set; }

        public string StartTime { get; private set; }
        public string EndTime { get; private set; }

        public void UpdateAppointmentDateTime(DateTime appointmentDate, string startTime, string endTime)
        {
            this.AppointmentDate = appointmentDate;
            this.StartTime = startTime;
            this.EndTime = endTime;
        }
    }
}
