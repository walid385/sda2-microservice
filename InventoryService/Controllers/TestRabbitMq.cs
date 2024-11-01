using Microsoft.AspNetCore.Mvc;
using MassTransit;
using System.Threading.Tasks;
using InventoryService.Events;


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
        var lowStockEvent = new LowStockEvent
        {
            ProductId = productId,
            Quantity = quantity
        };
        
        await _publishEndpoint.Publish(lowStockEvent);
        return Ok("Low stock event triggered");
    }
}
