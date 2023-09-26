using Application.Common.Exceptions;
using Application.Common.Interfaces.Persistance;
using Domain.Entity.PatientAggregate;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.PatientService.Query.GetAllAppointmentsByPatientId
{
    public record GetAllAppointmentsByPatientIdQuery : IRequest<List<AppointmentDTO>>
    {
        public int PatientId { get; set; }
    }

    public class GetAllAppointmentsByPatientIdQueryHandler : 
        IRequestHandler<GetAllAppointmentsByPatientIdQuery, List<AppointmentDTO>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IRepository<Patient> _patientRepository;

        public GetAllAppointmentsByPatientIdQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _patientRepository = _unitOfWork.Repository<Patient>();
        }
        public async Task<List<AppointmentDTO>> Handle(GetAllAppointmentsByPatientIdQuery request, CancellationToken cancellationToken)
        {
            var patient = await _patientRepository.ListAllAsync(filter: p => p.Id == request.PatientId,
                includeProperties: "Appointments");

            var patientFromDb = patient.FirstOrDefault(p => p.Id == request.PatientId);

            if (patientFromDb == null)
                throw new NotFoundException(nameof(Patient), request.PatientId);

            var appointmentDto = new AppointmentDTO().MapToAppointmentDtoList(patientFromDb.Appointments);

            return appointmentDto;
        }
    }
}
