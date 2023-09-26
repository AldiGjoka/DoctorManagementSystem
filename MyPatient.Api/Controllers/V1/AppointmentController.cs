using Application.Features.Appointmens.Query;
using Application.Features.PatientService;
using Application.Features.PatientService.Command.CreateAppointment;
using Application.Features.PatientService.Command.DeleteAppointment;
using Application.Features.PatientService.Command.UpdateAppointment;
using Application.Features.PatientService.Query.GetAllAppointmentsByPatientId;
using Application.Features.PatientService.Query.GetAppointmentById;
using Domain.Entity.PatientAggregate;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MyPatient.Api.Controllers.V1
{
    [Route("api/appointment")]
    [ApiController]
    public class AppointmentController : ControllerBase
    {
        private readonly IMediator _mediator;

        public AppointmentController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("{patientId}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> CreateAppointment(int patientId, [FromBody] CreateAppointmentCommand appointment)
        {
            if (patientId != appointment.PatientId)
            {
                return NotFound();
            }

            await _mediator.Send(appointment);

            return NoContent();
        }

        [HttpGet("doctor/{doctorId}")]
        [ProducesResponseType(typeof(AppointemntListDTO), StatusCodes.Status200OK)]
        public async Task<ActionResult<AppointmentDTO>> GetAllTodayAppintments(int doctorId)
        {
            var request = new GetAllTodayAppointmentsQuery() { DoctorId = doctorId};

            var result = await _mediator.Send(request);

            return Ok(result);
        }

        [HttpGet("{patientId}/{appointmentId}")]
        [ProducesResponseType(typeof(AppointmentDTO), StatusCodes.Status200OK)]
        public async Task<ActionResult<AppointmentDTO>> GetAppintmentById(int patientId, int appointmentId)
        {
            var request = new GetAppointmentByIdQuery() { PatientId = patientId, AppointmentId = appointmentId };

            var result = await _mediator.Send(request);

            return Ok(result);
        }

        [HttpGet("{patientId}")]
        [ProducesResponseType(typeof(List<AppointmentDTO>), StatusCodes.Status200OK)]
        public async Task<ActionResult<List<AppointmentDTO>>> GetAppintmentsByPatientId(int patientId)
        {
            var request = new GetAllAppointmentsByPatientIdQuery() { PatientId = patientId };

            var result = await _mediator.Send(request);

            return Ok(result);
        }

        [HttpDelete("{patientId}/{appointmentId}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<ActionResult> DeleteAppointment(int patientId, int appointmentId)
        {
            var request = new DeleteAppointmentCommand() { PatientId = patientId, AppointmentId = appointmentId };

            await _mediator.Send(request);

            return NoContent();
        }

        [HttpPut("{patientId}/{appointmentId}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<ActionResult> UpdateAppointment([FromBody] UpdateAppointmentCommand updateAppointment)
        {
            await _mediator.Send(updateAppointment);

            return NoContent();
        }
    }
}
