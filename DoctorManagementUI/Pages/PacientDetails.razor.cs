using DoctorManagementUI.Auth;
using DoctorManagementUI.ViewModels;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using System.Net.Http.Json;

namespace DoctorManagementUI.Pages
{
    public partial class PacientDetails
    {
        public PacientetResponseVM Pacient;

        [Inject]
        public HttpClient _httpClient { get; set; }

        [Inject]
        private AuthenticationStateProvider authenticationStateProvider { get; set; }

        [Inject]
        NavigationManager _navigationManager { get; set; }

        [Parameter]
        public string PacientId { get; set; }


        protected override async Task OnInitializedAsync()
        {
            Pacient = new PacientetResponseVM();

            try
            {
                var doctorId = await ((CustomAuthenticationStateProvider)authenticationStateProvider).GetDoctorId();

                var response = await _httpClient.GetFromJsonAsync<PacientetResponseVM>($"https://localhost:7137/api/patient/{doctorId}/{PacientId}");

                Pacient = response;
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        protected void HandleShtoReceteClick(int patientId)
        {
            _navigationManager.NavigateTo($"/recete/{patientId}");
        }

        protected void HandleShikoRecetatClick(int patientId)
        {
            _navigationManager.NavigateTo($"/recete/details/{patientId}");
        }
    }
}
