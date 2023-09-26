using Blazored.SessionStorage;
using DoctorManagementUI.Extensions;
using DoctorManagementUI.ViewModels;
using Microsoft.AspNetCore.Components.Authorization;
using System.Security.Claims;

namespace DoctorManagementUI.Auth
{
    public class CustomAuthenticationStateProvider : AuthenticationStateProvider
    {
        private readonly ISessionStorageService _sessionStorage;
        private ClaimsPrincipal _anonymus = new ClaimsPrincipal(new ClaimsIdentity());

        public CustomAuthenticationStateProvider(ISessionStorageService sessionStorage)
        {
            _sessionStorage = sessionStorage;
        }

        public override async Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            try
            {
                var userSession = await _sessionStorage.ReadItemEncryptedAsync<LoginResponseVM>("UserSession");
                if(userSession == null)
                    return await Task.FromResult(new AuthenticationState(_anonymus));

                var claimsPrincipal = new ClaimsPrincipal(new ClaimsIdentity(new List<Claim>
                {
                    new Claim(ClaimTypes.Name, userSession.UserName),
                    new Claim("token", userSession.Token),
                    new Claim(ClaimTypes.Email, userSession.Email),
                    new Claim("Id", userSession.Id),
                    new Claim(ClaimTypes.Role, userSession.UserName)
                }, "JwtAuth"));

                return await Task.FromResult(new AuthenticationState(claimsPrincipal));
            }
            catch
            {
                return await Task.FromResult(new AuthenticationState(_anonymus));
            }
        }

        public async Task UpdateAuthenticationState(LoginResponseVM? userSession)
        {
            ClaimsPrincipal claimsPrincipal;

            if(userSession != null)
            {
                claimsPrincipal = new ClaimsPrincipal(new ClaimsIdentity(new List<Claim>
                {
                    new Claim(ClaimTypes.Name, userSession.UserName),
                    new Claim("token", userSession.Token),
                    new Claim(ClaimTypes.Email, userSession.Email),
                    new Claim("Id", userSession.Id),
                    new Claim(ClaimTypes.Role, userSession.UserName)
                }));
                await _sessionStorage.SaveItemEncryptedAsync("UserSession", userSession);
            }
            else
            {
                claimsPrincipal = _anonymus;
                await _sessionStorage.RemoveItemAsync("UserSession");
            }

            NotifyAuthenticationStateChanged(Task.FromResult(new AuthenticationState(claimsPrincipal)));
        }

        public async Task<string> GetToken()
        {
            var result = string.Empty;

            try
            {
                var userSession = await _sessionStorage.ReadItemEncryptedAsync<LoginResponseVM>("UserSession");
                result = userSession.Token;
            }
            catch
            {
            }

            return result;
        }

        public async Task<string> GetDoctorId()
        {
            var result = string.Empty;

            try
            {
                var userSession = await _sessionStorage.ReadItemEncryptedAsync<LoginResponseVM>("UserSession");
                result = userSession.Id;
            }
            catch
            {
            }

            return result;
        }
    }
}
