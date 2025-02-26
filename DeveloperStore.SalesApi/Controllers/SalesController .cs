using DeveloperStore.SalesApi.Data;
using DeveloperStore.SalesApi.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DeveloperStore.SalesApi.Controllers;

[ApiController]
[Route("api/[controller]")]
//[Authorize]
public class SalesController : ControllerBase
{
    private readonly ApplicationDbContext _context;

    public SalesController(ApplicationDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<IActionResult> GetSales()
    {
        var sales = await _context.Sales
            .Include(s => s.Items)
            .ThenInclude(i => i.Product)
            .ToListAsync();
        return Ok(sales);
    }

    [HttpPost]
    public async Task<IActionResult> CreateSale([FromBody] Sale sale)
    {
        if (sale.Items.Any(i => i.Quantity > 20))
            return BadRequest("Não é permitido mais de 20 itens do mesmo produto.");

        foreach (var item in sale.Items)
        {
            item.TotalPrice = item.Quantity * item.UnitPrice;
            item.Discount = item.Quantity >= 10 ? item.TotalPrice * 0.10m : 0;
            item.TotalPrice -= item.Discount;
        }

        sale.TotalAmount = sale.Items.Sum(i => i.TotalPrice);
        sale.Date = DateTime.UtcNow;

        _context.Sales.Add(sale);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetSales), new { id = sale.Id }, sale);
    }
}