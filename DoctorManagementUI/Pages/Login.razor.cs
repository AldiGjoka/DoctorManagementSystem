using DoctorManagementUI.Auth;
using DoctorManagementUI.ViewModels;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.JSInterop;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;

namespace DoctorManagementUI.Pages
{
    public partial class Login
    {
        public LoginRequestVM LoginRequest { get; set; }
        public string Message { get; set; }

        [Inject]
        public HttpClient _httpClient { get; set; }

        [Inject]
        public NavigationManager _navigationManager { get; set; }

        [Inject]
        private AuthenticationStateProvider authenticationStateProvider { get; set; }

        [Inject]
        private IJSRuntime jSRuntime { get; set; }

        public Login()
        {

        }

        protected override void OnInitialized()
        {
            LoginRequest = new LoginRequestVM();
        }


        protected async void HandleValidSubmit()
        {
            try
            {
                var serialize = JsonSerializer.Serialize(LoginRequest);

                var payload = new StringContent(serialize, Encoding.UTF8, "application/json");

                var loginResponse = await _httpClient.PostAsync("https://localhost:7137/api/authenticate", payload);

                if (loginResponse.IsSuccessStatusCode)
                {
                    var response = await loginResponse.Content.ReadFromJsonAsync<LoginResponseVM>();
                    if(response != null)
                    {
                        response.Id = await GetDoctorId(response.Email);
                    }
                    var customAuth = (CustomAuthenticationStateProvider)authenticationStateProvider;

                    await customAuth.UpdateAuthenticationState(response);

                    _navigationManager.NavigateTo("/", true);
                }
                else
                {
                    await jSRuntime.InvokeVoidAsync("alert", "UserName or password is incorrect");
                    return;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            
        }

        protected async Task<string> GetDoctorId(string email)
        {
            var doctor = await _httpClient.GetFromJsonAsync<DoctorResponseVM>($"https://localhost:7137/api/doctor/email/{email}");

            return doctor.doctorId.ToString();
        }
    }
}
