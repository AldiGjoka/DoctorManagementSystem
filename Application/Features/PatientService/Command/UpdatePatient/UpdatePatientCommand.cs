using Application.Common.Exceptions;
using Application.Common.Interfaces.Persistance;
using Domain.Entity.PatientAggregate;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.PatientService.Command.UpdatePatient
{
    public record UpdatePatientCommand : IRequest
    {
        public int PatientId { get; set; }
        public string PersonalNumber { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Birthdate { get; set; }
        public int Gender { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
    }

    public class UpdatePatientCommandHandler : IRequestHandler<UpdatePatientCommand>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IRepository<Patient> _patientRepository;

        public UpdatePatientCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _patientRepository = _unitOfWork.Repository<Patient>();
        }
        public async Task Handle(UpdatePatientCommand request, CancellationToken cancellationToken)
        {
            var validator = new UpdatePatientValidator();
            var validationResult = validator.Validate(request);

            if (validationResult.Errors.Count > 0)
                throw new ValidationException(validationResult);

            var patient = await _patientRepository.GetByIdAsync(request.PatientId);

            if(patient == null)
                throw new NotFoundException(nameof(Patient), request.PatientId);

            patient.UpdatePatient(request.Email, request.PhoneNumber);

            await _patientRepository.UpdateAsync(patient);

            await _unitOfWork.SaveAsync();
        }
    }
}
