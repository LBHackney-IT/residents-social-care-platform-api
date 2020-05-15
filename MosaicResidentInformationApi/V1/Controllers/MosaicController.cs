using Microsoft.AspNetCore.Mvc;

namespace MosaicResidentInformationApi.V1.Controllers
{
    [ApiController]
    [Route("api/v1/residents")]
    [Produces("application/json")]
    [ApiVersion("1.0")]
    public class MosaicController : BaseController
    {

        [HttpGet]
        public IActionResult ListContacts()
        {
            return Ok("Hello World");
        }

        [HttpGet]
        [Route("{mosaicId}")]
        public IActionResult ViewRecord(string mosaicId)
        {
            return Ok("Hello World");
        }

    }
}
