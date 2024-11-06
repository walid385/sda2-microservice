namespace TaxService.DTOs
{
    public class TaxCalculationDto
    {
        public float Amount { get; set; } // Pre-tax amount
        public string Region { get; set; } // Region information if needed
        public string ProductType { get; set; } // Product type if applicable
    }

    public class TaxResultDto
    {
        public float TaxAmount { get; set; }
        public float TotalAmount { get; set; } // Amount + TaxAmount
        public float TaxRates { get; set; } // Total tax rate
    }
}
