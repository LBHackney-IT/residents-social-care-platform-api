using System;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Npgsql;
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
    public class SocialCareController : BaseController
    {
        private IGetAllResidentsUseCase _getAllResidentsUseCase;
        private IGetEntityByIdUseCase _getEntityByIdUseCase;

        private IGetAllCaseNotesUseCase _getAllCaseNotesUseCase;

        public SocialCareController(IGetAllResidentsUseCase getAllResidentsUseCase, IGetEntityByIdUseCase getEntityByIdUseCase, IGetAllCaseNotesUseCase getAllCaseNotesUseCase)
        {
            _getAllResidentsUseCase = getAllResidentsUseCase;
            _getEntityByIdUseCase = getEntityByIdUseCase;
            _getAllCaseNotesUseCase = getAllCaseNotesUseCase;
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
        /// Returns list of cases notes for a Person/Mosaic ID
        /// </summary>
        /// <response code="200">Success. Returns a list of matching case note information</response>
        // TO DO: Error Handling & responses????
        [ProducesResponseType(typeof(CaseNoteInformationList), StatusCodes.Status200OK)]
        [HttpGet]
        [Route("{personId}/case-notes")]
        public IActionResult ListCaseNotes(long personId)
        {
            return Ok(_getAllCaseNotesUseCase.Execute(personId));
        }

        [HttpGet]
        [Route("test")]
        public IActionResult TestEndpoint()
        {
            var connection = new NpgsqlConnection(Environment.GetEnvironmentVariable("CONNECTION_STRING"));
            connection.Open();

            var npgsqlCommand = connection.CreateCommand();
            npgsqlCommand.CommandText = "SELECT RANDOM()";
            var result = npgsqlCommand.ExecuteScalar();

            connection.Close();

            return Ok(result);
        }
    }
}
