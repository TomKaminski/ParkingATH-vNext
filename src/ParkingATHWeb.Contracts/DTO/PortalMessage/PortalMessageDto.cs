using System;
using ParkingATHWeb.Contracts.Common;
using ParkingATHWeb.Shared.Enums;

namespace ParkingATHWeb.Contracts.DTO.SupportMessage
{
    public class PortalMessageDto : BaseDto<Guid>
    {
        public DateTime CreateDate { get; set; }
        public string Text { get; set; }
        public bool ToAdmin { get; set; }
        public bool IsNotification { get; set; }
        public PortalMessageEnum PortalMessageType { get; set; }
        public bool IsDisplayed { get; set; }

        public Guid? PreviousMessageId { get; set; }
        public int UserId { get; set; }
        public int ReceiverUserId { get; set; }
    }
}
