using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using CustomerService.DTOs;

namespace CustomerService.Services
{
    public class OrderManagementClient
    {
        private readonly HttpClient _httpClient;

        public OrderManagementClient(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<bool> CreateOrder(CreateOrderDto orderDto)
        {
            var url = "http://order-service/api/orders"; // Construct the URL here, similar to InventoryClient
            var response = await _httpClient.PostAsJsonAsync(url, orderDto);

            return response.IsSuccessStatusCode;
        }
    }
}
