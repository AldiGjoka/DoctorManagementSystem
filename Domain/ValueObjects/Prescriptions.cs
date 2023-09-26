using Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.ValueObjects
{
    public class Prescriptions : ValueObject
    {
        // Required by EF Core
        private Prescriptions() { }

        public Prescriptions(string medicationName, string dosage, string instructions, DateTime date)
        {
            MedicationName = medicationName;
            Dosage = dosage;
            Instructions = instructions;
            Date = date;
        }

        public string MedicationName { get; private set; }
        public string Dosage { get; private set; }
        public string Instructions { get; private set; }
        public DateTime Date { get; private set; }
        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return MedicationName;
            yield return Dosage;
            yield return Instructions;
            yield return Date;
        }
    }
}
