using Domain.Entity.PatientAggregate;
using Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.PatientService
{
    public class RecetaDTO
    {
        public int RecetaId { get; set; }
        public int PatientId { get; private set; }
        public List<Prescriptions> Prescriptions { get; set; }
        public DateTime Date { get; set; }

        public List<RecetaDTO> MapToRecetaDto(List<Receta> recetat)
        {
            var recetaDto = new List<RecetaDTO>();

            foreach(var recet in recetat)
            {
                var rec = new RecetaDTO()
                {
                    RecetaId = recet.Id,
                    PatientId = recet.PatientId,
                    Prescriptions = recet.Prescriptions,
                    Date = recet.Date
                };

                recetaDto.Add(rec);
            };

            return recetaDto;
        }
    }
}
