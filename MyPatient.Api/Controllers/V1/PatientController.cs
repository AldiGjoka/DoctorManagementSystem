using Application.Features.PatientService;
using Application.Features.PatientService.Command.CreateAppointment;
using Application.Features.PatientService.Command.CreatePatient;
using Application.Features.PatientService.Command.DeletePatient;
using Application.Features.PatientService.Command.UpdatePatient;
using Application.Features.PatientService.Query.GetAllPatients;
using Application.Features.PatientService.Query.GetPatientById;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MyPatient.Api.Controllers.V1
{
    [Route("api/patient")]
    [ApiController]
    public class PatientController : ControllerBase
    {
        private readonly IMediator _mediator;
        public PatientController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("{doctorId}")]
        [ProducesResponseType(typeof(PatientDTO), StatusCodes.Status200OK)]
        public async Task<ActionResult<IReadOnlyList<PatientDTO>>> GetAllPatientsByDoctor(int doctorId)
        {
            var result = await _mediator.Send(new GetAllPatientsQuery() { DoctorId = doctorId});

            return Ok(result);
        }


        [HttpGet("{doctorId}/{patientId}")]
        [ProducesResponseType(typeof(PatientDTO), StatusCodes.Status200OK)]
        public async Task<ActionResult<IReadOnlyList<PatientDTO>>> GetAllPatientsById(int doctorId, int patientId)
        {
            var result = await _mediator.Send(new GetPatientByIdQuery() { DoctorId = doctorId, PatientId = patientId });

            return Ok(result);
        }

        [HttpPost]
        [ProducesResponseType(typeof(PatientDTO), StatusCodes.Status200OK)]
        public async Task<ActionResult<PatientDTO>> CreatePatient([FromBody] CreatePatientCommand patient)
        {
            var result = await _mediator.Send(patient);

            return CreatedAtAction(nameof(GetAllPatientsById), new { doctorId = patient.DoctorId, patientId = result.PatientId }, result);
        }

        [HttpPut("{doctorId}/{patientId}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> UpdatePatient(int doctorId, int patientId, [FromBody] UpdatePatientCommand patient)
        {
            if(patientId != patient.PatientId)
            {
                return NotFound();
            }
            await _mediator.Send(patient);

            return NoContent();
        }

        [HttpDelete("{doctorId}/{patientId}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<ActionResult> DeletePatient(int doctorId, int patientId)
        {
            await _mediator.Send(new DeletePatientCommand() { DoctorId = doctorId, PatientId = patientId });

            return NoContent();
        }
    }
}
