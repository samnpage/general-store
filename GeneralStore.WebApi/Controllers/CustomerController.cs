using GeneralStore.WebApi.Data;
using GeneralStore.WebApi.Models;
using Microsoft.AspNetCore.Authorization.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GeneralStore.WebApi.Controllers;

[ApiController]
[Route("[controller]")]
public class CustomerController : ControllerBase
{
    private readonly GeneralStoreDbContext _db;

    public CustomerController(GeneralStoreDbContext db)
    {
        _db = db;
    }

    [HttpGet]
    public async Task<IActionResult> GetAllCustomersAsync()
    {
        var customers = await _db.Customers.ToListAsync();
        return Ok(customers);
    }

    [HttpPost]
    public async Task<IActionResult> CreateCustomerAsync([FromForm] CustomerEdit newCustomer)
    {
        Customer customer = new()
        {
            Name = newCustomer.Name,
            Email = newCustomer.Email
        };

        _db.Customers.Add(customer);
        await _db.SaveChangesAsync();

        return Ok(newCustomer);
    }
}