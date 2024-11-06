using System.Text.Json;


namespace CustomerService.Services
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
            if (string.IsNullOrEmpty(state))
            {
                state = "DEFAULT";
            }

            var url = $"http://tax-service/api/Tax/rate/{state}";
            var response = await _httpClient.GetAsync(url);

            if (!response.IsSuccessStatusCode)
            {
                Console.WriteLine($"Failed to retrieve tax rate for state: {state}. Using default rate.");
                url = $"http://tax-service/api/Tax/rate/DEFAULT";
                response = await _httpClient.GetAsync(url);

                if (!response.IsSuccessStatusCode)
                {
                    Console.WriteLine("Failed to retrieve default tax rate.");
                    return null;
                }
            }

            var content = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<TaxRateDto>(content);
        }
    }

    // DTO for the tax rate response
    public class TaxRateDto
    {
        public float StateTax { get; set; }
        public float CountyTax { get; set; }
        public float CityRate { get; set; }
        public float TotalTaxRate { get; set; } 
        public string State { get; set; }
    }
}
