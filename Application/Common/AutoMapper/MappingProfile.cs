using Application.Features.Appointmens.Query;
using Application.Features.PatientService;
using AutoMapper;
using Domain.Entity;
using Domain.Entity.PatientAggregate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Common.AutoMapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<AppointmentAgg, AppointemntListDTO>().ReverseMap();
        }
    }
}
