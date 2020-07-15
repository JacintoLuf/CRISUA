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
    public class PublicationTitlesController : ControllerBase
    {
        private readonly MyDbContext _context;

        public PublicationTitlesController(MyDbContext context)
        {
            _context = context;
        }

        // GET: api/PublicationTitles
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PublicationTitle>>> GetPublicationTitle()
        {
            return await _context.PublicationTitle.ToListAsync();
        }

        // GET: api/PublicationTitles/5
        [HttpGet("{id}")]
        public async Task<ActionResult<PublicationTitle>> GetPublicationTitle(string id)
        {
            var publicationTitle = await _context.PublicationTitle.FindAsync(id);

            if (publicationTitle == null)
            {
                return NotFound();
            }

            return publicationTitle;
        }

        // PUT: api/PublicationTitles/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPublicationTitle(string id, PublicationTitle publicationTitle)
        {
            if (id != publicationTitle.Title)
            {
                return BadRequest();
            }

            _context.Entry(publicationTitle).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PublicationTitleExists(id))
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

        // POST: api/PublicationTitles
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<PublicationTitle>> PostPublicationTitle(PublicationTitle publicationTitle)
        {
            _context.PublicationTitle.Add(publicationTitle);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (PublicationTitleExists(publicationTitle.Title))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetPublicationTitle", new { id = publicationTitle.Title }, publicationTitle);
        }

        // DELETE: api/PublicationTitles/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<PublicationTitle>> DeletePublicationTitle(string id)
        {
            var publicationTitle = await _context.PublicationTitle.FindAsync(id);
            if (publicationTitle == null)
            {
                return NotFound();
            }

            _context.PublicationTitle.Remove(publicationTitle);
            await _context.SaveChangesAsync();

            return publicationTitle;
        }

        private bool PublicationTitleExists(string id)
        {
            return _context.PublicationTitle.Any(e => e.Title == id);
        }
    }
}
