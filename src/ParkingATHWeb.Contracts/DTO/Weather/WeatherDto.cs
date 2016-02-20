using System;
using System.Collections.Generic;
using ParkingATHWeb.Contracts.Common;
using ParkingATHWeb.Contracts.DTO.WeatherInfo;

namespace ParkingATHWeb.Contracts.DTO.Weather
{
    public class WeatherDto : BaseDto<Guid>
    {
        public int Clouds { get; set; }
        public double Temperature { get; set; }
        public double Pressure { get; set; }
        public double Humidity { get; set; }
        public DateTime DateOfRead { get; set; }
        public DateTime ValidToDate { get; set; }
        public List<WeatherInfoDto> WeatherInfo { get; set; }
    }
}
