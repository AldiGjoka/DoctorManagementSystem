using Application.Common.Interfaces.Auth;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.AuthService.Command.AuthUser
{
    public record AuthUserCommand : IRequest<AuthResponse>
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }

    public class AuthUserCommandHandler : IRequestHandler<AuthUserCommand, AuthResponse>
    {
        private readonly IAuthService _authService;
        public AuthUserCommandHandler(IAuthService authService)
        {
            _authService = authService;
        }

        public async Task<AuthResponse> Handle(AuthUserCommand request, CancellationToken cancellationToken)
        {
            var user = new AuthRequest() { Email = request.Email, Password = request.Password };

            var result = await _authService.AuthenticateAsync(user);

            return result;
        }
    }
}
