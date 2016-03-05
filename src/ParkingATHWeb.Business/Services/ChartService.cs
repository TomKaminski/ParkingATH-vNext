using System;
using System.Threading.Tasks;
using ParkingATHWeb.Contracts.Common;
using ParkingATHWeb.Contracts.DTO.Chart;
using ParkingATHWeb.Contracts.Providers.Chart;
using ParkingATHWeb.Contracts.Services;
using ParkingATHWeb.Shared.Enums;

namespace ParkingATHWeb.Business.Services
{
    public class ChartService : IChartService
    {
        private readonly IGateUsagesChartDataProvider _gateUsagesChartDataProvider;
        private readonly IOrdersChartDataProvider _ordersChartDataProvider;


        public ChartService(IGateUsagesChartDataProvider gateUsagesChartDataProvider, IOrdersChartDataProvider ordersChartDataProvider)
        {
            _gateUsagesChartDataProvider = gateUsagesChartDataProvider;
            _ordersChartDataProvider = ordersChartDataProvider;
        }

        public async Task<ServiceResult<ChartListDto>> GetDataAsync(ChartRequestDto request)
        {
            switch (request.Type)
            {
                case ChartType.GateOpenings:
                    return ServiceResult<ChartListDto>.Success(await _gateUsagesChartDataProvider.GetChartData(request));
                case ChartType.Orders:
                    return ServiceResult<ChartListDto>.Success(await _ordersChartDataProvider.GetChartData(request));
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }
}
