using Domain.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Common.Interfaces.Persistance
{
    public interface IDoctorService : IRepository<Doctor>
    {
        Task<Doctor> GetDoctorByEmail(string email);
    }
}
