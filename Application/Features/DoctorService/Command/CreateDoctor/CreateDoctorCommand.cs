using Application.Common.Exceptions;
using Application.Common.Interfaces.Persistance;
using Domain.Entity;
using Domain.ValueObjects;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.DoctorService.Command.CreateDoctor
{
    public record CreateDoctorCommand : IRequest<DoctorDTO>
    {
        public string PersonalNumber { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Specialization { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string State { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public string StreetName { get; set; }
        public string PostalCode { get; set; }
    }

    public class CreateDoctorCommandHandler : IRequestHandler<CreateDoctorCommand, DoctorDTO>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IRepository<Doctor> _doctorRepository;

        public CreateDoctorCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _doctorRepository = unitOfWork.Repository<Doctor>();
        }
        public async Task<DoctorDTO> Handle(CreateDoctorCommand request, CancellationToken cancellationToken)
        {
            var validator = new CreateDoctorValidator();
            var validationResult = validator.Validate(request);

            if (validationResult.Errors.Count > 0)
                throw new ValidationException(validationResult);

            var address = new Address(request.State, request.City, request.Country, request.StreetName, request.PostalCode);

            var doctor = new Doctor(request.PersonalNumber, request.FirstName, request.LastName, request.Specialization,
                request.Email, request.PhoneNumber, address);

            var result = await _doctorRepository.AddAsync(doctor);

            await _unitOfWork.SaveAsync();

            var dto = new DoctorDTO().MapToDoctorDTO(result);

            return dto;
        }
    }
}
