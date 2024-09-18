using calisma1.Interfaces;
using calisma1.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace calisma1.Services
{
    public class GenericService<T> : IGenericService<T> where T : class
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IGenericRepository<T> _repository;

        public GenericService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _repository = _unitOfWork.GetRepository<T>();
        }

        public async Task<Response<List<T>>> GetAllAsync()
        {
            var entities = await _repository.GetAllAsync();
            return new Response<List<T>>(entities.ToList(), true, $"{typeof(T).Name} entities successfully retrieved.");
        }

        public async Task<Response<T>> GetByIdAsync(int id)
        {
            var entity = await _repository.GetByIdAsync(id);
            if (entity == null)
            {
                return new Response<T>(null, false, $"{typeof(T).Name} entity not found.");
            }
            return new Response<T>(entity, true, $"{typeof(T).Name} entity successfully retrieved.");
        }

        public async Task<Response<T>> CreateAsync(T entity)
        {
            await _repository.AddAsync(entity);
            await _unitOfWork.SaveAsync();
            return new Response<T>(entity, true, $"{typeof(T).Name} entity successfully created.");
        }

        public async Task<Response<T>> UpdateAsync(int id, T entity)
        {
            var existingEntity = await _repository.GetByIdAsync(id);
            if (existingEntity == null)
            {
                return new Response<T>(null, false, $"{typeof(T).Name} entity not found.");
            }

            _repository.Update(entity);
            await _unitOfWork.SaveAsync();

            return new Response<T>(entity, true, $"{typeof(T).Name} entity successfully updated.");
        }

        public async Task<Response<bool>> DeleteAsync(int id)
        {
            var entity = await _repository.GetByIdAsync(id);
            if (entity == null)
            {
                return new Response<bool>(false, false, $"{typeof(T).Name} entity not found.");
            }

            _repository.Delete(entity);
            await _unitOfWork.SaveAsync();

            return new Response<bool>(true, true, $"{typeof(T).Name} entity successfully deleted.");
        }
    }
}