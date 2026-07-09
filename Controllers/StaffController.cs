using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TableServe.Api.Data;
using TableServe.Api.Models;

namespace TableServe.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StaffController : ControllerBase
    {
        private TableServeDbContext _db;

        public StaffController(TableServeDbContext db)
        {
            _db = db;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Staff>>> GetAll()
        {
            return await _db.Staff.ToListAsync();
        }

        // GET: api/staff/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Staff>> GetById(int id)
        {
            var staff = await _db.Staff.FindAsync(id);
            if (staff == null) return NotFound();
            return staff;
        }
    }
}
