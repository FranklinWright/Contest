using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Contest.Data;
using Contest.Shared;

namespace Contest.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClassUsersController : MyController
    {
        private readonly ApplicationDbContext _context;

        public ClassUsersController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/ClassUsers
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ClassUser>>> GetClassUser()
        {
            var userId = GetUserId();
            return await _context.ClassUser.Where(cu => cu.UserId == userId).ToListAsync();
        }

        // GET: api/ClassUsers/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ClassUser>> GetClassUser(int id)
        {
            var classUser = await _context.ClassUser.FindAsync(id);

            if (classUser == null)
            {
                return NotFound();
            }

            return classUser;
        }

        // PUT: api/ClassUsers/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutClassUser(int id, ClassUser classUser)
        {
            if (id != classUser.ClassUserId)
            {
                return BadRequest();
            }

            _context.Entry(classUser).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ClassUserExists(id))
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

        // POST: api/ClassUsers
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<ClassUser>> PostClassUser(ClassUser classUser)
        {
            _context.ClassUser.Add(classUser);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetClassUser", new { id = classUser.ClassUserId }, classUser);
        }

        // DELETE: api/ClassUsers/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteClassUser(int id)
        {
            var classUser = await _context.ClassUser.FindAsync(id);
            if (classUser == null)
            {
                return NotFound();
            }

            _context.ClassUser.Remove(classUser);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ClassUserExists(int id)
        {
            return _context.ClassUser.Any(e => e.ClassUserId == id);
        }
    }
}
