using System.Net.Http;
using System.Text.Json;
using System.Text;
using System.Threading.Tasks;
using SalesService.DTOs;

namespace SalesService.Services
{
    public class TaxClient
    {
        private readonly HttpClient _httpClient;

        public TaxClient(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<TaxRateDto> GetTaxRateByStateAsync(string state)
        {
            var url = $"http://tax-service/api/tax/rate/{state}";
            var response = await _httpClient.GetAsync(url);

            if (!response.IsSuccessStatusCode)
            {
                Console.WriteLine($"Failed to retrieve tax rate for state: {state}. Using default rate.");
                url = $"http://tax-service/api/tax/rate/DEFAULT";
                response = await _httpClient.GetAsync(url);

                if (!response.IsSuccessStatusCode)
                {
                    Console.WriteLine("Failed to retrieve default tax rate.");
                    return null;
                }
            }

            var content = await response.Content.ReadAsStringAsync();

            // Configure the JsonSerializerOptions to be case-insensitive
            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };

            // Deserialize the JSON content into TaxRateDto
            var taxRateDto = JsonSerializer.Deserialize<TaxRateDto>(content, options);

            // Log the deserialized tax rate for debugging
            Console.WriteLine($"Deserialized TaxRateDto: TotalTaxRate={taxRateDto.TotalTaxRate}, StateTax={taxRateDto.StateTax}, CountyTax={taxRateDto.CountyTax}, CityRate={taxRateDto.CityRate}");

            return taxRateDto;
        }




    }

    public class TaxCalculationDto
    {
        public float Amount { get; set; }
        public string Region { get; set; }
    }

    public class TaxRateDto
    {
        public float TaxAmount { get; set; }
        public float TotalAmount { get; set; }
        public float StateTax { get; set; }
        public float CountyTax { get; set; }
        public float CityRate { get; set; }
        public float TotalTaxRate { get; set; }
        public float TaxRates { get; set; }
    }
}
