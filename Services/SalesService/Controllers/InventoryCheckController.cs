using Microsoft.AspNetCore.Mvc;
using SalesService.Services;
using System.Threading.Tasks;

[ApiController]
[Route("api/[controller]")]
public class InventoryCheckController : ControllerBase
{
    private readonly InventoryClient _inventoryClient;

    public InventoryCheckController(InventoryClient inventoryClient)
    {
        _inventoryClient = inventoryClient;
    }

    [HttpGet("check-availability")]
    public async Task<ActionResult<bool>> CheckAvailability(int productId, int quantity)
    {
        var isAvailable = await _inventoryClient.CheckProductAvailability(productId, quantity);
        return Ok(isAvailable);
    }
}
