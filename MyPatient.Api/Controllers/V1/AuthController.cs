using Application.Features.AuthService.Command.AuthUser;
using Application.Features.AuthService.Command.RegisterUser;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MyPatient.Api.Controllers.V1
{
    [Route("api/authenticate")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IMediator _mediator;
        public AuthController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost()]
        public async Task<ActionResult<AuthResponse>> Authenticate([FromBody] AuthUserCommand user)
        {
            var result = await _mediator.Send(user);

            return Ok(result);

        }

        [HttpPost("/register")]
        public async Task<ActionResult<RegistrationResponse>> Registrate([FromBody] RegistrateUserCommand request)
        {
            var result = await _mediator.Send(request);

            return Ok(result);
        }
    }
}
