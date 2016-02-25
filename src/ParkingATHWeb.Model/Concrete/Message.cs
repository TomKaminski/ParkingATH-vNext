using System;
using System.Collections.Generic;
using ParkingATHWeb.Model.Common;
using ParkingATHWeb.Shared.Enums;

namespace ParkingATHWeb.Model.Concrete
{
    public class Message:Entity<Guid>
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
        public virtual User User { get; set; }

        public long ViewInBrowserTokenId { get; set; }
        public virtual Token ViewInBrowserToken { get; set; }

    }
}
