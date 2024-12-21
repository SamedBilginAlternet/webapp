using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace WebApplication5.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PanelUserController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public PanelUserController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/PanelUser
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PanelUser>>> GetPanelUsers()
        {
            return await _context.PanelUsers.ToListAsync();
        }

        // GET: api/PanelUser/5
        [HttpGet("{id}")]
        public async Task<ActionResult<PanelUser>> GetPanelUser(Guid id)
        {
            var panelUser = await _context.PanelUsers.FindAsync(id);

            if (panelUser == null)
            {
                return NotFound();
            }

            return panelUser;
        }

        // PUT: api/PanelUser/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPanelUser(Guid id, PanelUser panelUser)
        {
            if (id != panelUser.Id)
            {
                return BadRequest();
            }

            _context.Entry(panelUser).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PanelUserExists(id))
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

        // POST: api/PanelUser
        [HttpPost]
        public async Task<ActionResult<PanelUser>> PostPanelUser(PanelUser panelUser)
        {
            _context.PanelUsers.Add(panelUser);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetPanelUser", new { id = panelUser.Id }, panelUser);
        }

        // DELETE: api/PanelUser/
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePanelUser(Guid id)
        {
            var panelUser = await _context.PanelUsers.FindAsync(id);
            if (panelUser == null)
            {
                return NotFound();
            }

            _context.PanelUsers.Remove(panelUser);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool PanelUserExists(Guid id)
        {
            return _context.PanelUsers.Any(e => e.Id == id);
        }


    }
}
