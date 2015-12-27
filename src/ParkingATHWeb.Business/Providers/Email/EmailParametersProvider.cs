using System.Collections.Generic;
using ParkingATHWeb.Contracts.DTO.User;

namespace ParkingATHWeb.Business.Providers.Email
{
    public static class EmailParametersProvider
    {
        public static Dictionary<string, string> GetBaseParametersForEmail(UserBaseDto userData)
        {
            return new Dictionary<string, string>
            {
                {"Name",userData.Name },
                {"LastName",userData.LastName },
                {"Id",userData.Id.ToString() },
            };
        }
    }
}
