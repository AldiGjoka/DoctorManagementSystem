using Application.Common.Exceptions;
using Application.Common.Interfaces.Persistance;
using Application.Features.DoctorService.Query.GetAllDoctors;
using Domain.Entity;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.DoctorService.Query.GetDoctorById
{
    public record GetDoctorByIdQuery : IRequest<DoctorDTO>
    {
        public int DoctorId { get; set; }
    }

    public class GetDoctorByIdQueryHandler : IRequestHandler<GetDoctorByIdQuery, DoctorDTO>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IRepository<Doctor> _doctorRepository;

        public GetDoctorByIdQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _doctorRepository = _unitOfWork.Repository<Doctor>();
        }

        public async Task<DoctorDTO> Handle(GetDoctorByIdQuery request, CancellationToken cancellationToken)
        {
            var response = await _doctorRepository.GetByIdAsync(request.DoctorId);

            if(response == null)
            {
                throw new NotFoundException(nameof(Doctor), request.DoctorId);
            }

            var dto = new DoctorDTO().MapToDoctorDTO(response);

            return dto;
        }
    }
}
