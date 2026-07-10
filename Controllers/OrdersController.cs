using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TableServe.Api.Data;
using TableServe.Api.Models;

namespace TableServe.Api.Controllers
{
    

    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly TableServeDbContext _db;

        public OrdersController(TableServeDbContext db)
        {
            _db = db;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Order>>> GetAll([FromQuery] string? status = null)
        {
            var query = _db.Orders.Include(order => order.Staff).AsQueryable();
            if (status != null) query = query.Where(order => order.Status == status);
            return await query.ToListAsync();
        }

        // GET: api/orders/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Order>> GetById([FromRoute] int id)
        {
            var order = await _db.Orders.Include(order => order.Staff)
                                        .Include(order => order.OrderItems)
                                        .ThenInclude(orderItem => orderItem.MenuItem).SingleOrDefaultAsync((order)=> order.Id == id);

            if (order == null) return NotFound();
            return order;
        }

        // POST: api/orders
        [HttpPost]
        public async Task<ActionResult<Order>> Create([FromBody] Order order)
        {
            _db.Orders.Add(order);
            await _db.SaveChangesAsync();

            return CreatedAtAction(nameof(GetById), new { id = order.Id }, order);
        }

        // PUT: api/orders/5
        [HttpPut("{id}")]
        public async Task<ActionResult<Order>> Update(int id, Order updatedOrder)
        {
            if (id != updatedOrder.Id)
            {
                return BadRequest();
            }

            var currentOrder = await _db.Orders.FindAsync(id);
            if (currentOrder == null)
            {
                return NotFound();
            }

            _db.Entry(currentOrder).CurrentValues.SetValues(updatedOrder);
            await _db.SaveChangesAsync();

            return Ok(currentOrder);
        }

        // DELETE: api/orders/5
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var order = await _db.Orders.FindAsync(id);
            if (order == null) return NotFound();

            _db.Orders.Remove(order);
            await _db.SaveChangesAsync();

            return NoContent();
        }

    }
}
