using System.Threading.Tasks;
using ParkingATHWeb.Contracts.Common;
using ParkingATHWeb.Contracts.DTO.Chart;
using ParkingATHWeb.Contracts.Services.Base;

namespace ParkingATHWeb.Contracts.Services
{
    public interface IChartService : IDependencyService
    {
        Task<ServiceResult<ChartListDto>> GetDataAsync(ChartRequestDto request);
    }
}
