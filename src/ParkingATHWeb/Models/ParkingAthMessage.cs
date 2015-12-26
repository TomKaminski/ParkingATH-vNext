namespace ParkingATHWeb.Models
{
    public class ParkingAthMessage
    {
        public class MessageDto
        {
            public string HtmlBody { get; set; }
            public string Title { get; set; }
            public string Recipents { get; set; }
            public string From { get; set; }
        }
    }
}
