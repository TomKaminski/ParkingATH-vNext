using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.Data.Entity;
using ParkingATHWeb.Model;
using ParkingATHWeb.Model.Common;

namespace ParkingATHWeb.DataAccess.Common
{
    public class GenericRepository<T> : IGenericRepository<T> where T : BaseEntity
    {
        private readonly DbContext _entities;
        private readonly DbSet<T> _dbset;

        protected GenericRepository(IDatabaseFactory factory)
        {
            _entities = factory.Get();
            _dbset = _entities.Set<T>();
        }

        public T Add(T entity)
        {
            _dbset.Add(entity);
            return entity;
        }

        public void Delete(T entity)
        {
            _dbset.Attach(entity);
            _dbset.Remove(entity);
        }

        public void Edit(T entity)
        {
            _entities.Entry(entity).State = EntityState.Modified;
        }

        public IQueryable<T> Include(Expression<Func<T, object>> include)
        {
            return _dbset.Include(include);
        }


        //Sync
        public T Find(object id)
        {
            return _dbset.Find(id);
        }

        public T First(Expression<Func<T, bool>> expression)
        {
            return _dbset.First(expression);
        }

        public T FirstOrDefault(Expression<Func<T, bool>> expression)
        {
            return _dbset.FirstOrDefault(expression);
        }

        public IQueryable<T> GetAll()
        {
            return _dbset;
        }

        public IQueryable<T> GetAll(Expression<Func<T, bool>> expression)
        {
            return _dbset.Where(expression);
        }

        //Async
        public async Task<IEnumerable<T>> GetAllAsync()
        {
            return await _dbset.ToListAsync();
        }

        public async Task<IEnumerable<T>> GetAllAsync(Expression<Func<T, bool>> expression)
        {
            return await _dbset.Where(expression).ToListAsync();
        }

        public async Task<T> FindAsync(object id)
        {
            return await _dbset.FindAsync(id);
        }

        public async Task<T> FirstAsync(Expression<Func<T, bool>> expression)
        {
            return await _dbset.FirstAsync(expression);
        }

        public async Task<T> FirstOrDefaultAsync(Expression<Func<T, bool>> expression)
        {
            return await _dbset.FirstOrDefaultAsync(expression);
        }
    }
}
