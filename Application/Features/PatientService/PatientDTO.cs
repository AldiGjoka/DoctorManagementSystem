using Domain.Entity.PatientAggregate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.PatientService
{
    public class PatientDTO
    {
        public int PatientId { get; set; }
        public string PersonalNumber { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Birthdate { get; set; }
        public int Gender { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }

        public List<PatientDTO> MapToPatientDtoList(IReadOnlyList<Patient> patients)
        {
            var patientsDto = new List<PatientDTO>();

            foreach (var pat in patients)
            {
                var patDto = new PatientDTO()
                {
                    PatientId = pat.Id,
                    PersonalNumber = pat.PersonalNumber,
                    FirstName = pat.FirstName,
                    LastName = pat.LastName,
                    Birthdate = pat.Birthdate,
                    Gender = (int)pat.Gender,
                    Email = pat.Email,
                    PhoneNumber = pat.PhoneNumber
                };

                patientsDto.Add(patDto);
            }

            return patientsDto;
        }

        public PatientDTO MapToPatientDto(Patient patient)
        {
            var patientDto = new PatientDTO()
            {
                PatientId = patient.Id,
                PersonalNumber = patient.PersonalNumber,
                FirstName = patient.FirstName,
                LastName = patient.LastName,
                Birthdate = patient.Birthdate,
                Gender = (int)patient.Gender,
                Email = patient.Email,
                PhoneNumber = patient.PhoneNumber
            };

            return patientDto;
        }
    }
}
