using Microsoft.AspNetCore.Mvc;
using NelsonHub.Shared.DAL;
using NelsonHub.Shared.DAL.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace NelsonHub.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReferenceController : ControllerBase
    {
        private readonly IReferenceData _db;

        public ReferenceController(IReferenceData db)
        {
            _db = db;
        }

        //GET all
        [HttpGet]
        public async Task<ActionResult<List<ReferenceModel>>> GetAll()
        {
            return await _db.GetAllReferences();
        }

        //GET one reference

        //POST
        [HttpPost]
        public async Task<ActionResult> Post(ReferenceModel reference)
        {
            var rowsChanged = await _db.InsertReference(reference);

            if(rowsChanged == 0)
            {
                return Conflict("Cannot create the Reference at this time.");
            }
            else
            {
                var resourceUrl = Path.Combine(Request.Path.ToString(), Uri.EscapeUriString(reference.Id.ToString()));
                return Created(resourceUrl, reference);
            }
        }
    }
}
