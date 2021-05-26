using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ResidentsSocialCarePlatformApi.V1.Boundary.Requests;
using ResidentsSocialCarePlatformApi.V1.Boundary.Responses;
using ResidentsSocialCarePlatformApi.V1.Domain;
using ResidentsSocialCarePlatformApi.V1.UseCase.Interfaces;

namespace ResidentsSocialCarePlatformApi.V1.Controllers
{
    [ApiController]
    [Route("api/v1/residents")]
    [Produces("application/json")]
    [ApiVersion("1.0")]
    public class ResidentsController : BaseController
    {
        private readonly IGetAllResidentsUseCase _getAllResidentsUseCase;
        private readonly IGetEntityByIdUseCase _getEntityByIdUseCase;
        private readonly IGetAllCaseNotesUseCase _getAllCaseNotesUseCase;
        private readonly IGetVisitInformationByPersonId _getVisitInformationByPersonId;
        private readonly IGetResidentRecordsUseCase _getResidentRecordsUseCase;
        private readonly IGetRelationshipsByPersonIdUseCase _getRelationIGetRelationshipsByPersonIdUseCase;

        public ResidentsController(
            IGetAllResidentsUseCase getAllResidentsUseCase,
            IGetEntityByIdUseCase getEntityByIdUseCase,
            IGetAllCaseNotesUseCase getAllCaseNotesUseCase,
            IGetVisitInformationByPersonId getVisitInformationByPersonId,
            IGetResidentRecordsUseCase getResidentRecordsUseCase,
            IGetRelationshipsByPersonIdUseCase getRelationIGetRelationshipsByPersonIdUseCase
            )
        {
            _getAllResidentsUseCase = getAllResidentsUseCase;
            _getEntityByIdUseCase = getEntityByIdUseCase;
            _getAllCaseNotesUseCase = getAllCaseNotesUseCase;
            _getVisitInformationByPersonId = getVisitInformationByPersonId;
            _getResidentRecordsUseCase = getResidentRecordsUseCase;
            _getRelationIGetRelationshipsByPersonIdUseCase = getRelationIGetRelationshipsByPersonIdUseCase;
        }

        /// <summary>
        /// Returns list of contacts who share the query search parameter
        /// </summary>
        /// <response code="200">Success. Returns a list of matching residents information</response>
        /// <response code="400">Invalid Query Parameter.</response>
        [ProducesResponseType(typeof(ResidentInformationList), StatusCodes.Status200OK)]
        [HttpGet]
        public IActionResult ListContacts([FromQuery] ResidentQueryParam rqp, int? cursor = 0, int? limit = 20)
        {
            try
            {
                return Ok(_getAllResidentsUseCase.Execute(rqp, (int) cursor, (int) limit));
            }
            catch (InvalidQueryParameterException e)
            {
                return BadRequest(e.Message);
            }
        }

        /// /// <summary>
        /// Find a resident by Mosaic ID
        /// </summary>
        /// <response code="200">Success. Returns resident related to the specified ID</response>
        /// <response code="404">No resident found for the specified ID</response>
        [ProducesResponseType(typeof(Boundary.Responses.ResidentInformation), StatusCodes.Status200OK)]
        [HttpGet]
        [Route("{mosaicId}")]
        public IActionResult ViewRecord(int mosaicId)
        {
            try
            {
                return Ok(_getEntityByIdUseCase.Execute(mosaicId));
            }
            catch (ResidentNotFoundException)
            {
                return NotFound();
            }
        }

        /// /// <summary>
        /// Returns list of historic records for a Person ID
        /// </summary>
        /// <response code="200">Success. Returns a list of record information for a resident</response>
        [ProducesResponseType(typeof(CaseNoteInformationList), StatusCodes.Status200OK)]
        [HttpGet]
        [Route("{personId}/records")]
        public IActionResult GetResidentRecords(long personId)
        {
            return Ok(_getResidentRecordsUseCase.Execute(personId));
        }

        /// /// <summary>
        /// Returns list of cases notes for a Person/Mosaic ID
        /// </summary>
        /// <response code="200">Success. Returns a list of matching case note information</response>
        [ProducesResponseType(typeof(CaseNoteInformationList), StatusCodes.Status200OK)]
        [HttpGet]
        [Route("{personId}/case-notes")]
        public IActionResult ListCaseNotes(long personId)
        {
            return Ok(_getAllCaseNotesUseCase.Execute(personId));
        }

        /// /// <summary>
        /// Returns a list of visit information for a Person/Mosaic ID
        /// </summary>
        /// <response code="200">Success. Returns a list of visit information for a resident</response>
        /// <response code="404">No visit information found for the specified Person ID</response>
        [ProducesResponseType(typeof(List<Boundary.Responses.VisitInformation>), StatusCodes.Status200OK)]
        [HttpGet]
        [Route("{personId}/visits")]
        public IActionResult GetVisitInformation(long personId)
        {
            var visitInformation = _getVisitInformationByPersonId.Execute(personId);

            if (visitInformation.Count == 0) return NotFound();

            return Ok(visitInformation);
        }

        /// /// <summary>
        /// Get relationships for a person
        /// </summary>
        /// <response code="200">Success. Returns relationships for a person</response>
        /// <response code="400">Invalid person ID</response>
        [ProducesResponseType(typeof(Boundary.Responses.Relationships), StatusCodes.Status200OK)]
        [HttpGet]
        [Route("{personId}/relationships")]
        public IActionResult GetRelationships([FromQuery] GetRelationshipsRequest request)
        {
            var validator = new GetRelationshipsRequestValidator();
            var validationResults = validator.Validate(request);

            if (!validationResults.IsValid)
            {
                return BadRequest(validationResults.ToString());
            }

            var relationships = _getRelationIGetRelationshipsByPersonIdUseCase.Execute(request);

            return Ok(relationships);
        }
    }
}
