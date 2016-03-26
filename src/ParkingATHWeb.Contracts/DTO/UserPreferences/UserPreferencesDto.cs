using System;
using ParkingATHWeb.Contracts.Common;

namespace ParkingATHWeb.Contracts.DTO.UserPreferences
{
    public class UserPreferencesDto : BaseDto<int>
    {
        public bool ShrinkedSidebar { get; set; }
        public byte[] ProfilePhoto { get; set; }
        public Guid? ProfilePhotoId { get; set; }

        public int UserId { get; set; }
    }
}
