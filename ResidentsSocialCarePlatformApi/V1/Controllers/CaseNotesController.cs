using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ResidentsSocialCarePlatformApi.V1.UseCase.Interfaces;

namespace ResidentsSocialCarePlatformApi.V1.Controllers
{
    [ApiController]
    [Route("api/v1/case-notes")]
    [Produces("application/json")]
    [ApiVersion("1.0")]
    public class CaseNotesController : BaseController
    {
        private IGetCaseNoteInformationByIdUseCase _getCaseNoteInformationByIdUseCase;

        public CaseNotesController(IGetCaseNoteInformationByIdUseCase getCaseNoteInformationByIdUseCase)
        {
            _getCaseNoteInformationByIdUseCase = getCaseNoteInformationByIdUseCase;
        }

        /// /// <summary>
        /// Get a case note by ID
        /// </summary>
        /// <response code="200">Success. Returns a case note related to the specified ID</response>
        /// <response code="404">No case note found for the specified ID</response>
        [ProducesResponseType(typeof(Boundary.Responses.CaseNoteInformation), StatusCodes.Status200OK)]
        [HttpGet]
        [Route("{caseNoteId}")]
        public IActionResult GetCaseNote(long caseNoteId)
        {
            var caseNote = _getCaseNoteInformationByIdUseCase.Execute(caseNoteId);

            if (caseNote == null) return NotFound();

            return Ok(caseNote);
        }
    }
}
