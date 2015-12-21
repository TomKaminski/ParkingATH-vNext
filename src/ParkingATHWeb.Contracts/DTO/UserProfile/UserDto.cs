using System;
using ParkingATHWeb.Contracts.Common;

namespace ParkingATHWeb.Contracts.DTO.UserProfile
{
    public class UserDto : BaseDto
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public int Charges { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public bool IsAdmin { get; set; }

  
        public string PasswordHash { get; set; }
        public string PasswordSalt { get; set; }
        public bool LockedOut { get; set; }
        public int UnsuccessfulLoginAttempts { get; set; }
        public DateTime LockedTo { get; set; }


        public long PasswordChangeTokenId { get; set; }
        public long EmailChangeTokenId { get; set; }
    }
}
