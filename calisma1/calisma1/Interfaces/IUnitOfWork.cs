using System;
using System.Threading.Tasks;
using calisma1.Interfaces;

namespace calisma1.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IGenericRepository<T> GetRepository<T>() where T : class;
        Task<int> SaveAsync();
    }
}