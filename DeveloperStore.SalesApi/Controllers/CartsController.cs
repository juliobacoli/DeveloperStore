using DeveloperStore.SalesApi.Data;
using DeveloperStore.SalesApi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DeveloperStore.SalesApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CartsController : ControllerBase
{
    private readonly ApplicationDbContext _context;

    public CartsController(ApplicationDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<IActionResult> GetCarts()
    {
        var carts = await _context.Carts
            .Include(c => c.Products)
            .ThenInclude(ci => ci.Product)
            .ToListAsync();
        return Ok(carts);
    }

    [HttpPost]
    public async Task<IActionResult> CreateCart([FromBody] Cart cart)
    {
        cart.Date = DateTime.UtcNow;
        _context.Carts.Add(cart);
        await _context.SaveChangesAsync();
        return CreatedAtAction(nameof(GetCarts), new { id = cart.Id }, cart);
    }
}
