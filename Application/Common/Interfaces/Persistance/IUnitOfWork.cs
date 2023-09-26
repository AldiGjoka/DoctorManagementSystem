using Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Common.Interfaces.Persistance
{
    public interface IUnitOfWork : IDisposable
    {
        Task SaveAsync();
        IRepository<T> Repository<T>() where T : class, IAggregateRoot;
        public IDoctorService DoctorService { get; }
    }
}
