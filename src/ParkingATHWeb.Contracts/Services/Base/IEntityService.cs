using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using ParkingATHWeb.Contracts.Common;

namespace ParkingATHWeb.Contracts.Services.Base
{
    public interface IEntityService<TDto>
        where TDto : BaseDto
    {
        //sync methods
        ServiceResult<TDto> Get(object id);
        ServiceResult<TDto> Get(Expression<Func<TDto, bool>> predicate);

        ServiceResult<TDto> Create(TDto entity);
        ServiceResult CreateMany(IEnumerable<TDto> entities);

        ServiceResult Edit(TDto entity);
        ServiceResult EditMany(IList<TDto> entities);

        ServiceResult Delete(TDto entity);
        ServiceResult DeleteMany(IEnumerable<TDto> entities);

        ServiceResult<IEnumerable<TDto>> GetAll();
        ServiceResult<IEnumerable<TDto>> GetAll(Expression<Func<TDto, bool>> predicate);


        //async methods
        Task<ServiceResult<TDto>> GetAsync(object id);
        Task<ServiceResult<TDto>> GetAsync(Expression<Func<TDto, bool>> predicate);

        Task<ServiceResult<TDto>> CreateAsync(TDto entity);
        Task<ServiceResult> CreateManyAsync(IEnumerable<TDto> entities);

        Task<ServiceResult> EditAsync(TDto entity);
        Task<ServiceResult> EditManyAsync(IEnumerable<TDto> entities);

        Task<ServiceResult> DeleteAsync(TDto entity);
        Task<ServiceResult> DeleteManyAsync(IEnumerable<TDto> entities);

        Task<ServiceResult<IEnumerable<TDto>>> GetAllAsync();
        Task<ServiceResult<IEnumerable<TDto>>> GetAllAsync(Expression<Func<TDto, bool>> predicate);
    }
}
