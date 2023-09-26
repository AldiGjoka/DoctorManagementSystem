using Application.Features.DoctorService;
using Application.Features.DoctorService.Command.CreateDoctor;
using Application.Features.DoctorService.Command.DeleteDoctor;
using Application.Features.DoctorService.Command.UpdateDoctor;
using Application.Features.DoctorService.Query.GetAllDoctors;
using Application.Features.DoctorService.Query.GetDoctorByEmail;
using Application.Features.DoctorService.Query.GetDoctorById;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace MyPatient.Api.Controllers.V1
{
    [Route("api/doctor")]
    [ApiController]
    public class DoctorController : ControllerBase
    {
        private readonly IMediator _mediator;
        public DoctorController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        [ProducesResponseType(typeof(IReadOnlyList<DoctorDTO>), StatusCodes.Status200OK)]
        public async Task<ActionResult<IReadOnlyList<DoctorDTO>>> GetAllDoctors()
        {
            var result = await _mediator.Send(new GetAllDoctorsQuery());

            return Ok(result);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(DoctorDTO), StatusCodes.Status200OK)]
        public async Task<ActionResult<DoctorDTO>> GetDoctorById(int id)
        {
            var result = await _mediator.Send(new GetDoctorByIdQuery() { DoctorId = id });

            return Ok(result);
        }

        [HttpGet("email/{email}")]
        [ProducesResponseType(typeof(DoctorDTO), StatusCodes.Status200OK)]
        public async Task<ActionResult<DoctorDTO>> GetDoctorByEmail(string email)
        {
            var result = await _mediator.Send(new GetDoctorByEmailQuery() { Email = email });

            return Ok(result);
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<ActionResult> DeleteDoctorById(int id)
        {
            await _mediator.Send(new DeleteDoctorCommand() { Id = id });

            return NoContent();
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult> CreateDoctor([FromBody] CreateDoctorCommand createDoctor)
        {
            var result = await _mediator.Send(createDoctor);

            return CreatedAtAction(nameof(GetDoctorById), new { id = result.doctorId }, result);
        }

        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult> UpdateDoctor([FromBody] UpdateDoctorCommand updateDoctor)
        {
            await _mediator.Send(updateDoctor);

            return Ok();
        }
    }
}
