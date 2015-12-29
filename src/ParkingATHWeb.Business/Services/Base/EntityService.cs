using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using AutoMapper;
using ParkingATHWeb.Contracts.Common;
using ParkingATHWeb.Contracts.Services.Base;
using ParkingATHWeb.DataAccess.Common;
using ParkingATHWeb.Model.Common;
using ParkingATHWeb.Shared.Helpers;
using System.Linq;

namespace ParkingATHWeb.Business.Services.Base
{
    public class EntityService<TDto, TEntity,T> : IEntityService<TDto,T>
      where TDto : BaseDto<T>
      where TEntity : Entity<T>
      where T: struct 
    {
        private readonly IGenericRepository<TEntity,T> _repository;
        private readonly IUnitOfWork _unitOfWork;

        protected EntityService(IGenericRepository<TEntity,T> repository, IUnitOfWork unitOfWork)
        {
            _repository = repository;
            _unitOfWork = unitOfWork;
        }

        public virtual ServiceResult<TDto> Get(T id)
        {
            var result = Mapper.Map<TDto>(_repository.Find(id));
            return ServiceResult<TDto>.Success(result);
        }

        public virtual ServiceResult<TDto> Get(Expression<Func<TDto, bool>> predicate)
        {
            var result = Mapper.Map<TDto>(_repository.FirstOrDefault(MapExpressionToEntity(predicate)));
            return ServiceResult<TDto>.Success(result);
        }

        public virtual ServiceResult<TDto> Create(TDto entity)
        {
            var result = Mapper.Map<TDto>(_repository.Add(Mapper.Map<TEntity>(entity)));
            _unitOfWork.Commit();
            return ServiceResult<TDto>.Success(result);
        }

        public virtual ServiceResult CreateMany(IEnumerable<TDto> entities)
        {
            foreach (var entity in entities)
            {
                _repository.Add(Mapper.Map<TEntity>(entity));
            }
            _unitOfWork.Commit();
            return ServiceResult.Success();
        }

        public virtual ServiceResult Edit(TDto entity)
        {
            var obj = _repository.Find(entity.Id);
            if (obj == null)
            {
                return ServiceResult.Failure(new List<string> { "Nie znaleziono pasującego elementu." });
            }
            obj = Mapper.Map<TEntity>(entity);
            //obj = MapperHelper<TDto, TEntity>.MapNoIdToEntityOnEdit(entity, obj);
            _repository.Edit(obj);
            _unitOfWork.Commit();
            return ServiceResult.Success();
        }

        public virtual ServiceResult EditMany(IList<TDto> entities)
        {
            var ids = entities.Select(k => k.Id).ToList();
            var dbEntities = _repository.GetAll(x => ids.Contains(x.Id)).ToList();

            foreach (var t in entities)
            {
                var entity = dbEntities.Single(x => x.Id.Equals(t.Id));
                entity = MapperHelper<TDto, TEntity>.MapNoIdToEntityOnEdit(t, entity);
                _repository.Edit(entity);
            }
            _unitOfWork.Commit();
            return ServiceResult.Success();
        }

        public virtual ServiceResult Delete(TDto entity)
        {
            _repository.Delete(Mapper.Map<TEntity>(entity));
            _unitOfWork.Commit();
            return ServiceResult.Success();
        }

        public virtual ServiceResult DeleteMany(IEnumerable<TDto> entities)
        {
            foreach (var entity in entities)
            {
                _repository.Delete(Mapper.Map<TEntity>(entity));
            }
            _unitOfWork.Commit();
            return ServiceResult.Success();
        }

        public virtual ServiceResult Delete(T id)
        {
            var obj = _repository.Find(id);
            _repository.Delete(obj);
            _unitOfWork.Commit();
            return ServiceResult.Success();
        }

        public virtual ServiceResult DeleteMany(IEnumerable<T> ids)
        {
            var dbEntities = _repository.GetAll(x => ids.Contains(x.Id)).ToList();

            foreach (var t in dbEntities)
            {
                _repository.Delete(t);
            }
            _unitOfWork.Commit();
            return ServiceResult.Success();
        }

        public virtual ServiceResult<IEnumerable<TDto>> GetAll()
        {
            return ServiceResult<IEnumerable<TDto>>
                .Success(_repository.GetAll()
                .Select(Mapper.Map<TDto>).ToList());
        }

        public virtual ServiceResult<IEnumerable<TDto>> GetAll(Expression<Func<TDto, bool>> predicate)
        {
            return ServiceResult<IEnumerable<TDto>>
                .Success(_repository.GetAll(MapExpressionToEntity(predicate))
                .Select(Mapper.Map<TDto>).ToList());
        }

        public virtual async Task<ServiceResult<TDto>> GetAsync(T id)
        {
            return ServiceResult<TDto>.Success(Mapper.Map<TDto>(await _repository.FindAsync(id)));
        }

        public virtual async Task<ServiceResult<TDto>> GetAsync(Expression<Func<TDto, bool>> predicate)
        {
            return ServiceResult<TDto>
                .Success(Mapper.Map<TDto>(await _repository.FirstOrDefaultAsync(MapExpressionToEntity(predicate))));
        }

        public virtual async Task<ServiceResult<TDto>> CreateAsync(TDto entity)
        {
            var item = _repository.Add(Mapper.Map<TEntity>(entity));
            await _unitOfWork.CommitAsync();
            return ServiceResult<TDto>.Success(Mapper.Map<TDto>(item));
        }

        public virtual async Task<ServiceResult> CreateManyAsync(IEnumerable<TDto> entities)
        {
            foreach (var entity in entities)
            {
                _repository.Add(Mapper.Map<TEntity>(entity));
            }
            await _unitOfWork.CommitAsync();
            return ServiceResult.Success();
        }

        public virtual async Task<ServiceResult> EditAsync(TDto entity)
        {
            var obj = await _repository.FindAsync(entity.Id);
            if (obj == null)
            {
                return ServiceResult.Failure(new List<string> { "Nie znaleziono pasującego elementu." });
            }

            obj = MapperHelper<TDto, TEntity>.MapNoIdToEntityOnEdit(entity, obj);
            _repository.Edit(obj);
            await _unitOfWork.CommitAsync();
            return ServiceResult.Success();
        }

        public virtual async Task<ServiceResult> EditManyAsync(IList<TDto> entities)
        {
            var ids = entities.Select(k => k.Id).ToList();
            var dbEntities = _repository.GetAll(x => ids.Contains(x.Id)).ToList();

            foreach (var t in entities)
            {
                var entity = dbEntities.Single(x => x.Id.Equals(t.Id));
                entity = MapperHelper<TDto, TEntity>.MapNoIdToEntityOnEdit(t, entity);
                _repository.Edit(entity);
            }
            await _unitOfWork.CommitAsync();
            return ServiceResult.Success();
        }

        public virtual async Task<ServiceResult> DeleteAsync(TDto entity)
        {
            _repository.Delete(Mapper.Map<TEntity>(entity));
            await _unitOfWork.CommitAsync();
            return ServiceResult.Success();
        }

        public virtual async Task<ServiceResult> DeleteManyAsync(IEnumerable<TDto> entities)
        {
            foreach (var entity in entities)
            {
                _repository.Delete(Mapper.Map<TEntity>(entity));
            }
            await _unitOfWork.CommitAsync();
            return ServiceResult.Success();
        }

        public virtual async Task<ServiceResult> DeleteAsync(T id)
        {
            var obj = await _repository.FindAsync(id);
            _repository.Delete(obj);
            await _unitOfWork.CommitAsync();
            return ServiceResult.Success();
        }

        public virtual async Task<ServiceResult> DeleteManyAsync(IEnumerable<T> ids)
        {
            var dbEntities = (await _repository.GetAllAsync(x => ids.Contains(x.Id))).ToList();

            foreach (var t in dbEntities)
            {
                _repository.Delete(t);
            }
            await _unitOfWork.CommitAsync();
            return ServiceResult.Success();
        }

        public virtual async Task<ServiceResult<IEnumerable<TDto>>> GetAllAsync()
        {
            return ServiceResult<IEnumerable<TDto>>
                .Success((await _repository.GetAllAsync())
                .Select(Mapper.Map<TDto>).ToList());
        }

        public virtual async Task<ServiceResult<IEnumerable<TDto>>> GetAllAsync(Expression<Func<TDto, bool>> predicate)
        {
            return ServiceResult<IEnumerable<TDto>>
                .Success((await _repository.GetAllAsync(MapExpressionToEntity(predicate)))
                .Select(Mapper.Map<TDto>).ToList());
        }

        protected static Expression<Func<TEntity, bool>> MapExpressionToEntity(Expression<Func<TDto, bool>> predicate)
        {
            var param = Expression.Parameter(typeof(TEntity));
            var result = new CustomExpressionVisitor<TEntity>(param).Visit(predicate.Body);
            return Expression.Lambda<Func<TEntity, bool>>(result, param);
        }
    }
}
