using System;
using ParkingATHWeb.Contracts.Common;
using ParkingATHWeb.Shared.Enums;

namespace ParkingATHWeb.Contracts.DTO
{
    public class MessageDto : BaseDto<Guid>
    {
        public string To { get; set; }
        public string CC { get; set; }
        public string BCC { get; set; }
        public string Title { get; set; }
        public string MessageParameters { get; set; }
        public EmailType Type { get; set; }
        public string DisplayFrom { get; set; }
        public string From { get; set; }

        public int UserId { get; set; }
    }
}
