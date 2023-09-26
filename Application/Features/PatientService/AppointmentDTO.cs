using Domain.Entity;
using Domain.Entity.PatientAggregate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.PatientService
{
    public class AppointmentDTO
    {
        public int AppointmentId { get; set; }
        public int PatientId { get; set; }
        public DateTime AppointmentDate { get; set; }

        public string StartTime { get; set; }
        public string EndTime { get; set; }

        public AppointmentDTO MapToAppointmentDto(AppointmentAgg appointment)
        {
            this.AppointmentId = appointment.Id;
            this.PatientId = appointment.PatientId;
            this.AppointmentDate = appointment.AppointmentDate;
            this.StartTime = appointment.StartTime;
            this.EndTime = appointment.EndTime;

            return this;
        }

        public List<AppointmentDTO> MapToAppointmentDtoList(List<AppointmentAgg> appointments)
        {
            var appointmentDTO = new List<AppointmentDTO>();

            foreach(var appointment in appointments)
            {
                var app = new AppointmentDTO()
                {
                    AppointmentId = appointment.Id,
                    PatientId = appointment.PatientId,
                    AppointmentDate = appointment.AppointmentDate,
                    StartTime = appointment.StartTime,
                    EndTime = appointment.EndTime,
                };

                appointmentDTO.Add(app);
            }
            
            return appointmentDTO;
        }
    }
}
