using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using AutoMapper;
using ParkingATHWeb.Contracts.Common;
using ParkingATHWeb.Contracts.Services.Base;
using ParkingATHWeb.DataAccess.Common;
using ParkingATHWeb.Model.Common;

namespace ParkingATHWeb.Business.Services.Base
{
    public class EntityService<TDto, TEntity>:IEntityService<TDto>
        where TDto: BaseDto
        where TEntity: BaseEntity
    {
        private readonly IGenericRepository<TEntity> _repository;
        private readonly IUnitOfWork _unitOfWork;

        public EntityService(IGenericRepository<TEntity> repository, IUnitOfWork unitOfWork)
        {
            _repository = repository;
            _unitOfWork = unitOfWork;
        }

        public ServiceResult<TDto> Get(object id)
        {
            var result = Mapper.Map<TDto>(_repository.Find(id));
            return ServiceResult<TDto>.Success(result);
        }

        public ServiceResult<TDto> Get(Expression<Func<TDto, bool>> predicate)
        {
            throw new NotImplementedException();
        }

        public ServiceResult<TDto> Create(TDto entity)
        {
            throw new NotImplementedException();
        }

        public ServiceResult CreateMany(IEnumerable<TDto> entities)
        {
            throw new NotImplementedException();
        }

        public ServiceResult Edit(TDto entity)
        {
            throw new NotImplementedException();
        }

        public ServiceResult EditMany(IList<TDto> entities)
        {
            throw new NotImplementedException();
        }

        public ServiceResult Delete(TDto entity)
        {
            throw new NotImplementedException();
        }

        public ServiceResult DeleteMany(IEnumerable<TDto> entities)
        {
            throw new NotImplementedException();
        }

        public ServiceResult<IEnumerable<TDto>> GetAll()
        {
            throw new NotImplementedException();
        }

        public ServiceResult<IEnumerable<TDto>> GetAll(Expression<Func<TDto, bool>> predicate)
        {
            throw new NotImplementedException();
        }

        public Task<ServiceResult<TDto>> GetAsync(object id)
        {
            throw new NotImplementedException();
        }

        public Task<ServiceResult<TDto>> GetAsync(Expression<Func<TDto, bool>> predicate)
        {
            throw new NotImplementedException();
        }

        public Task<ServiceResult<TDto>> CreateAsync(TDto entity)
        {
            throw new NotImplementedException();
        }

        public Task<ServiceResult> CreateManyAsync(IEnumerable<TDto> entities)
        {
            throw new NotImplementedException();
        }

        public Task<ServiceResult> EditAsync(TDto entity)
        {
            throw new NotImplementedException();
        }

        public Task<ServiceResult> EditManyAsync(IEnumerable<TDto> entities)
        {
            throw new NotImplementedException();
        }

        public Task<ServiceResult> DeleteAsync(TDto entity)
        {
            throw new NotImplementedException();
        }

        public Task<ServiceResult> DeleteManyAsync(IEnumerable<TDto> entities)
        {
            throw new NotImplementedException();
        }

        public Task<ServiceResult<IEnumerable<TDto>>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<ServiceResult<IEnumerable<TDto>>> GetAllAsync(Expression<Func<TDto, bool>> predicate)
        {
            throw new NotImplementedException();
        }
    }
}
