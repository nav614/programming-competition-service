using System;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using ProgrammingCompetitionService.Intrerfaces;
using ProgrammingCompetitionService.Models;

namespace ProgrammingCompetitionService.Repositories
{
	public class GenericRepository<T> : IGenericRepository<T> where T : class
	{
		protected readonly PCContext _context;

		public GenericRepository(PCContext context)
		{
			_context = context;
		}

        public async Task<T> Add(T entity)
        {
            _context.Set<T>().Add(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<IEnumerable<T>> GetAll()
        {
            return await _context.Set<T>().ToListAsync();
        }

        public async Task<T> GetById(Guid id)
        {
            return await _context.Set<T>().FindAsync(id);
        }

        public async Task<T> Remove(Guid id)
        {
            var entity = await _context.Set<T>().FindAsync(id);
            if (entity == null)
            {
                return entity;
            }

            _context.Set<T>().Remove(entity);
            await _context.SaveChangesAsync();

            return entity;
        }
    }
}

