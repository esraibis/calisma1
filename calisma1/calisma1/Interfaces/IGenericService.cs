using calisma1.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace calisma1.Interfaces
{
    public interface IGenericService<T> where T : class
    {
        Task<Response<List<T>>> GetAllAsync();
        Task<Response<T>> GetByIdAsync(int id);
        Task<Response<T>> CreateAsync(T entity);
        Task<Response<T>> UpdateAsync(int id, T entity);
        Task<Response<bool>> DeleteAsync(int id);
    }
}