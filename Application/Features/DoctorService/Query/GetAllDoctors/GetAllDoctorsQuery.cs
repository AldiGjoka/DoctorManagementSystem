using Application.Common.Interfaces.Persistance;
using Domain.Entity;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.DoctorService.Query.GetAllDoctors
{
    public record GetAllDoctorsQuery : IRequest<IReadOnlyList<DoctorDTO>>
    {
    }

    public class GetAllDoctorsQueryHandler : IRequestHandler<GetAllDoctorsQuery, IReadOnlyList<DoctorDTO>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IRepository<Doctor> _doctorRepository;

        public GetAllDoctorsQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _doctorRepository = unitOfWork.Repository<Doctor>();
        }

        public async Task<IReadOnlyList<DoctorDTO>> Handle(GetAllDoctorsQuery request, CancellationToken cancellationToken)
        {
            var result = await _doctorRepository.ListAllAsync();

            var dto = new DoctorDTO().MapToDoctorDTOList(result);

            return dto;
        }
    }
}
