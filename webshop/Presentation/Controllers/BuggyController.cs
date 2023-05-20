using Microsoft.AspNetCore.Mvc;
using Contracts.Dtos.Errors;
using Services.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;

namespace Presentation.Controllers
{
    [ApiExplorerSettings(IgnoreApi = true)]
    public class BuggyController : BaseApiController
    {
        private readonly IServiceManager serviceManager;

        public BuggyController(IServiceManager _serviceManager)
        {
            serviceManager = _serviceManager;
        }

        [HttpGet("notfound")]
        public async Task<ActionResult> GetNotFoundRequest()
        {
            var thing = await serviceManager.ProductService.GetProductAsync(new Guid());

            if (thing == null) return NotFound(new ApiResponse(404));

            return Ok();
        }

        [HttpGet("servererror")]
        public async Task<ActionResult> GetServerError()
        {
            var thing = await serviceManager.ProductService.GetProductAsync(new Guid());

            var thingToReturn = thing.ToString();

            return NotFound();
        }

        [HttpGet("badrequest")]
        public ActionResult GetBadRequest()
        {
            return BadRequest(new ApiResponse(400));
        }

        [HttpGet("badrequest/{id}")]
        public ActionResult GetBadRequest(Guid id)
        {
            return Ok();
        }

        [HttpGet("testauth")]
        [Authorize]
        public ActionResult<string> GetSecretText()
        {
            return "fakeSecret";
        }

        [HttpGet("testadmin")]
        [Authorize(Roles = "Admin")]
        public ActionResult<string> IsAdmin()
        {
            return "hi admin";
        }
    }
}
