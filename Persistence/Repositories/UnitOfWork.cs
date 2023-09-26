using Application.Common.Interfaces.Persistance;
using Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Repositories
{
    public class UnitOfWork : IUnitOfWork, IDisposable
    {
        private readonly ClinicDbContext _context;
        public UnitOfWork(ClinicDbContext context)
        {
            _context = context;
        }

        private IDoctorService _doctorService;

        public IDoctorService DoctorService
        {
            get
            {
                if(_doctorService == null)
                {
                    _doctorService = new DoctorService(_context);
                }

                return _doctorService;
            }
        }

        public void Dispose()
        {
            _context.Dispose();
        }

        public IRepository<T> Repository<T>() where T : class, IAggregateRoot
        {
            return new BaseRepository<T>(_context);
        }

        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
