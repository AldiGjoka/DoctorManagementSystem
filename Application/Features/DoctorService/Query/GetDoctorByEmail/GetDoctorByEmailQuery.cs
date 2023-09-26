using Application.Common.Exceptions;
using Application.Common.Interfaces.Persistance;
using Domain.Entity;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.DoctorService.Query.GetDoctorByEmail
{
    public record GetDoctorByEmailQuery : IRequest<DoctorDTO>
    {
        public string Email { get; set; }
    }

    public class GetDoctorByEmailQueryHandler : IRequestHandler<GetDoctorByEmailQuery, DoctorDTO>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IDoctorService _doctorRepository;

        public GetDoctorByEmailQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _doctorRepository = _unitOfWork.DoctorService;
        }
        public async Task<DoctorDTO> Handle(GetDoctorByEmailQuery request, CancellationToken cancellationToken)
        {
            var response = await _doctorRepository.GetDoctorByEmail(request.Email);

            if(response == null)
                throw new NotFoundException(nameof(Doctor), request.Email);

            var doctorDto = new DoctorDTO().MapToDoctorDTO(response);

            return doctorDto;
        }
    }
}
