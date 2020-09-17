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
using CsvHelper;
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

        [HttpPost("UploadFiles")]
        public async Task<IEnumerable<Autor>> Post(IFormFile file)
        {

            // full path to file in temp location
            var content = new StringBuilder();
            content.AppendLine("tipo: " + Path.GetExtension(file.FileName));

            List<Autor> lista= new List<Autor>();

            using (var reader = new StreamReader(file.OpenReadStream()))
            using (var csv = new CsvReader(reader,
                   System.Globalization.CultureInfo.CreateSpecificCulture("PT")))
            {
                var records = csv.GetRecords<Autor>();
                //Encoding iso = Encoding.GetEncoding("ISO-8859-6");
                lista = records.ToList();
                //return lista;
            }

            foreach (Autor autor in lista)
            {
                if (autor.Name == null || !Regex.IsMatch(autor.Iupi, "[a-z0-9]{8}-[a-z0-9]{4}-[a-z0-9]{4}-[a-z0-9]{4}-[a-z0-9]{12}") || !Regex.IsMatch(autor.OrcidId, "[0-9]{4}-[0-9]{4}-[0-9]{4}-[0-9]{4}"))
                {
                    return null;//BadRequest();
                }

                if (autor.Name != "" || autor.OrcidId != "" || autor.Iupi != "")
                {
                    DatabaseServices.insertLoginPerson(_context, autor.Name, autor.OrcidId, autor.Iupi);
                    PublicacoesService.GetDifWorks(_context, autor.Name,
                        PublicacoesService.ConvertProductToWork(
                        PublicacoesService.GetProducts(_context, autor.Iupi, autor.Name)), autor.Iupi, MVC_2020_Business.Services.DatabaseServices.getOrcid(_context, autor.Iupi));
                }
            }

            //string text;
            //using (TextReader textReader = new StreamReader(file.OpenReadStream()))
            //    text = textReader.ReadToEnd();

            return lista;

            //CsvReader csv;
            //using (var ms = new MemoryStream())
            //{
            //    file.CopyTo(ms);
            //    var fileBytes = ms.ToArray();
            //    string s = Convert.ToBase64String(fileBytes);
            //    // act on the Base64 data
            //    using (var textReader2 = new StreamReader(s))
            //        csv = new CsvReader(textReader2, System.Globalization.CultureInfo.CreateSpecificCulture("PT"));
            //    var records = csv.GetRecords<Autor>().ToList();
            //    return records;
            //}




        }
    }
}