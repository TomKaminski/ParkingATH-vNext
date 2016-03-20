namespace ParkingATHWeb.Contracts.DTO.Payments
{
    public class Status
    {
        public string statusCode { get; set; }
    }

    public class PaymentResponse
    {
        public Status status { get; set; }
        public string redirectUri { get; set; }
        public string orderId { get; set; }
        public string extOrderId { get; set; }
    }
}
