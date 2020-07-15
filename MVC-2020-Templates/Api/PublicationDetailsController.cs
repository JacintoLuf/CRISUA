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
    public class PublicationDetailsController : ControllerBase
    {
        private readonly MyDbContext _context;

        public PublicationDetailsController(MyDbContext context)
        {
            _context = context;
        }

        // GET: api/PublicationDetails
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PublicationDetail>>> GetPublicationDetail()
        {
            return await _context.PublicationDetail.ToListAsync();
        }

        // GET: api/PublicationDetails/5
        [HttpGet("{id}")]
        public async Task<ActionResult<PublicationDetail>> GetPublicationDetail(int id)
        {
            var publicationDetail = await _context.PublicationDetail.FindAsync(id);

            if (publicationDetail == null)
            {
                return NotFound();
            }

            return publicationDetail;
        }

        // PUT: api/PublicationDetails/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPublicationDetail(int id, PublicationDetail publicationDetail)
        {
            if (id != publicationDetail.PublicationId)
            {
                return BadRequest();
            }

            _context.Entry(publicationDetail).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PublicationDetailExists(id))
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

        // POST: api/PublicationDetails
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<PublicationDetail>> PostPublicationDetail(PublicationDetail publicationDetail)
        {
            _context.PublicationDetail.Add(publicationDetail);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetPublicationDetail", new { id = publicationDetail.PublicationId }, publicationDetail);
        }

        // DELETE: api/PublicationDetails/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<PublicationDetail>> DeletePublicationDetail(int id)
        {
            var publicationDetail = await _context.PublicationDetail.FindAsync(id);
            if (publicationDetail == null)
            {
                return NotFound();
            }

            _context.PublicationDetail.Remove(publicationDetail);
            await _context.SaveChangesAsync();

            return publicationDetail;
        }

        private bool PublicationDetailExists(int id)
        {
            return _context.PublicationDetail.Any(e => e.PublicationId == id);
        }
    }
}
