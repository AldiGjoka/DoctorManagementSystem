using Application.Common.Exceptions;
using Application.Common.Interfaces.Persistance;
using Domain.Entity.PatientAggregate;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.PatientService.Command.DeletePatient
{
    public record DeletePatientCommand : IRequest
    {
        public int DoctorId { get; set; }
        public int PatientId { get; set; }
    }

    public class DeletePatientCommandHandler : IRequestHandler<DeletePatientCommand>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IRepository<Patient> _patientRepository;

        public DeletePatientCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _patientRepository = _unitOfWork.Repository<Patient>();
        }
        public async Task Handle(DeletePatientCommand request, CancellationToken cancellationToken)
        {
            var patient = await _patientRepository.GetByIdAsync(request.PatientId);

            if(patient == null || patient.DoctorId != request.DoctorId)
                throw new NotFoundException(nameof(Patient), request.PatientId);

            await _patientRepository.DeleteAsync(patient);

            await _unitOfWork.SaveAsync();
        }
    }
}
