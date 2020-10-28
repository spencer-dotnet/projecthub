using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NelsonHub.Shared.DAL;
using NelsonHub.Shared.DAL.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace NelsonHub.Server.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class GlossaryController : ControllerBase
    {
        private readonly IGlossaryData _db;

        public GlossaryController(IGlossaryData db)
        {
            _db = db;
        }

        // GET all glossary items
        [HttpGet]
        public async Task<ActionResult<List<GlossaryItemModel>>> GetAll()
        {
            return await _db.GetAllGlossaryItems();
        }

        // GET glossary item by term
        [HttpGet]
        [Route("{term}")]
        public async Task<ActionResult<GlossaryItemModel>> Get(string term)
        {
            var glossaryItem = await _db.GetGlossaryItemByTerm(term);

            if (glossaryItem == null)
            {
                return NotFound();
            }
            else
            {
                return Ok(glossaryItem);
            }
        }

        // POST glossary item
        [HttpPost]
        public async Task<ActionResult> Post(GlossaryItemModel glossaryItem)
        {
            var rowsChanged = await _db.InsertGlossaryItem(glossaryItem);

            if (rowsChanged == 0)
            {
                return Conflict("Cannot create the term because it already exists.");
            }
            else
            {
                var resourceUrl = Path.Combine(Request.Path.ToString(), Uri.EscapeUriString(glossaryItem.Term));
                return Created(resourceUrl, glossaryItem);
            }
        }

        [HttpDelete]
        public async Task<ActionResult> Delete(GlossaryItemModel glossaryItem)
        {
            var rowsDeleted = await _db.DeleteGlossaryItemByTerm(glossaryItem.Term);

            if (rowsDeleted == 0)
            {
                return BadRequest();
            }
            else
            {
                return Accepted(glossaryItem);
            }
        }
    }
}
