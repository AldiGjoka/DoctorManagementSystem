using Application.Features.AuthService.Command.AuthUser;
using Application.Features.AuthService.Command.RegisterUser;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Common.Interfaces.Auth
{
    public interface IAuthService
    {
        Task<AuthResponse> AuthenticateAsync(AuthRequest request);
        Task<RegistrationResponse> RegisterAsync(RegistrationRequest request);
    }
}
