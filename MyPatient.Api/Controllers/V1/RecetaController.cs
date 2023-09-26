using Application.Features.PatientService;
using Application.Features.PatientService.Command.CreateRecete;
using Application.Features.PatientService.Query.GetAllRecetaByPatientId;
using Domain.Entity.PatientAggregate;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MyPatient.Api.Controllers.V1
{
    [Route("api/receta")]
    [ApiController]
    public class RecetaController : ControllerBase
    {
        private readonly IMediator _mediator;

        public RecetaController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("{patientId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> CreateReceta([FromBody] CreateReceteCommand recete)
        {
            await _mediator.Send(recete);

            return Ok();
        }

        [HttpGet("{patientId}")]
        [ProducesResponseType(typeof(List<RecetaDTO>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<List<RecetaDTO>>> GetAllRecetaByPatientId(int patientId)
        {
            var result = await _mediator.Send(new GetAllRecetaByPatientIdQuery() { PatientId = patientId });

            return Ok(result);
        }
    }
}
