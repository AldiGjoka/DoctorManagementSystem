using Application.Common.Interfaces.Persistance;
using AutoMapper;
using Domain.Entity;
using Domain.Entity.PatientAggregate;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Appointmens.Query
{
    public record GetAllTodayAppointmentsQuery : IRequest<List<AppointemntListDTO>>
    {
        public int DoctorId { get; set; }
    }


    public class GetAllTodayAppointmentsQueryHandler : IRequestHandler<GetAllTodayAppointmentsQuery, List<AppointemntListDTO>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IRepository<AppointmentAgg> _appointmentRepository;
        private readonly IMapper _mapper;

        public GetAllTodayAppointmentsQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _appointmentRepository = unitOfWork.Repository<AppointmentAgg>();
            _mapper = mapper;
        }
        public async Task<List<AppointemntListDTO>> Handle(GetAllTodayAppointmentsQuery request, CancellationToken cancellationToken)
        {
            var todayDate = DateTime.Today;
            var response = await _appointmentRepository.ListAllAsync(
                filter: x => x.AppointmentDate.Date == todayDate.Date,
                orderBy: x => x.OrderBy(a => a.StartTime),
                includeProperties: "Patient");

            var todayAppointments = response.Where(x => x.Patient.DoctorId == request.DoctorId);

            var appointmentDto = _mapper.Map<List<AppointemntListDTO>>(todayAppointments);

            return appointmentDto;
        }
    }
}
