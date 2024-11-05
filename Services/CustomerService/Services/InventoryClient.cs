using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using System.Text.Json.Serialization;


namespace CustomerService.Services
{
    public class InventoryClient
    {
        private readonly HttpClient _httpClient;

        public InventoryClient(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<decimal?> GetProductPrice(int productId)
        {
             var url = $"http://inventory-service/api/inventory/{productId}";
            var response = await _httpClient.GetAsync(url);

            if (!response.IsSuccessStatusCode)
            {
                Console.WriteLine($"Failed to retrieve price for ProductId: {productId}");
                return null;
            }

            var content = await response.Content.ReadAsStringAsync();
            Console.WriteLine($"Response content for ProductId {productId}: {content}");

            try
            {
                var product = JsonSerializer.Deserialize<ProductDto>(content);
                Console.WriteLine($"Deserialized UnitPrice for ProductId {productId}: {product?.UnitPrice}");
                return product?.UnitPrice;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error deserializing ProductDto for ProductId {productId}: {ex.Message}");
                return null;
            }
        }


    }

    // DTO for InventoryService response
    public class ProductDto
    {
        public int ProductId { get; set; }

        [JsonPropertyName("unitPrice")]
        public decimal UnitPrice { get; set; }
    }
}
