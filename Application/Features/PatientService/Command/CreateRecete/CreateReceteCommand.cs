using Application.Common.Exceptions;
using Application.Common.Interfaces.Persistance;
using Domain.Entity.PatientAggregate;
using Domain.ValueObjects;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.PatientService.Command.CreateRecete
{
    public record CreateReceteCommand : IRequest
    {
        public int PatientId { get; set; }
        public List<Prescriptions> Prescriptions { get; set; }
        public DateTime Date { get; set; }
    }

    public class CreateReceteCommandHandler : IRequestHandler<CreateReceteCommand>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IRepository<Patient> _patientRepository;

        public CreateReceteCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _patientRepository = _unitOfWork.Repository<Patient>();
        }
        public async Task Handle(CreateReceteCommand request, CancellationToken cancellationToken)
        {
            var patient = await _patientRepository.GetByIdAsync(request.PatientId);

            if (patient == null)
                throw new NotFoundException(nameof(Patient), request.PatientId);

            patient.CreateRecete(request.Prescriptions, request.Date);

            await _patientRepository.UpdateAsync(patient);

            await _unitOfWork.SaveAsync();
        }
    }
}
