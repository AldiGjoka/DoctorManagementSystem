using Application.Common.Exceptions;
using Application.Common.Interfaces.Persistance;
using Domain.Entity;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.DoctorService.Command.UpdateDoctor
{
    public record UpdateDoctorCommand : IRequest
    {
        public int Id { get; set; }
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

    public class UpdateDoctorCommandHandler : IRequestHandler<UpdateDoctorCommand>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IRepository<Doctor> _doctorRepository;

        public UpdateDoctorCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _doctorRepository = unitOfWork.Repository<Doctor>();
        }
        public async Task Handle(UpdateDoctorCommand request, CancellationToken cancellationToken)
        {
            var validator = new UpdateDoctorValidator();
            var validationResult = validator.Validate(request);

            if (validationResult.Errors.Count > 0)
                throw new ValidationException(validationResult);

            var doctor = await _doctorRepository.GetByIdAsync(request.Id);

            if(doctor == null)
                throw new NotFoundException(nameof(doctor), request.Id);

            doctor.UpdateDoctor(request.Email, request.PhoneNumber, request.State, request.City,
                request.Country, request.StreetName, request.PostalCode);

            await _doctorRepository.UpdateAsync(doctor);

            await _unitOfWork.SaveAsync();
        }
    }
}
