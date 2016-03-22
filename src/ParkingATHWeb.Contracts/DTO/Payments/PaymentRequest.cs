using System.Collections.Generic;

namespace ParkingATHWeb.Contracts.DTO.Payments
{
    public class Buyer
    {
        public string email { get; set; }
        public string firstName { get; set; }
        public string lastName { get; set; }
    }

    public class Product
    {
        public string name { get; set; }
        public string unitPrice { get; set; }
        public string quantity { get; set; }
    }

    public class PaymentRequest
    {
        public string notifyUrl { get; set; }
        public string customerIp { get; set; }
        public string merchantPosId { get; set; }
        public string description { get; set; }
        public string continueUrl { get; set; }

        public string currencyCode => "PLN";

        public string totalAmount { get; set; }
        public string extOrderId { get; set; }
        public Buyer buyer { get; set; }
        public List<Product> products { get; set; }
    }
}
