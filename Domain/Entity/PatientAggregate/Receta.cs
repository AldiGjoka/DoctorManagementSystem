using Domain.Common;
using Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entity.PatientAggregate
{
    public class Receta : BaseEntity
    {
        // Required by EF Core
        private Receta() { }
        public Receta(int patientId, List<Prescriptions> prescriptions, DateTime date)
        {
            PatientId = patientId;
            Prescriptions = prescriptions;
            Date = date;
        }

        public int PatientId { get; private set; }
        public Patient Patient { get; private set; }
        public List<Prescriptions> Prescriptions { get; private set; }
        public DateTime Date { get; private set; }
    }
}
