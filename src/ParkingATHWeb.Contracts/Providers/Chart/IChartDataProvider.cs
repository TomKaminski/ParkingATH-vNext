using System.Threading.Tasks;
using ParkingATHWeb.Contracts.DTO.Chart;

namespace ParkingATHWeb.Contracts.Providers.Chart
{
    public interface IChartDataProvider
    {
        Task<ChartListDto> GetChartData(ChartRequestDto request);
    }
}
