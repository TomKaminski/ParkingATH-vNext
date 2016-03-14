using System;
using System.ComponentModel.DataAnnotations;

namespace ParkingATHWeb.Areas.Portal.ViewModels.PortalMessage
{
    public class SetDisplayedMessageViewModel
    {
        [Required]
        public Guid MessageId { get; set; }
    }
}