using Application.Common.Interfaces.Persistance;
using Domain.Entity.PatientAggregate;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.PatientService.Query.GetAllPatients
{
    public record GetAllPatientsQuery : IRequest<IReadOnlyList<PatientDTO>>
    {
        public int DoctorId { get; set; }
    }

    public class GetAllPatientsQueryHandler : IRequestHandler<GetAllPatientsQuery, IReadOnlyList<PatientDTO>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IRepository<Patient> _patientRepository;

        public GetAllPatientsQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _patientRepository = _unitOfWork.Repository<Patient>();
        }

        public async Task<IReadOnlyList<PatientDTO>> Handle(GetAllPatientsQuery request, CancellationToken cancellationToken)
        {
            var result = await _patientRepository.ListAllAsync(patient => patient.DoctorId == request.DoctorId);

            var dto = new PatientDTO().MapToPatientDtoList(result);

            return dto;
        }
    }
}
