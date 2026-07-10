using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TableServe.Api.Data;
using TableServe.Api.Models;

namespace TableServe.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MenuItemsController : ControllerBase
    {
        private readonly TableServeDbContext _db;

        public MenuItemsController(TableServeDbContext db)
        {
            _db = db;
        }

        // GET: api/menuitems
        [HttpGet]
        public async Task<ActionResult<IEnumerable<MenuItem>>> GetAll()
        {
            return await _db.MenuItems.ToListAsync();
        }

        // GET: api/menuitems/5
        [HttpGet("{id}")]
        public async Task<ActionResult<MenuItem>> GetById(int id)
        {
            var menuItem = await _db.MenuItems.FindAsync(id);
            if (menuItem == null) return NotFound();
            return menuItem;
        }

        // POST: api/menuitems
        [HttpPost]
        public async Task<ActionResult<MenuItem>> Create(MenuItem menuItem)
        {
            _db.MenuItems.Add(menuItem);
            await _db.SaveChangesAsync();

            return CreatedAtAction(nameof(GetById), new { id = menuItem.Id }, menuItem);
        }

        // PUT: api/menuitems/5
        [HttpPut("{id}")]
        public async Task<ActionResult<MenuItem>> Update(int id, MenuItem updatedMenuItem)
        {
            if (id != updatedMenuItem.Id)
            {
                return BadRequest();
            }

            var currentMenuItem = await _db.MenuItems.FindAsync(id);
            if (currentMenuItem == null)
            {
                return NotFound();
            }

            _db.Entry(currentMenuItem).CurrentValues.SetValues(updatedMenuItem);
            await _db.SaveChangesAsync();

            return Ok(currentMenuItem);
        }

        // DELETE: api/menuitems/5
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var menuItem = await _db.MenuItems.FindAsync(id);
            if (menuItem == null) return NotFound();

            _db.MenuItems.Remove(menuItem);
            await _db.SaveChangesAsync();

            return NoContent();
        }

       
    }
}
