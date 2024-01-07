using Contest.Data;
using Contest.Shared;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Contest.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClassesController(ApplicationDbContext context) : MyController
    {
        private readonly ApplicationDbContext _context = context;

        // GET: api/Classes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Class>>> GetClass()
        {
            var role = GetAccountType(_context);

            if (role == "Teacher")
            {
                var userId = GetUserId();
                return await _context.Class.Where(c => c.UserId == userId).ToListAsync();
            }
            else
            {
                return await _context.Class.ToListAsync();
            }
        }

        // GET: api/Classes/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Class>> GetClass(int id)
        {
            var @class = await _context.Class.FindAsync(id);

            if (@class == null)
            {
                return NotFound();
            }

            return @class;
        }

        // PUT: api/Classes/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutClass(int id, Class @class)
        {
            if (id != @class.ClassId)
            {
                return BadRequest();
            }

            var userId = GetUserId();

            if (userId == null)
            {
                return Unauthorized();
            }
            else
            {
                @class.UserId = (Guid)userId;
            }

            _context.Entry(@class).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ClassExists(id))
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

        // POST: api/Classes
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Class>> PostClass(Class @class)
        {
            var userId = GetUserId();

            if (userId == null)
            {
                return Unauthorized();
            }
            else
            {
                @class.UserId = (Guid)userId;
            }

            _context.Class.Add(@class);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetClass", new { id = @class.ClassId }, @class);
        }

        // DELETE: api/Classes/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteClass(int id)
        {
            var @class = await _context.Class.FindAsync(id);
            if (@class == null)
            {
                return NotFound();
            }

            _context.Class.Remove(@class);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ClassExists(int id)
        {
            return _context.Class.Any(e => e.ClassId == id);
        }
    }
}
