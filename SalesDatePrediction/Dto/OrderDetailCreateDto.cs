namespace SalesDatePrediction.Dto
{
    public class OrderDetailCreateDto
    {
        public int ProductId { get; set; }
        public decimal UnitPrice { get; set; }
        public short Qty { get; set; }
        public decimal Discount { get; set; }
    }
}
