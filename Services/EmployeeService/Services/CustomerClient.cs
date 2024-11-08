using System;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using EmployeeService.Models;

namespace EmployeeService.Services
{
    public class CustomerClient
    {
        private readonly HttpClient _httpClient;

        public CustomerClient(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<CustomerInfoDto?> GetCustomerInfo(int customerId)
        {
            var url = $"http://customer-service/api/customers/{customerId}";
            var response = await _httpClient.GetAsync(url);

            if (!response.IsSuccessStatusCode)
            {
                Console.WriteLine($"Failed to retrieve customer info for CustomerId: {customerId}");
                return null;
            }

            var content = await response.Content.ReadAsStringAsync();
            Console.WriteLine($"Raw customer data for CustomerId {customerId}: {content}");

            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };

            var customerInfo = JsonSerializer.Deserialize<CustomerInfoDto>(content, options);
            Console.WriteLine($"Deserialized Customer Info - ID: {customerInfo?.CustomerId}, State: '{customerInfo?.State}', Name: {customerInfo?.FirstName} {customerInfo?.LastName}");

            return customerInfo;
        }
    }

    // DTO for CustomerService response
    public class CustomerInfoDto
    {
        public int CustomerId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }
        public string State { get; set; }
    }
}
