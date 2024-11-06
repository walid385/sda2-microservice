using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace TaxService.DTOs
{
    public class TaxRateDto
    {
        [Key]
        public int TTID { get; set; }  // Tax Table ID

        public int TaxYear { get; set; }

        public float StateTax { get; set; }

        public float CountyTax { get; set; }

        public float CityRate { get; set; }

        public float TaxRates { get; set; }  // Total tax rate, e.g., 0.08 for 8%

        [JsonInclude]
        public float TotalTaxRate => StateTax + CountyTax + CityRate;  // Combined tax rate

        public string State { get; set; }
    }
}
