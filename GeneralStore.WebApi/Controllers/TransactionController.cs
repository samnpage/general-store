using GeneralStore.WebApi.Data;
using GeneralStore.WebApi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GeneralStore.WebApi.Controllers;

[ApiController]
[Route("[controller]")]
public class TransactionController : ControllerBase
{
    private readonly GeneralStoreDbContext _db;

    public TransactionController(GeneralStoreDbContext db)
    {
        _db = db;
    }

    [HttpGet]
    public async Task<IActionResult> GetAllTransactionsAsync()
    {
        var transactions = await _db.Transactions.ToListAsync();
        return Ok(transactions);
    }

    [HttpPost]
    public async Task<IActionResult> CreateTransactionAsync([FromBody] TransactionCreate transaction)
    {
        Transaction newTransaction = new()
        {
            ProductId = transaction.ProductId,
            CustomerId = transaction.CustomerId,
            Quantity = transaction.Quantity,
            DateOfTransaction = DateTime.Now
        };

        _db.Transactions.Add(newTransaction);
        await _db.SaveChangesAsync();

        return Ok(transaction);
    }
}