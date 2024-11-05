using Microsoft.AspNetCore.Mvc;
using MassTransit;
using System.Threading.Tasks;
using Events;

[ApiController]
[Route("api/[controller]")]
public class TestController : ControllerBase
{
    private readonly IPublishEndpoint _publishEndpoint;

    public TestController(IPublishEndpoint publishEndpoint)
    {
        _publishEndpoint = publishEndpoint;
    }

    [HttpPost("trigger-low-stock")]
    public async Task<IActionResult> TriggerLowStock(int productId, int quantity)
    {
        await _publishEndpoint.Publish<ILowStockEvent>(new
        {
            ProductId = productId,
            Quantity = quantity
        }, context =>
        {
            context.SetRoutingKey("low_stock"); // Set the routing key for the direct exchange
        });

        return Ok("Low stock event triggered");
    }
}
