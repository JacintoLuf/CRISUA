using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MVC_2020_Business.Models;
using MVC_2020_Business.Services;
using MVC_2020_Database.DataModels;
//using ServiceStack.Text;
using System.Text.RegularExpressions;

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

        //Get Publication by OrcidID
        [HttpGet("[action]/{OrcidId}")]
        public async Task<ActionResult<InfoPortal>> Orcid(string OrcidId)
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
            InfoPortal info = new InfoPortal();

            info = DatabaseServices.getInfoPortalUA(_context, person_Publication);


            return info;
        }

        //Get Publication by IUPI
        [HttpGet("[action]/{iupi}")]
        public async Task<ActionResult<InfoPortal>> Iupi(string iupi)
        {
            var dataset = _context.Set<Person_Identifier>()
                           .Where(x => x.Value.Equals(iupi))
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
            InfoPortal info = new InfoPortal();

            info = DatabaseServices.getInfoPortalUA(_context, person_Publication);


            return info;
        }

    }
}