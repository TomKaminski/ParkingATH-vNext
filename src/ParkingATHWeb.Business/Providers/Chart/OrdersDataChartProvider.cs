using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ParkingATHWeb.Contracts.DTO.Chart;
using ParkingATHWeb.Contracts.Providers.Chart;
using ParkingATHWeb.DataAccess.Interfaces;

namespace ParkingATHWeb.Business.Providers.Chart
{
    public class OrdersDataChartProvider : BaseChartDataProvider, IOrdersChartDataProvider
    {
        private readonly IOrderRepository _ordersRepository;

        public OrdersDataChartProvider(IOrderRepository ordersRepository)
        {
            _ordersRepository = ordersRepository;
        }


        protected override async Task<Dictionary<DateTime, int>> GetData(ChartRequestDto request)
        {
            var orders = await _ordersRepository.GetAllAsync(x => x.UserId == request.UserId && x.Date >= request.DateRange.StartDate && x.Date <= request.DateRange.EndDate);
            return orders.GroupBy(x => x.Date).ToDictionary(x => x.Key, x => x.Count());
        }
    }
}
