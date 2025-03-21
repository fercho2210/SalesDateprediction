namespace SalesDatePrediction.Dto
{
    public class OrderCreateDto
    {
        public int EmpId { get; set; }
        public int ShipperId { get; set; }
        public string ShipName { get; set; }= string.Empty;
        public string ShipAddress { get; set; } = string.Empty; 
        public string ShipCity { get; set; } = string.Empty;
        public DateTime OrderDate { get; set; }
        public DateTime RequiredDate { get; set; }
        public DateTime? ShippedDate { get; set; }
        public decimal Freight { get; set; }
        public string ShipCountry { get; set; } = string.Empty;
        public int CustId { get; set; } 
        public required OrderDetailCreateDto OrderDetail { get; set; } // Detalle de la orden
    }
}
