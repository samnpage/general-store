using GeneralStore.WebApi.Data;
using GeneralStore.WebApi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GeneralStore.WebApi.Controllers;

[ApiController]
[Route("[controller]")]
// allows us to reach into the database.
public class ProductController : ControllerBase
{
    private readonly GeneralStoreDbContext _db;

    // Constructor
    public ProductController(GeneralStoreDbContext db)
    {
        _db = db;
    }

    [HttpGet]
    public async Task<IActionResult> GetAllProductsAsync()
    {
        var products = await _db.Products.ToListAsync();
        return Ok(products);
    }

    [HttpPost]
    public async Task<IActionResult> CreateProductAsync([FromBody] ProductRequest req)
    {
        Product newProduct = new()
        {
            Name = req.Name,
            Price = req.Price,
            QuantityInStock = req.Quantity
        };

        // adds new product to database
        _db.Products.Add(newProduct);
        await _db.SaveChangesAsync();

        return Ok(newProduct);
    }
}   