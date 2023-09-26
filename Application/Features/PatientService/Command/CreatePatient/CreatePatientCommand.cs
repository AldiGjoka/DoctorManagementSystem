using Application.Common.Exceptions;
using Application.Common.Interfaces.Persistance;
using Domain.Entity.PatientAggregate;
using Domain.Enums;
using Domain.ValueObjects;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.PatientService.Command.CreatePatient
{
    public record CreatePatientCommand : IRequest<PatientDTO>
    {
        public int DoctorId { get; set; }
        public string PersonalNumber { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Birthdate { get; set; }
        public Gender Gender { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string State { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public string StreetName { get; set; }
        public string PostalCode { get; set; }
    }

    public class CreatePatientCommandHandler : IRequestHandler<CreatePatientCommand, PatientDTO>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IRepository<Patient> _patientRepository;

        public CreatePatientCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _patientRepository = _unitOfWork.Repository<Patient>();
        }
        public async Task<PatientDTO> Handle(CreatePatientCommand request, CancellationToken cancellationToken)
        {
            var validator = new CreatePatientValidator();
            var validationResult = validator.Validate(request);

            if (validationResult.Errors.Count > 0)
                throw new ValidationException(validationResult);

            var address = new Address(request.State, request.City, request.Country, request.StreetName, request.PostalCode);

            var patient = new Patient(request.DoctorId, request.PersonalNumber, request.FirstName, request.LastName,
                request.Birthdate, request.Gender, request.Email, request.PhoneNumber, address);

            var result = await _patientRepository.AddAsync(patient);

            await _unitOfWork.SaveAsync();

            var dto = new PatientDTO().MapToPatientDto(result);

            return dto;
        }
    }
}
