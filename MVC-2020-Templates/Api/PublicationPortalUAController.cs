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

namespace MVC_2020_Template.Api
{
    [Route("api/[controller]")]
    [ApiController]
    public class PublicationPortalUAController : ControllerBase
    {
        private readonly MyDbContext _context;

        public PublicationPortalUAController(MyDbContext context)
        {
            _context = context;
        }

        [HttpGet("{OrcidId}")]
        public async Task<ActionResult<Rootobject>> GetPublicationDetail(string OrcidId)
        {
            var dataset = _context.Set<Person_Identifier>()
                           .Where(x => x.Value.Equals(OrcidId))
                           .Select(x => new Person_Identifier
                           {
                               PersonID = x.PersonID,
                               Value = x.Value,
                               IdentifierId = x.IdentifierId
                           }).ToList();

            var person_id = 0;

            foreach (Person_Identifier person in dataset)
            {
                person_id = person.PersonID;
            }

            var person_Publication = await _context.Person_Publication.Where(i => i.PersonId == person_id).ToListAsync();

            if (person_Publication == null)
            {
                return NotFound();
            }
            Rootobject info = new Rootobject();

            info = DatabaseServices.getInfoPortalUA(_context, person_Publication);


            return info;
        }
    }
}