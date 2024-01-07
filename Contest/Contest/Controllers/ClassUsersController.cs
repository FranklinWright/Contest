using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Contest.Data;
using Contest.Shared;
using System.Security.Principal;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;


namespace Contest.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClassUsersController(ApplicationDbContext context) : MyController
    {
        private readonly ApplicationDbContext _context = context;

        // GET: api/ClassUsers
        [HttpGet]
        public async Task<ActionResult<List<ClassUserResponse>>> GetClassUser(
            [FromQuery] int classId,
            [FromQuery] Guid userId
            )
        {
            if (classId != 0)
            {
                var result = new List<ClassUserResponse>();
                var classUserResponse = new ClassUserResponse();

                var selectedClass = await _context.Class.FindAsync(classId);
                if (selectedClass == null)
                {
                    return NotFound();
                };

                classUserResponse.ClassId = selectedClass.ClassId;
                classUserResponse.ClassName = selectedClass.ClassName!;
                classUserResponse.Students = new List<Student>();

                var selectedClassUsers = await _context.ClassUser.Where(cu => cu.ClassId == classId).ToListAsync();
                foreach (var classUser in selectedClassUsers)
                {
                    var user = await _context.Users.FindAsync(classUser.UserId.ToString());
                    if (user == null)
                    {
                        return NotFound();
                    };

                    classUserResponse.Students.Add(new Student
                    {
                        UserId = Guid.Parse(user.Id),
                        FirstName = user.FirstName,
                        LastName = user.LastName
                    });
                }

                result.Add(classUserResponse);
                return Ok(result);
            }
            else if (userId != Guid.Empty)
            {
                var query = from cu in _context.ClassUser
                            join c in _context.Class on cu.ClassId equals c.ClassId
                            join u in _context.Users on cu.UserId.ToString() equals u.Id
                            where cu.UserId == userId
                            select new ClassUserResponse
                            {
                                ClassId = c.ClassId,
                                ClassName = c.ClassName!
                            };

                return Ok(await query.ToListAsync());
            }
            else
            {
                // return a list of ClassUserResponse
                var query = from cu in _context.ClassUser
                            join c in _context.Class on cu.ClassId equals c.ClassId
                            join u in _context.Users on cu.UserId.ToString() equals u.Id
                            select new ClassUserResponse
                            {
                                ClassId = c.ClassId,
                                ClassName = c.ClassName!
                            };
                return Ok(await query.ToListAsync());
            }
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
            var userId = GetUserId();

            if (userId == null)
            {
                return Unauthorized();
            }
            else
            {
                @classUser.UserId = (Guid)userId;
            }

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
