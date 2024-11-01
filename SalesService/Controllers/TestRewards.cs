using Microsoft.AspNetCore.Mvc;
using MassTransit;
using System.Threading.Tasks;
using SalesService.Events;

namespace SalesService.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TestRewardsController : ControllerBase
    {
        private readonly IPublishEndpoint _publishEndpoint;

        public TestRewardsController(IPublishEndpoint publishEndpoint)
        {
            _publishEndpoint = publishEndpoint;
        }

        [HttpPost("test-rabbitmq")]
        public async Task<IActionResult> TestRabbitMQ()
        {
            var testEvent = new UpdateCustomerRewardsEvent
            {
                CustomerId = 1,
                Points = 10
            };
            await _publishEndpoint.Publish(testEvent);
            return Ok("Test message published to RabbitMQ");
        }
    }
}
