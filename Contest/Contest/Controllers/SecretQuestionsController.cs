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
    public class SecretQuestionsController : MyController
    {
        private readonly ApplicationDbContext _context;

        public SecretQuestionsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/SecretQuestions
        [HttpGet]
        public async Task<ActionResult<IEnumerable<SecretQuestion>>> GetSecretQuestion()
        {
            return await _context.SecretQuestion.ToListAsync();
        }

        // GET: api/SecretQuestions/5
        [HttpGet("{id}")]
        public async Task<ActionResult<SecretQuestion>> GetSecretQuestion(int id)
        {
            var secretQuestion = await _context.SecretQuestion.FindAsync(id);

            if (secretQuestion == null)
            {
                return NotFound();
            }

            return secretQuestion;
        }

        // PUT: api/SecretQuestions/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutSecretQuestion(int id, SecretQuestion secretQuestion)
        {
            if (id != secretQuestion.SecretQuestionId)
            {
                return BadRequest();
            }

            _context.Entry(secretQuestion).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SecretQuestionExists(id))
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

        // POST: api/SecretQuestions
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<SecretQuestion>> PostSecretQuestion(SecretQuestion secretQuestion)
        {
            _context.SecretQuestion.Add(secretQuestion);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetSecretQuestion", new { id = secretQuestion.SecretQuestionId }, secretQuestion);
        }

        // DELETE: api/SecretQuestions/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSecretQuestion(int id)
        {
            var secretQuestion = await _context.SecretQuestion.FindAsync(id);
            if (secretQuestion == null)
            {
                return NotFound();
            }

            _context.SecretQuestion.Remove(secretQuestion);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool SecretQuestionExists(int id)
        {
            return _context.SecretQuestion.Any(e => e.SecretQuestionId == id);
        }
    }
}
