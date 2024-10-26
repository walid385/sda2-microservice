using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace SalesService.Services
{
    public class InventoryClient
    {
        private readonly HttpClient _httpClient;

        public InventoryClient(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<bool> CheckProductAvailability(int productId, int quantity)
        {
            var url = $"http://inventory-service/api/inventory/{productId}/availability?quantity={quantity}";
            var response = await _httpClient.GetAsync(url);
            response.EnsureSuccessStatusCode();
            return JsonSerializer.Deserialize<bool>(await response.Content.ReadAsStringAsync());
        }
    }
}