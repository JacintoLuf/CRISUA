using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MVC_2020_Business.Services;
using MVC_2020_Database.DataModels;
using RestSharp;
using CsvHelper;
using MVC_2020_Business.Models;
using System.Text;
using System.IO;

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

        //// GET: api/Publications
        //[HttpGet]
        //public async Task<ActionResult<IEnumerable<Publication>>> GetPublication()
        //{
        //    return await _context.Publication.ToListAsync();
        //}

        // GET: api/Publications/5
        //[HttpGet("{id}")]
        //public async Task<ActionResult<Publication>> GetPublication(int id)
        //{
        //    var publication = await _context.Publication.FindAsync(id);

        //    if (publication == null)
        //    {
        //        return NotFound();
        //    }

        //    return publication;
        //}

        // PUT: api/Publications/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{nome}/{iupi}/{orcidId?}")]
        //[SwaggerOperationFilter(typeof(Api.ReApplyOptionalRouteParameterOperationFilter))]
        public async Task<IActionResult> PutPublication([FromRoute] string nome, [FromRoute] string iupi, [FromRoute] string? orcidId = null)
        {
            if (nome == null || !Regex.IsMatch(iupi, "[a-z0-9]{8}-[a-z0-9]{4}-[a-z0-9]{4}-[a-z0-9]{4}-[a-z0-9]{12}") || !Regex.IsMatch(orcidId, "[0-9]{4}-[0-9]{4}-[0-9]{4}-[0-9]{4}"))
            {
                return BadRequest();
            }

            //nao funciona
            if (orcidId == null || orcidId == ",")
            {
                //https://ws-id.ua.pt/api/orcid/####################
                //https://ws-id.ua.pt/api/orcid/####################
                string req = "https://ws-id.ua.pt/api/orcid/" + iupi;
                var client = new RestClient(req);
                client.Timeout = -1;
                var request = new RestRequest(Method.GET);
                request.AddHeader("Authorization", "Basic Y3JpczprdWhrMzI0MjMkJCEzNDUkRVJHVEVSLjM0");
                IRestResponse response = client.Execute(request);
                orcidId = response.Content;
            }


            try
            {
                DatabaseServices.insertLoginPerson(_context, nome, orcidId, iupi);
                PublicacoesService.GetDifWorks(_context, nome,
                    PublicacoesService.ConvertProductToWork(
                    PublicacoesService.GetProducts(_context, iupi, nome)), iupi, MVC_2020_Business.Services.DatabaseServices.getOrcid(_context, iupi));
            }
            catch (Exception e)
            {
                throw;
            }

            return Ok();


            //try
            //{
            //    await _context.SaveChangesAsync();
            //}
            //catch (DbUpdateConcurrencyException)
            //{
            //    if (!PublicationExists(id))
            //    {
            //        return NotFound();
            //    }
            //    else
            //    {
            //        throw;
            //    }
            //}
        }

        [HttpPost("UploadFiles")]
        public async Task<IEnumerable<Autor>> Post(IFormFile file)
        {

            // full path to file in temp location
            var content = new StringBuilder();
            content.AppendLine("tipo: " + Path.GetExtension(file.FileName));

            List<Autor> lista = new List<Autor>();

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


        // POST: api/Publications
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        //[HttpPost]
        //public async Task<ActionResult<Publication>> PostPublication(Publication publication)
        //{
        //    _context.Publication.Add(publication);
        //    await _context.SaveChangesAsync();

        //    return CreatedAtAction("GetPublication", new { id = publication.PublicationId }, publication);
        //}

        // DELETE: api/Publications/5
        [HttpDelete("[action]")]
        public async Task<ActionResult<Publication>> ClearBD()
        {
            //var publication = await _context.Publication.FindAsync(id);
            //if (publication == null)
            //{
            //    return NotFound();
            //}

            //_context.Publication.Remove(publication);
            //await _context.SaveChangesAsync();

            //return publication;

            DatabaseServices.limparBD(_context);
            return null;
        }

        private bool PublicationExists(int id)
        {
            return _context.Publication.Any(e => e.PublicationId == id);
        }
    }
}
