using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ResidentsSocialCarePlatformApi.V1.UseCase.Interfaces;

namespace ResidentsSocialCarePlatformApi.V1.Controllers
{
    [ApiController]
    [Route("api/v1/visits")]
    [Produces("application/json")]
    [ApiVersion("1.0")]
    public class VisitInformationController : BaseController
    {

        private IGetVisitInformationByVisitId _getVisitInformationByVisitId;

        public VisitInformationController(IGetVisitInformationByVisitId getVisitInformationByVisitId)
        {
            _getVisitInformationByVisitId = getVisitInformationByVisitId;
        }

        /// <summary>
        /// Get visit information by visit ID
        /// </summary>
        /// <response code="200">Success. Returns a matching visit</response>
        /// <response code="404">No visit found the the specified visit ID.</response>
        [ProducesResponseType(typeof(Boundary.Responses.VisitInformation), StatusCodes.Status200OK)]
        [HttpGet]
        [Route("{visitId}")]
        public IActionResult GetVisit(long visitId)
        {
            var visitInformation = _getVisitInformationByVisitId.Execute(visitId);

            if (visitInformation == null)
            {
                return NotFound();
            }

            return Ok(visitInformation);
        }

    }
}
