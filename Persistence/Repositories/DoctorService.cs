using Application.Common.Interfaces.Persistance;
using Domain.Entity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Repositories
{
    public class DoctorService : BaseRepository<Doctor>, IDoctorService
    {
        public DoctorService(ClinicDbContext context) : base(context)
        {
        }

        public async Task<Doctor> GetDoctorByEmail(string email)
        {
            var response = await _context.Doctors.FirstOrDefaultAsync(x => x.Email == email);

            return response;
        }
    }
}
