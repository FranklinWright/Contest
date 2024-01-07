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
    public class TutorialsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public TutorialsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/Tutorials
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Tutorial>>> GetTutorial()
        {
            return await _context.Tutorial.OrderBy(t => t.Title).ToListAsync();
        }

        // GET: api/Tutorials/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Tutorial>> GetTutorial(int id)
        {
            var tutorial = await _context.Tutorial.FindAsync(id);

            if (tutorial == null)
            {
                return NotFound();
            }

            return tutorial;
        }

        // PUT: api/Tutorials/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTutorial(int id, Tutorial tutorial)
        {
            if (id != tutorial.TutorialId)
            {
                return BadRequest();
            }

            _context.Entry(tutorial).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TutorialExists(id))
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

        // POST: api/Tutorials
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Tutorial>> PostTutorial(Tutorial tutorial)
        {
            _context.Tutorial.Add(tutorial);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetTutorial", new { id = tutorial.TutorialId }, tutorial);
        }

        // DELETE: api/Tutorials/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTutorial(int id)
        {
            var tutorial = await _context.Tutorial.FindAsync(id);
            if (tutorial == null)
            {
                return NotFound();
            }

            _context.Tutorial.Remove(tutorial);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool TutorialExists(int id)
        {
            return _context.Tutorial.Any(e => e.TutorialId == id);
        }
    }
}
