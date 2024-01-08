using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Contest.Data;
using Contest.Shared;
using Contest.Migrations;

namespace Contest.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentTutorialsController : MyController
    {
        private readonly ApplicationDbContext _context;

        public StudentTutorialsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/StudentTutorials
        [HttpGet]
        public async Task<ActionResult<IEnumerable<StudentTutorial>>> GetStudentTutorial()
        {
            var userId = GetUserId();
            return await _context.StudentTutorial.Where(c => c.UserId == userId).ToListAsync();
        }

        // GET: api/StudentTutorials/5
        [HttpGet("{id}")]
        public async Task<ActionResult<StudentTutorial>> GetStudentTutorial(int id)
        {
            var studentTutorial = await _context.StudentTutorial.FindAsync(id);

            if (studentTutorial == null)
            {
                return NotFound();
            }

            return studentTutorial;
        }

        // PUT: api/StudentTutorials/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutStudentTutorial(int id, StudentTutorial studentTutorial)
        {
            if (id != studentTutorial.StudentTutorialId)
            {
                return BadRequest();
            }

            _context.Entry(studentTutorial).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!StudentTutorialExists(id))
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

        // POST: api/StudentTutorials
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<StudentTutorial>> PostStudentTutorial(StudentTutorial studentTutorial)
        {
            var userId = GetUserId();

            if (userId == null)
            {
                return Unauthorized();
            }
            else
            {
                @studentTutorial.UserId = (Guid)userId;
            }

            _context.StudentTutorial.Add(studentTutorial);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetStudentTutorial", new { id = studentTutorial.StudentTutorialId }, studentTutorial);
        }

        // DELETE: api/StudentTutorials/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteStudentTutorial(int id)
        {
            var studentTutorial = await _context.StudentTutorial.FindAsync(id);
            if (studentTutorial == null)
            {
                return NotFound();
            }

            _context.StudentTutorial.Remove(studentTutorial);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool StudentTutorialExists(int id)
        {
            return _context.StudentTutorial.Any(e => e.StudentTutorialId == id);
        }
    }
}
