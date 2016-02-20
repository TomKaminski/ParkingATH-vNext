using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using ParkingATHWeb.Contracts.Common;
using ParkingATHWeb.Contracts.DTO.GateUsage;
using ParkingATHWeb.Contracts.Services.Base;

namespace ParkingATHWeb.Contracts.Services
{
    public interface IGateUsageService : IEntityService<GateUsageBaseDto, Guid>, IDependencyService
    {
        Task<ServiceResult<IEnumerable<GateUsageAdminDto>>> GetAllAdminAsync();

        Task<ServiceResult<IEnumerable<GateUsageAdminDto>>> GetAllAdminAsync(Expression<Func<GateUsageBaseDto, bool>> predicate);

        ServiceResult<Dictionary<DateTime, int>> GetGateUsagesChartData(
            IEnumerable<GateUsageBaseDto> gateUsagesFiltered, DateTime startDate, DateTime endDate);

        Task<ServiceResult<Dictionary<DateTime, int>>> GetGateUsagesChartData(DateTime startDate, DateTime endDate, int userId);
    }
}
