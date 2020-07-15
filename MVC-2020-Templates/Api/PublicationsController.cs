using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MVC_2020_Database.DataModels;

namespace MVC_2020_Template.Api
{
    [Route("api/[controller]")]
    [ApiController]
    public class PublicationsController : ControllerBase
    {
        private readonly MyDbContext _context;

        public PublicationsController(MyDbContext context)
        {
            _context = context;
        }

        // GET: api/Publications
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Publication>>> GetPublication()
        {
            return await _context.Publication.ToListAsync();
        }

        // GET: api/Publications/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Publication>> GetPublication(int id)
        {
            var publication = await _context.Publication.FindAsync(id);

            if (publication == null)
            {
                return NotFound();
            }

            return publication;
        }

        // PUT: api/Publications/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPublication(int id, Publication publication)
        {
            if (id != publication.PublicationId)
            {
                return BadRequest();
            }

            _context.Entry(publication).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PublicationExists(id))
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

        // POST: api/Publications
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<Publication>> PostPublication(Publication publication)
        {
            _context.Publication.Add(publication);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetPublication", new { id = publication.PublicationId }, publication);
        }

        // DELETE: api/Publications/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Publication>> DeletePublication(int id)
        {
            var publication = await _context.Publication.FindAsync(id);
            if (publication == null)
            {
                return NotFound();
            }

            _context.Publication.Remove(publication);
            await _context.SaveChangesAsync();

            return publication;
        }

        private bool PublicationExists(int id)
        {
            return _context.Publication.Any(e => e.PublicationId == id);
        }
    }
}
