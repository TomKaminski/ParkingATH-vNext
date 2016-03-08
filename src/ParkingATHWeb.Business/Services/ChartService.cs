using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using ParkingATHWeb.Contracts.Common;
using ParkingATHWeb.Contracts.DTO.Chart;
using ParkingATHWeb.Contracts.DTO.UserPreferences;
using ParkingATHWeb.Contracts.Providers.Chart;
using ParkingATHWeb.Contracts.Services;
using ParkingATHWeb.DataAccess.Common;
using ParkingATHWeb.DataAccess.Interfaces;
using ParkingATHWeb.Model.Concrete;
using ParkingATHWeb.Shared.Enums;

namespace ParkingATHWeb.Business.Services
{
    public class ChartService : IChartService
    {
        private readonly IGateUsagesChartDataProvider _gateUsagesChartDataProvider;
        private readonly IOrdersChartDataProvider _ordersChartDataProvider;
        private readonly IUserPreferencesRepository _userPreferencesRepository;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;


        public ChartService(IGateUsagesChartDataProvider gateUsagesChartDataProvider, IOrdersChartDataProvider ordersChartDataProvider, IUserPreferencesRepository userPreferencesRepository, IMapper mapper, IUnitOfWork unitOfWork)
        {
            _gateUsagesChartDataProvider = gateUsagesChartDataProvider;
            _ordersChartDataProvider = ordersChartDataProvider;
            _userPreferencesRepository = userPreferencesRepository;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
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

        public async Task<ServiceResult<Dictionary<ChartType,ChartListDto>, ChartRequestDto>> GetDefaultDataAsync(int userId)
        {
            var chartRequest = new ChartRequestDto(DateTime.Today.AddDays(-6), DateTime.Today.AddDays(1).AddSeconds(-1),
                ChartType.GateOpenings,ChartGranuality.PerDay, userId);
            var gateOpeningsData = await GetDataAsync(chartRequest);
            chartRequest.Type = ChartType.Orders;
            var orderData = await GetDataAsync(chartRequest);
            var resultDictionary = new Dictionary<ChartType,ChartListDto>()
            {
                {ChartType.GateOpenings, gateOpeningsData.IsValid ? gateOpeningsData.Result : new ChartListDto() },
                {ChartType.Orders, orderData.IsValid ? orderData.Result : new ChartListDto() }
            };
            return ServiceResult<Dictionary<ChartType, ChartListDto>, ChartRequestDto>.Success(resultDictionary, chartRequest);
        }
    }
}
