using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ASPNETWebApplication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DocumentController : ControllerBase
    {
        private readonly Helpers.ILiteDbDocumentService liteDbDocumentService;

        public DocumentController(Helpers.ILiteDbDocumentService liteDbDocumentService)
        {
            this.liteDbDocumentService = liteDbDocumentService;
        }

        [HttpGet]
        public IEnumerable<Models.Document> Get()
        {
            return liteDbDocumentService.GetAllDocuments();
        }

        [HttpGet("{id}", Name = "GetDocument")]
        public ActionResult<Models.Document> Get(int id)
        {
            Models.Document document = liteDbDocumentService.GetDocument(id);

            if (document != default)
                return Ok(document);
            else
                return NotFound();
        }

        [HttpPost]
        public ActionResult<Models.Document> Insert(Models.Document document)
        {
            int id = liteDbDocumentService.InsertDocument(document);
            if (id != default)
                return CreatedAtAction("GetDocument", liteDbDocumentService.GetDocument(id));
            else
                return BadRequest();
        }

        [HttpPut]
        public ActionResult<Models.Document> Update(Models.Document document)
        {
            bool result = liteDbDocumentService.UpdateDocument(document);
            if (result)
                return NoContent();
            else
                return NotFound();
        }

        [HttpDelete("{id}")]
        public ActionResult<Models.Document> Delete(int id)
        {
            bool result = liteDbDocumentService.DeleteDocument(id);
            if (result)
                return NoContent();
            else
                return NotFound();
        }
    }
}