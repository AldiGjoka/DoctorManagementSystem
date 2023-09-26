using Application.Common.Exceptions;
using Application.Common.Interfaces.Persistance;
using Domain.Entity.PatientAggregate;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.PatientService.Query.GetPatientById
{
    public record GetPatientByIdQuery : IRequest<PatientDTO>
    {
        public int DoctorId { get; set; }
        public int PatientId { get; set; }
    }

    public class GetPatientByIdQueryHandler : IRequestHandler<GetPatientByIdQuery, PatientDTO>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IRepository<Patient> _patientRepository;

        public GetPatientByIdQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _patientRepository = _unitOfWork.Repository<Patient>();
        }
        public async Task<PatientDTO> Handle(GetPatientByIdQuery request, CancellationToken cancellationToken)
        {
            var result = await _patientRepository.GetByIdAsync(request.PatientId);

            if (result == null || result.DoctorId != request.DoctorId)
                throw new NotFoundException(nameof(Patient), request.PatientId);

            var dto = new PatientDTO().MapToPatientDto(result);

            return dto;
        }
    }
}
