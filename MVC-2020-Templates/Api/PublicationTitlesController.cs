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
        [HttpGet("{PublicationId}")]
        public async Task<ActionResult<IEnumerable<PublicationTitle>>> GetPublicationTitle(int PublicationId)
        {
            var publicationTitle = await _context.PublicationTitle.Where(i => i.PublicationId == PublicationId).ToListAsync();

            if (publicationTitle == null)
            {
                return NotFound();
            }

            return publicationTitle;
        }

        // PUT: api/PublicationTitles/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkPublicationId=2123754.
        [HttpPut("{PublicationId}")]
        public async Task<IActionResult> PutPublicationTitle(string PublicationId, PublicationTitle publicationTitle)
        {
            if (PublicationId != publicationTitle.Title)
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
                if (!PublicationTitleExists(PublicationId))
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
        // more details, see https://go.microsoft.com/fwlink/?linkPublicationId=2123754.
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

            return CreatedAtAction("GetPublicationTitle", new { PublicationId = publicationTitle.PublicationId }, publicationTitle);
        }

        // DELETE: api/PublicationTitles/5
        [HttpDelete("{PublicationId}")]
        public async Task<ActionResult<PublicationTitle>> DeletePublicationTitle(int PublicationId)
        {
            var publicationTitle = await _context.PublicationTitle.FirstAsync(i => i.PublicationId == PublicationId);
            if (publicationTitle == null)
            {
                return NotFound();
            }

            _context.PublicationTitle.Remove(publicationTitle);
            await _context.SaveChangesAsync();

            return publicationTitle;
        }

        private bool PublicationTitleExists(string title)
        {
            return _context.PublicationTitle.Any(e => e.Title == title);
        }
    }
}
