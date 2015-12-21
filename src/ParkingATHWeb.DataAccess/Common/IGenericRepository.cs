using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace ParkingATHWeb.DataAccess.Common
{
    public interface IGenericRepository<T>
    {
        //Basic methods
        T Add(T entity);
        void Delete(T entity);
        void Edit(T entity);

        IQueryable<T> Include(Expression<Func<T, object>> include);

        //Sync
        IQueryable<T> GetAll();

        IQueryable<T> GetAll(Expression<Func<T, bool>> expression);

        T Find(object id);
            
        T First(Expression<Func<T, bool>> expression);

        T FirstOrDefault(Expression<Func<T, bool>> expression);


        //Async
        Task<IEnumerable<T>> GetAllAsync();

        Task<IEnumerable<T>> GetAllAsync(Expression<Func<T, bool>> expression);

        Task<T> FindAsync(object id);

        Task<T> FirstAsync(Expression<Func<T, bool>> expression);

        Task<T> FirstOrDefaultAsync(Expression<Func<T, bool>> expression);
    }
}
