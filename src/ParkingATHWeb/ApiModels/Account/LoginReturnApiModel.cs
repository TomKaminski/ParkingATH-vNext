namespace ParkingATHWeb.ApiModels.Account
{
    public class LoginReturnApiModel
    {
        public string Name { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public int Charges { get; set; }
        public string PasswordHash { get; set; }
        public string ImageBase64 { get; set; }
    }
}
