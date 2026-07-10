using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TableServe.Api.Data;
using TableServe.Api.Models;

namespace TableServe.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderItemsController : ControllerBase
    {
        private readonly TableServeDbContext _db;

        public OrderItemsController(TableServeDbContext db)
        {
            _db = db;
        }

        // GET: api/orderitems
        [HttpGet]
        public async Task<ActionResult<IEnumerable<OrderItem>>> GetAll()
        {
            return await _db.OrderItems.Include(item=> item.MenuItem).ToListAsync();
        }

        // GET: api/orderitems/5
        [HttpGet("{id}")]
        public async Task<ActionResult<OrderItem>> GetById(int id)
        {
            var orderItem = await _db.OrderItems.Include(item => item.MenuItem).SingleOrDefaultAsync(item => item.Id == id);
            if (orderItem == null) return NotFound();
            return orderItem;
        }

        // POST: api/orderitems
        [HttpPost]
        public async Task<ActionResult<OrderItem>> Create(OrderItem orderItem)
        {
            _db.OrderItems.Add(orderItem);
            await _db.SaveChangesAsync();
            var orderWithMenuItem = await _db.OrderItems.Include(item => item.MenuItem).SingleOrDefaultAsync(item => item.Id == orderItem.Id);

            return CreatedAtAction(nameof(GetById), new { id = orderWithMenuItem.Id }, orderWithMenuItem);
        }

        // PUT: api/orderitems/5
        [HttpPut("{id}")]
        public async Task<ActionResult<OrderItem>> Update(int id, OrderItem orderItem)
        {
            if (id != orderItem.Id) return BadRequest();

            var existing = await _db.OrderItems.FindAsync(id);
            if (existing == null) return NotFound();

            _db.Entry(existing).CurrentValues.SetValues(orderItem);

            await _db.SaveChangesAsync();

            return Ok(existing);
        }

        // DELETE: api/orderitems/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var orderItem = await _db.OrderItems.FindAsync(id);
            if (orderItem == null) return NotFound();

            _db.OrderItems.Remove(orderItem);
            await _db.SaveChangesAsync();

            return NoContent();
        }
    }
}