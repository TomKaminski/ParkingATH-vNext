using System;
using System.Linq;
using AutoMapper;
using ParkingATHWeb.Business.HelperClasses;
using ParkingATHWeb.Contracts.DTO.Weather;
using ParkingATHWeb.Contracts.DTO.WeatherInfo;
using ParkingATHWeb.Model.Concrete;
using ParkingATHWeb.Shared.Helpers;
using Weather = ParkingATHWeb.Model.Concrete.Weather;

namespace ParkingATHWeb.Resolver.Mappings
{
    public static partial class BackendMappingProvider
    {
        public static void InitMappings()
        {
            InitializeOrderMappings();
            InitializeGateUsageMappings();
            InitializePriceTresholdMappings();
            InitializeStudentMappings();
            InitializeTokenMappings();
            InitalizeMessageMappings();
            InitializeWeatherMappings();
        }
    }
}
