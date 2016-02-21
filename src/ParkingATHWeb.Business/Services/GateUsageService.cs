using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.Data.Entity;
using ParkingATHWeb.Business.Services.Base;
using ParkingATHWeb.Contracts.Common;
using ParkingATHWeb.Contracts.DTO.GateUsage;
using ParkingATHWeb.Contracts.Services;
using ParkingATHWeb.DataAccess.Common;
using ParkingATHWeb.DataAccess.Interfaces;
using ParkingATHWeb.Model.Concrete;
using ParkingATHWeb.Shared.Helpers;

namespace ParkingATHWeb.Business.Services
{
    public class GateUsageService : EntityService<GateUsageBaseDto, GateUsage, Guid>, IGateUsageService
    {
        private readonly IGateUsageRepository _repository;
        private readonly IMapper _mapper;

        public GateUsageService(IUnitOfWork unitOfWork, IGateUsageRepository repository, IMapper mapper)
            : base(repository, unitOfWork, mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<ServiceResult<IEnumerable<GateUsageAdminDto>>> GetAllAdminAsync()
        {
            return ServiceResult<IEnumerable<GateUsageAdminDto>>
                .Success((await _repository.Include(x => x.User).ToListAsync())
                .Select(_mapper.Map<GateUsageAdminDto>));
        }

        public async Task<ServiceResult<IEnumerable<GateUsageAdminDto>>> GetAllAdminAsync(Expression<Func<GateUsageBaseDto, bool>> predicate)
        {
            return ServiceResult<IEnumerable<GateUsageAdminDto>>
                .Success((await _repository.Include(x => x.User)
                .Where(MapExpressionToEntity(predicate))
                .ToListAsync())
                .Select(_mapper.Map<GateUsageAdminDto>));
        }

        public ServiceResult<Dictionary<DateTime, int>> GetGateUsagesChartData(IEnumerable<GateUsageBaseDto> gateUsagesFiltered, DateTime startDate, DateTime endDate)
        {
            var result = new DateRange(startDate, endDate).Dates.ToDictionary(date => date, date => 0);
            var resultOfAggregation = gateUsagesFiltered.GroupBy(x => x.DateOfUse.Date).ToDictionary(x => x.Key, x => x.Count());
            foreach (var i in resultOfAggregation)
            {
                result[i.Key] = i.Value;
            }
            return ServiceResult<Dictionary<DateTime, int>>.Success(result);
        }

        public async Task<ServiceResult<Dictionary<DateTime, int>>> GetGateUsagesChartData(DateTime startDate, DateTime endDate, int userId)
        {
            var gateUsages = await _repository.GetAllAsync(x => x.UserId == userId && x.DateOfUse >= startDate && x.DateOfUse <= endDate);
            var result = new DateRange(startDate, endDate).Dates.ToDictionary(date => date, date => 0);
            var resultOfAggregation = gateUsages.GroupBy(x => x.DateOfUse).ToDictionary(x => x.Key, x => x.Count());
            foreach (var i in resultOfAggregation)
            {
                result[i.Key] = i.Value;
            }
            return ServiceResult<Dictionary<DateTime, int>>.Success(result);
        }
    }
}
