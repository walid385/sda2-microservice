using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using OrderManagementService.DTOs;

namespace OrderManagementService.Services
{
    public class InventoryClient
    {
        private readonly HttpClient _httpClient;

        public InventoryClient(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<ProductDto> GetProductById(int productId)
        {
            var url = $"http://inventory-service/api/inventory/{productId}";
            var response = await _httpClient.GetAsync(url);
            response.EnsureSuccessStatusCode();

            var content = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<ProductDto>(content);
        }

        public async Task<bool> CheckProductAvailability(int productId, int quantity)
        {
            var url = $"http://inventory-service/api/inventory/{productId}/availability?quantity={quantity}";
            var response = await _httpClient.GetAsync(url);
            response.EnsureSuccessStatusCode();

            var content = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<bool>(content);
        }
    }
}
