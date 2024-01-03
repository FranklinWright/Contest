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
    public class AccountTypesController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public AccountTypesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/AccountTypes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<AccountType>>> GetAccountType()
        {
            return await _context.AccountType.ToListAsync();
        }

        // GET: api/AccountTypes/5
        [HttpGet("{id}")]
        public async Task<ActionResult<AccountType>> GetAccountType(int id)
        {
            var accountType = await _context.AccountType.FindAsync(id);

            if (accountType == null)
            {
                return NotFound();
            }

            return accountType;
        }

        // PUT: api/AccountTypes/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAccountType(int id, AccountType accountType)
        {
            if (id != accountType.AccountTypeId)
            {
                return BadRequest();
            }

            _context.Entry(accountType).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AccountTypeExists(id))
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

        // POST: api/AccountTypes
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<AccountType>> PostAccountType(AccountType accountType)
        {
            _context.AccountType.Add(accountType);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetAccountType", new { id = accountType.AccountTypeId }, accountType);
        }

        // DELETE: api/AccountTypes/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAccountType(int id)
        {
            var accountType = await _context.AccountType.FindAsync(id);
            if (accountType == null)
            {
                return NotFound();
            }

            _context.AccountType.Remove(accountType);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool AccountTypeExists(int id)
        {
            return _context.AccountType.Any(e => e.AccountTypeId == id);
        }
    }
}
