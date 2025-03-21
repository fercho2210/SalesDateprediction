namespace SalesDatePrediction.Dto
{
    public class CustomerOrderPredictionDto
    {
        public string CustomerName { get; set; }=string.Empty;
        public DateTime LastOrderDate { get; set; }
        public DateTime NextPredictedOrder { get; set; }
        public int CustId { get; internal set; }
        public DateTime PredictedOrderDate { get; internal set; }
        public int CustomerId { get;  set; }
    }
}
