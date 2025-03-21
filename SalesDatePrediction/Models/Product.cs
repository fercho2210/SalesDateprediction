namespace SalesDatePrediction.Models
{
    public class Product
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; } = string.Empty;
        public int SupplierId { get; set; }
        public int CategoryId { get; set; }
        public decimal UnitPrice { get; set; }
        public bool Discontinued { get; set; }
        public required Supplier Supplier { get; set; } // Relación con Supplier
        public required Category Category { get; set; } // Relación con Category
    }
}
