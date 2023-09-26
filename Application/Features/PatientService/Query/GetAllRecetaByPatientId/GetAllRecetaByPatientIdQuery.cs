using Application.Common.Exceptions;
using Application.Common.Interfaces.Persistance;
using Domain.Entity.PatientAggregate;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.PatientService.Query.GetAllRecetaByPatientId
{
    public record GetAllRecetaByPatientIdQuery : IRequest<List<RecetaDTO>>
    {
        public int PatientId { get; set; }
    }

    public class GetAllRecetaByPatientIdHandler : IRequestHandler<GetAllRecetaByPatientIdQuery, List<RecetaDTO>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IRepository<Patient> _patientRepository;

        public GetAllRecetaByPatientIdHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _patientRepository = _unitOfWork.Repository<Patient>();
        }
        public async Task<List<RecetaDTO>> Handle(GetAllRecetaByPatientIdQuery request, CancellationToken cancellationToken)
        {
            var patient = await _patientRepository.ListAllAsync(filter: p => p.Id == request.PatientId,
                includeProperties: "Receta");

            var patientFromDb = patient.FirstOrDefault(x => x.Id == request.PatientId);

            if(patientFromDb == null)
                throw new NotFoundException(nameof(Patient), request.PatientId);

            var recetaDto = new RecetaDTO().MapToRecetaDto(patientFromDb.Receta);

            return recetaDto;
        }
    }
}
