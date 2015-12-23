﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using ParkingATHWeb.Model.Common;

namespace ParkingATHWeb.DataAccess.Common
{
    public interface IGenericRepository<T, TType>
        where TType : struct
        where T : Entity<TType>
    {
        //Basic methods
        T Add(T entity);
        void Delete(T entity);
        void Edit(T entity);

        IQueryable<T> Include(Expression<Func<T, object>> include);

        //Sync
        IQueryable<T> GetAll();

        IQueryable<T> GetAll(Expression<Func<T, bool>> expression);

        T Find(TType id);

        T First(Expression<Func<T, bool>> expression);

        T FirstOrDefault(Expression<Func<T, bool>> expression);


        //Async
        Task<IEnumerable<T>> GetAllAsync();

        Task<IEnumerable<T>> GetAllAsync(Expression<Func<T, bool>> expression);

        Task<T> FindAsync(TType id);

        Task<T> FirstAsync(Expression<Func<T, bool>> expression);

        Task<T> FirstOrDefaultAsync(Expression<Func<T, bool>> expression);
    }
}
