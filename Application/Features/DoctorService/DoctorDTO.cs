using Domain.Entity;
using Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.DoctorService
{
    public class DoctorDTO
    {
        public int doctorId { get; set; }
        public string PersonalNumber { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Specialization { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public Address Address { get; set; }


        public IReadOnlyList<DoctorDTO> MapToDoctorDTOList(IReadOnlyList<Doctor> doctors)
        {
            var doctorsDto = new List<DoctorDTO>();

            foreach (var doc in doctors)
            {
                var docDto = new DoctorDTO()
                {
                    doctorId = doc.Id,
                    PersonalNumber = doc.PersonalNumber,
                    FirstName = doc.FirstName,
                    LastName = doc.LastName,
                    Specialization = doc.Specialization,
                    Email = doc.Email,
                    PhoneNumber = doc.PhoneNumber,
                    Address = doc.Address,
                };

                doctorsDto.Add(docDto);
            }

            return doctorsDto;
        }

        public DoctorDTO MapToDoctorDTO(Doctor doctor)
        {
            DoctorDTO dto = new DoctorDTO()
            {
                doctorId = doctor.Id,
                PersonalNumber = doctor.PersonalNumber,
                FirstName = doctor.FirstName,
                LastName = doctor.LastName,
                Specialization = doctor.Specialization,
                Email = doctor.Email,
                PhoneNumber = doctor.PhoneNumber,
                Address = doctor.Address
            };

            return dto;
        }
    }
}
