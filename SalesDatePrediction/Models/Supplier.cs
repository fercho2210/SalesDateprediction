﻿namespace SalesDatePrediction.Models
{

    public class Supplier
    {
        public int Supplierid { get; set; }
        public string CompanyName { get; set; }= string.Empty;
        public string ContactName { get; set; } = string.Empty;
        public string ContactTitle { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;
        public string City { get; set; } = string.Empty;
        public string Region { get; set; } = string.Empty;
        public string Postalcode { get; set; } = string.Empty;
        public string Country { get; set; } = string.Empty;
        public string Phone { get; set; } = string.Empty;
        public string Fax { get; set; } = string.Empty;
    }

}
