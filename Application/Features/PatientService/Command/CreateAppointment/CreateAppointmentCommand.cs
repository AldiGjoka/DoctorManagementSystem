using Application.Common.Exceptions;
using Application.Common.Interfaces.Persistance;
using Domain.Entity.PatientAggregate;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.PatientService.Command.CreateAppointment
{
    public record CreateAppointmentCommand : IRequest
    {
        public int PatientId { get; set; }
        public DateTime AppointmentDate { get; set; }
        public string StartTime { get; set; }
        public string EndTime { get; set; }
    }

    public class CreateAppointmentHandler : IRequestHandler<CreateAppointmentCommand>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IRepository<Patient> _patientRepository;

        public CreateAppointmentHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _patientRepository = _unitOfWork.Repository<Patient>();
        }
        public async Task Handle(CreateAppointmentCommand request, CancellationToken cancellationToken)
        {
            var patient = await _patientRepository.GetByIdAsync(request.PatientId);

            if(patient == null)
                throw new NotFoundException(nameof(Patient), request.PatientId);

            patient.CreateAppointment(request.AppointmentDate, request.StartTime, request.EndTime);

            await _patientRepository.UpdateAsync(patient);

            await _unitOfWork.SaveAsync();
        }
    }
}
