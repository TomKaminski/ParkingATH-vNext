using System;
using System.ComponentModel.DataAnnotations;
using ParkingATHWeb.Shared.Enums;
using ParkingATHWeb.ViewModels.Base;

namespace ParkingATHWeb.Areas.Portal.ViewModels.Message
{
    public class QuickMessageViewModel : SmartParkBaseViewModel
    {
        [Required]
        public string Text { get; set; }
        public bool ToAdmin => true;
        public bool IsNotification => false;
        public PortalMessageEnum PortalMessageType => PortalMessageEnum.MessageToAdminFromUser;
        public bool IsDisplayed => true;
        public Guid? PreviousMessageId => null;
        public DateTime CreateDate => DateTime.Now;
        public int UserId { get; set; }
        public int ReceiverUserId { get; set; }
    }
}
