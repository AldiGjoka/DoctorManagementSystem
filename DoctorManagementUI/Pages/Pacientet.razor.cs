using DoctorManagementUI.Auth;
using DoctorManagementUI.ViewModels;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using System.Net.Http.Json;

namespace DoctorManagementUI.Pages
{
    public partial class Pacientet
    {
        public List<PacientetResponseVM> Pacient;

        [Inject]
        public HttpClient _httpClient { get; set; }

        [Inject]
        private AuthenticationStateProvider authenticationStateProvider { get; set; }

        protected override async Task OnInitializedAsync()
        {
            Pacient = new List<PacientetResponseVM>();

            try
            {
                var doctorId = await ((CustomAuthenticationStateProvider)authenticationStateProvider).GetDoctorId();

                var response = await _httpClient.GetFromJsonAsync<List<PacientetResponseVM>>($"https://localhost:7137/api/patient/{doctorId}");

                Pacient = response;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            
        }
    }
}
