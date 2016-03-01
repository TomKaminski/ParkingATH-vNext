using System;
using System.ComponentModel.DataAnnotations.Schema;
using ParkingATHWeb.Model.Common;

namespace ParkingATHWeb.Model.Concrete
{
    public class PortalMessage : Entity<Guid>
    {
        public DateTime CreateDate { get; set; }
        public string Text { get; set; }
        public bool ToAdmin { get; set; }
        public bool IsNotification { get; set; }
        public int PortalMessageType { get; set; }
        public bool IsDisplayed { get; set; }

        public Guid? PreviousMessageId { get; set; }

        [ForeignKey("PreviousMessageId")]
        public virtual PortalMessage PreviousMessage { get; set; }

        public int UserId { get; set; }
        public virtual User User { get; set; }

        public int ReceiverUserId { get; set; }
        public virtual User ReceiverUser { get; set; }
    }
}

