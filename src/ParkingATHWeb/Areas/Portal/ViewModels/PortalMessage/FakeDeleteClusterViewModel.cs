using System;
using System.ComponentModel.DataAnnotations;

namespace ParkingATHWeb.Areas.Portal.ViewModels.PortalMessage
{
    public class FakeDeleteClusterViewModel
    {
        [Required]
        public Guid StarterMessageId { get; set; }
    }
}