using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ResidentsSocialCarePlatformApi.V1.UseCase.Interfaces;

namespace ResidentsSocialCarePlatformApi.V1.Controllers
{
    [ApiController]
    [Route("api/v1/visit-information")]
    [Produces("application/json")]
    [ApiVersion("1.0")]
    public class VisitInformationController : BaseController
    {
        private readonly IGetVisitInformationByPersonId _getVisitInformationByPersonId;

        public VisitInformationController(IGetVisitInformationByPersonId getVisitInformationByPersonId)
        {
            _getVisitInformationByPersonId = getVisitInformationByPersonId;
        }


        /// /// <summary>
        /// Find a resident's visit information by their unique PersonID
        /// </summary>
        /// <response code="200">Success. Returns resident visit information</response>
        /// <response code="404">No visit information found for the specified Person ID</response>
        [ProducesResponseType(typeof(List<Boundary.Responses.VisitInformation>), StatusCodes.Status200OK)]
        [HttpGet]
        [Route("person-id/{personId}")]
        public IActionResult GetVisitInformation(long personId)
        {
            var visitInformation = _getVisitInformationByPersonId.Execute(personId);

            if (visitInformation.Count == 0) return NotFound();

            return Ok(visitInformation);
        }
    }
}
