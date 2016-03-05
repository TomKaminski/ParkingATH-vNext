using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ParkingATHWeb.Contracts.DTO.Chart;
using ParkingATHWeb.Contracts.Providers.Chart;
using ParkingATHWeb.DataAccess.Interfaces;

namespace ParkingATHWeb.Business.Providers.Chart
{
    public class GateUsagesChartDataProvider : BaseChartDataProvider, IGateUsagesChartDataProvider
    {
        private readonly IGateUsageRepository _gateUsagesRepository;

        public GateUsagesChartDataProvider(IGateUsageRepository gateUsagesRepository)
        {
            _gateUsagesRepository = gateUsagesRepository;
        }


        protected override async Task<Dictionary<DateTime, int>> GetData(ChartRequestDto request)
        {
            var gateUsages = await _gateUsagesRepository.GetAllAsync(x => x.UserId == request.UserId && x.DateOfUse >= request.DateRange.StartDate && x.DateOfUse <= request.DateRange.EndDate);
            return gateUsages.GroupBy(x => x.DateOfUse.Date).ToDictionary(x => x.Key, x => x.Count());
        }
    }
}
