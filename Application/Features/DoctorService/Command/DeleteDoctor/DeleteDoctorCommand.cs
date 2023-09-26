using Application.Common.Exceptions;
using Application.Common.Interfaces.Persistance;
using Domain.Entity;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.DoctorService.Command.DeleteDoctor
{
    public record DeleteDoctorCommand : IRequest
    {
        public int Id { get; set; }
    }

    public class DeleteDoctorCommandHandler : IRequestHandler<DeleteDoctorCommand>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IRepository<Doctor> _doctorRepository;

        public DeleteDoctorCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _doctorRepository = unitOfWork.Repository<Doctor>();
        }
        public async Task Handle(DeleteDoctorCommand request, CancellationToken cancellationToken)
        {
            var doctor = await _doctorRepository.GetByIdAsync(request.Id);

            if(doctor == null)
                throw new NotFoundException(nameof(Doctor), request.Id);

            await _doctorRepository.DeleteAsync(doctor);

            await _unitOfWork.SaveAsync();
        }
    }
}
