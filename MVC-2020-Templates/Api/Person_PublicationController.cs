using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MVC_2020_Business.Models;
using MVC_2020_Business.Services;
using MVC_2020_Database.DataModels;
using ServiceStack.OrmLite.Converters;

namespace MVC_2020_Template.Api
{
    [Route("api/[controller]")]
    [ApiController]
    public class Person_PublicationController : ControllerBase
    {
        private readonly MyDbContext _context;

        public Person_PublicationController(MyDbContext context)
        {
            _context = context;
        }

        // GET: api/Person_Publication
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Person_Publication>>> GetPerson_Publication()
        {
            return await _context.Person_Publication.ToListAsync();
        }

        // GET: api/Person_Publication/5
        [HttpGet("{id}")]
        public async Task<ActionResult<IEnumerable<Person_Publication>>> GetPerson_Publication(int id)
        {
            var person_Publication = await _context.Person_Publication.Where(i => i.PersonId == id).ToListAsync();

            if (person_Publication == null)
            {
                return NotFound();
            }

            return person_Publication;
        }

            // GET: api/Person_Publication/5
            //[HttpGet("{OrcidId}")]
            //public async Task<ActionResult<IEnumerable<Publicacao>>> GetPerson_Publication(string OrcidId)
            //{
            //    //orcidID = "0000-0002-4356-4522";
            //    var dataset = _context.Set<Person_Identifier>()
            //                    .Where(x => x.Value.Equals(OrcidId))
            //                    .Select(x => new Person_Identifier
            //                    {
            //                        PersonID = x.PersonID,
            //                        Value = x.Value,
            //                        IdentifierId = x.IdentifierId
            //                    }).ToList();

            //    var person_id = 0;

            //    foreach (Person_Identifier person in dataset)
            //    {
            //        person_id = person.PersonID;
            //    }

            //    var person_Publication = await _context.Person_Publication.Where(i => i.PersonId == person_id).ToListAsync();

            //    if (person_Publication == null)
            //    {
            //        return NotFound();
            //    }
            //    List<Publicacao> pubs = new List<Publicacao>();
            //    foreach(var p in person_Publication)
            //    {
            //        pubs.Add(DatabaseServices.retrieveInfoPublicacao(_context, p.PublicationId));
            //    }

            //    return pubs;
            //}

            // PUT: api/Person_Publication/5
            // To protect from overposting attacks, enable the specific properties you want to bind to, for
            // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPerson_Publication(int id, Person_Publication person_Publication)
        {
            if (id != person_Publication.PersonId)
            {
                return BadRequest();
            }

            _context.Entry(person_Publication).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!Person_PublicationExists(id))
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

        // POST: api/Person_Publication
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<Person_Publication>> PostPerson_Publication(Person_Publication person_Publication)
        {
            _context.Person_Publication.Add(person_Publication);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (Person_PublicationExists(person_Publication.PersonId))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetPerson_Publication", new { id = person_Publication.PersonId }, person_Publication);
        }

        // DELETE: api/Person_Publication/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Person_Publication>> DeletePerson_Publication(int id)
        {
            var person_Publication = await _context.Person_Publication.FirstAsync(i => i.PersonId == id);
            if (person_Publication == null)
            {
                return NotFound();
            }

            _context.Person_Publication.Remove(person_Publication);
            await _context.SaveChangesAsync();

            return person_Publication;
        }

        private bool Person_PublicationExists(int id)
        {
            return _context.Person_Publication.Any(e => e.PersonId == id);
        }
    }
}
