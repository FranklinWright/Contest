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
    public class ClassTutorialsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public ClassTutorialsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/ClassTutorials
        [HttpGet]
        public async Task<ActionResult<List<ClassTutorialResponse>>> GetClassTutorial(
            [FromQuery] int classId,
            [FromQuery] int tutorialId
            )
        {
            var query = from ct in _context.ClassTutorial
                        join t in _context.Tutorial on ct.TutorialId equals t.TutorialId
                        where ct.ClassId == classId
                        select new ClassTutorialResponse
                        {
                            ClassTutorialId = ct.ClassTutorialId,
                            ClassId = ct.ClassId,
                            TutorialId = ct.TutorialId,
                            Tutorial = t
                        };

            return Ok(await query.ToListAsync());
        }

        // GET: api/ClassTutorials/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ClassTutorial>> GetClassTutorial(int id)
        {
            var classTutorial = await _context.ClassTutorial.FindAsync(id);

            if (classTutorial == null)
            {
                return NotFound();
            }

            return classTutorial;
        }

        // PUT: api/ClassTutorials/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutClassTutorial(int id, ClassTutorial classTutorial)
        {
            if (id != classTutorial.ClassTutorialId)
            {
                return BadRequest();
            }

            _context.Entry(classTutorial).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ClassTutorialExists(id))
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

        // POST: api/ClassTutorials
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<ClassTutorial>> PostClassTutorial(ClassTutorial classTutorial)
        {
            _context.ClassTutorial.Add(classTutorial);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetClassTutorial", new { id = classTutorial.ClassTutorialId }, classTutorial);
        }

        // DELETE: api/ClassTutorials/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteClassTutorial(int id)
        {
            var classTutorial = await _context.ClassTutorial.FindAsync(id);
            if (classTutorial == null)
            {
                return NotFound();
            }

            _context.ClassTutorial.Remove(classTutorial);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ClassTutorialExists(int id)
        {
            return _context.ClassTutorial.Any(e => e.ClassTutorialId == id);
        }
    }
}
