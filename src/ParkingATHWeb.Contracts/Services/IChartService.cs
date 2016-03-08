using System.Collections.Generic;
using System.Threading.Tasks;
using ParkingATHWeb.Contracts.Common;
using ParkingATHWeb.Contracts.DTO.Chart;
using ParkingATHWeb.Contracts.Services.Base;
using ParkingATHWeb.Shared.Enums;

namespace ParkingATHWeb.Contracts.Services
{
    public interface IChartService : IDependencyService
    {
        Task<ServiceResult<ChartListDto>> GetDataAsync(ChartRequestDto request);
        Task<ServiceResult<Dictionary<ChartType, ChartListDto>, ChartRequestDto>> GetDefaultDataAsync(int userId);
    }
}
