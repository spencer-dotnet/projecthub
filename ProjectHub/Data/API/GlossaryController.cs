using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProjectHub.Data.Models;
using ProjectHub.Data.Services;
using ProjectHub.Data.SqlDatabase;

namespace ProjectHub.Data.API
{
    [Route("api/[controller]")]
    [ApiController]
    public class GlossaryController : ControllerBase
    {
        private readonly IGlossaryData _db;

        public GlossaryController(IGlossaryData db)
        {
            _db = db;
        }

        [HttpGet]
        public async Task<ActionResult<List<GlossaryItem>>> Get()
        {
            return await _db.GetAllGlossaryItems();
        }

        [HttpGet]
        [Route("{term}")]
        public async Task<ActionResult<GlossaryItem>> Get(string term)
        {
            var glossaryItem = await _db.GetGlossaryItemByTerm(term);

            if(glossaryItem == null)
            {
                return NotFound();
            }
            else
            {
                return Ok(glossaryItem);
            }
        }

        [HttpPost]
        public async Task<ActionResult> Post(GlossaryItem glossaryItem)
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

    }
}
