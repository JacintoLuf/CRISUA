using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MVC_2020_Database.DataModels;
using MVC_2020_Business;
using MVC_2020_Business.Models;
using System.Text;
using System.IO;
using CsvHelper;
using System.Text.RegularExpressions;
using MVC_2020_Business.Services;

namespace MVC_2020_Template.Api
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrgUnitsController : ControllerBase
    {
        private readonly MyDbContext _context;

        public OrgUnitsController(MyDbContext context)
        {
            _context = context;
        }

        // GET: api/OrgUnits
        [HttpGet]
        public async Task<ActionResult<IEnumerable<OrgUnit>>> GetOrgUnit()
        {
            return await _context.OrgUnit.ToListAsync();
        }

        // GET: api/OrgUnits/5
        [HttpGet("{id}")]
        public async Task<ActionResult<OrgUnit>> GetOrgUnit(int id)
        {
            var orgUnit = await _context.OrgUnit.FindAsync(id);

            if (orgUnit == null)
            {
                return NotFound();
            }

            return orgUnit;
        }

        [HttpPost("UploadFiles")]
        public async Task<IEnumerable<UnidadeInvestigacao>> Post(IFormFile file)
        {

            // full path to file in temp location
            var content = new StringBuilder();
            content.AppendLine("tipo: " + Path.GetExtension(file.FileName));

            List<UnidadeInvestigacao> lista = new List<UnidadeInvestigacao>();

            using (var reader = new StreamReader(file.OpenReadStream()))
            using (var csv = new CsvReader(reader,
                   System.Globalization.CultureInfo.CreateSpecificCulture("PT")))
            {
                var records = csv.GetRecords<UnidadeInvestigacao>();
                //Encoding iso = Encoding.GetEncoding("ISO-8859-6");
                lista = records.ToList();
                //return lista;
            }

            foreach (UnidadeInvestigacao ui in lista)
            {
                DatabaseServices.insertOrgUnit(_context,ui.Acronym,ui.URI,ui.ClassStartDate,ui.ClassEndDate,ui.Fraction,ui.Value,ui.OrgUnitId2,ui.PAddressId,ui.ActLanguageId,ui.Text,ui.Keywords,ui.Name);   
            }

            return lista;
        }

        // PUT: api/OrgUnits/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        //[HttpPut("{id}")]
        //public async Task<IActionResult> PutOrgUnit(int id, OrgUnit orgUnit)
        //{
        //    if (id != orgUnit.OrgUnitId)
        //    {
        //        return BadRequest();
        //    }

        //    _context.Entry(orgUnit).State = EntityState.Modified;

        //    try
        //    {
        //        await _context.SaveChangesAsync();
        //    }
        //    catch (DbUpdateConcurrencyException)
        //    {
        //        if (!OrgUnitExists(id))
        //        {
        //            return NotFound();
        //        }
        //        else
        //        {
        //            throw;
        //        }
        //    }

        //    return NoContent();
        //}

        //// POST: api/OrgUnits
        //// To protect from overposting attacks, enable the specific properties you want to bind to, for
        //// more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        //[HttpPost]
        //public async Task<ActionResult<OrgUnit>> PostOrgUnit(OrgUnit orgUnit)
        //{
        //    _context.OrgUnit.Add(orgUnit);
        //    await _context.SaveChangesAsync();

        //    return CreatedAtAction("GetOrgUnit", new { id = orgUnit.OrgUnitId }, orgUnit);
        //}

        //// DELETE: api/OrgUnits/5
        //[HttpDelete("{id}")]
        //public async Task<ActionResult<OrgUnit>> DeleteOrgUnit(int id)
        //{
        //    var orgUnit = await _context.OrgUnit.FindAsync(id);
        //    if (orgUnit == null)
        //    {
        //        return NotFound();
        //    }

        //    _context.OrgUnit.Remove(orgUnit);
        //    await _context.SaveChangesAsync();

        //    return orgUnit;
        //}

        //private bool OrgUnitExists(int id)
        //{
        //    return _context.OrgUnit.Any(e => e.OrgUnitId == id);
        //}
    }
}
