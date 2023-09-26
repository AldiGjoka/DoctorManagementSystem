using Application.Common.Exceptions;
using Application.Common.Interfaces.Persistance;
using Domain.Entity;
using Domain.Entity.PatientAggregate;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.PatientService.Command.DeleteAppointment
{
    public record DeleteAppointmentCommand : IRequest
    {
        public int PatientId { get; set; }
        public int AppointmentId { get; set; }
    }

    public class DeleteAppointmentCommandHandler : IRequestHandler<DeleteAppointmentCommand>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IRepository<Patient> _patientRepository;

        public DeleteAppointmentCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _patientRepository = _unitOfWork.Repository<Patient>();
        }
        public async Task Handle(DeleteAppointmentCommand request, CancellationToken cancellationToken)
        {
            var patient = await _patientRepository.ListAllAsync(filter: p => p.Id == request.PatientId,
                includeProperties: "Appointments");

            var patientFromDb = patient.FirstOrDefault(p => p.Id == request.PatientId);

            if (patientFromDb == null)
                throw new NotFoundException(nameof(Patient), request.PatientId);

            var appointment = patientFromDb.Appointments.FirstOrDefault(x => x.Id == request.AppointmentId);

            if (appointment == null)
                throw new NotFoundException(nameof(AppointmentAgg), request.AppointmentId);

            patientFromDb.Appointments.Remove(appointment);

            await _unitOfWork.SaveAsync();
        }
    }
}
