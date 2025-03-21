namespace SalesDatePrediction.Models
{
    public class OrderDetail
    {
        public int OrderId { get; set; }
        public int ProductId { get; set; }
        public decimal UnitPrice { get; set; }
        public short Qty { get; set; }
        public decimal Discount { get; set; }
        public  Order? Order { get; set; } // Relación con Order
        public  Product? Product { get; set; } // Relación con Product
    }
}
