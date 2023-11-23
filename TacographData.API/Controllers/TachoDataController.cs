using TacographData.API.Data;
using TacographData.API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace TacographData.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TachoDatasController :ControllerBase
    {
        private readonly TachoDataContext _context;

        public TachoDatasController(TachoDataContext context)
        {
            _context = context;
        }

        // GET: api/tachodatas
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TachoData>>> GetTachoDatas()
        {
            return await _context.TachoDatas.ToListAsync();
        }

        // GET: api/tachodatas/5
        [HttpGet("{id}")]
        public async Task<ActionResult<TachoData>> GetTachoData(int id)
        {
            var tdata = await _context.TachoDatas.FindAsync(id);

            if (tdata == null)
            {
                return NotFound();
            }

            return tdata;
        }

        // POST: api/tachodatas
        [HttpPost]
        public async Task<ActionResult<TachoData>> PostTachoData(TachoData user)
        {
            _context.TachoDatas.Add(user);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetTachoData), new { id = user.TrackId }, user);
        }

        // PUT: api/tachodatas/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTachoData(int id, TachoData user)
        {
            if (id != user.TrackId)
            {
                return BadRequest();
            }

            _context.Entry(user).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TachoDataExists(id))
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

        // DELETE: api/users/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTachoData(int id)
        {
            var user = await _context.TachoDatas.FindAsync(id);
            if (user == null)
            {
                return NotFound();
            }

            _context.TachoDatas.Remove(user);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool TachoDataExists(int id)
        {
            return _context.TachoDatas.Any(e => e.TrackId == id);
        }

        // dummy method to test the connection
        [HttpGet("hello")]
        public string Test()
        {
            return "Hello World!";
        }

    }
}
