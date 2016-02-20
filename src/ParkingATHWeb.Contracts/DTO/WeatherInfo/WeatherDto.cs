using System;
using ParkingATHWeb.Contracts.Common;

namespace ParkingATHWeb.Contracts.DTO.WeatherInfo
{
    public class WeatherInfoDto : BaseDto<Guid>
    {
        public int WeatherConditionId { get; set; }
        public string WeatherMain { get; set; }
        public string WeatherDescription { get; set; }
        public Guid WeatherId { get; set; }
    }
}
