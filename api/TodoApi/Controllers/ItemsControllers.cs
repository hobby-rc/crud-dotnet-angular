using Data;
using Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
namespace csharp_crud_api.Controllers;


[ApiController]
[Route("api/[controller]")]
public class ItemsController : ControllerBase
{
  private readonly ItemContext _context;

  public ItemsController(ItemContext context)
  {
    _context = context;
  }

  // GET: api/items
  [HttpGet]
  public async Task<ActionResult<IEnumerable<Item>>> GetItems()
  {
    return await _context.Items.ToListAsync();
  }

  // GET: api/items/5
  [HttpGet("{id}")]
  public async Task<ActionResult<Item>> GetItem(int id)
  {
    var item = await _context.Items.FindAsync(id);

    if (item == null)
    {
      return NotFound();
    }

    return item;
  }

  // POST: api/items
  [HttpPost]
  public async Task<ActionResult<Item>> PostItem(Item item)
  {
    _context.Items.Add(item);
    await _context.SaveChangesAsync();

    return CreatedAtAction(nameof(GetItem), new { id = item.Id }, item);
  }

  // PUT: api/items/5
  [HttpPut("{id}")]
  public async Task<IActionResult> PutItem(int id, Item item)
  {
    if (id != item.Id)
    {
      return BadRequest();
    }

    _context.Entry(item).State = EntityState.Modified;

    try
    {
      await _context.SaveChangesAsync();
    }
    catch (DbUpdateConcurrencyException)
    {
      if (!ItemExists(id))
      {
        return NotFound();
      }
      else
      {
        throw;
      }
    }

    return NoContent();
  }

  // DELETE: api/items/5
  [HttpDelete("{id}")]
  public async Task<IActionResult> DeleteItem(int id)
  {
    var item = await _context.Items.FindAsync(id);
    if (item == null)
    {
      return NotFound();
    }

    _context.Items.Remove(item);
    await _context.SaveChangesAsync();

    return NoContent();
  }

  private bool ItemExists(int id)
  {
    return _context.Items.Any(e => e.Id == id);
  }

  // dummy method to test the connection
  [HttpGet("hello")]
  public string Test()
  {
    return "Hello World!";
  }
}