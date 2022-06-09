using System;
using System.Linq.Expressions;

namespace ProgrammingCompetitionService.Intrerfaces
{
	public interface IGenericRepository<T> where T : class
    {
        Task<T> GetById(Guid id);
        Task<IEnumerable<T>> GetAll();
        Task<T> Add(T entity);
        Task<T> Remove(Guid id);
    }
}

