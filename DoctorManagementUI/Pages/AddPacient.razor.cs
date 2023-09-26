using DoctorManagementUI.Auth;
using DoctorManagementUI.ViewModels;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.JSInterop;
using System.Net.Http.Json;

namespace DoctorManagementUI.Pages
{
    public partial class AddPacient
    {
        public PacientRequestVM Pacient { get; set; }

        public string Message { get; set; }

        [Inject]
        public HttpClient _httpClient { get; set; }

        [Inject]
        private AuthenticationStateProvider authenticationStateProvider { get; set; }

        [Inject]
        private IJSRuntime jSRuntime { get; set; }



        protected override async Task OnInitializedAsync()
        {
            Pacient = new PacientRequestVM();
        }

        protected async void HandleValidSubmit()
        {
            Pacient.DoctorId = Int32.Parse(await ((CustomAuthenticationStateProvider)authenticationStateProvider).GetDoctorId());

            var response = await _httpClient.PostAsJsonAsync<PacientRequestVM>("https://localhost:7137/api/patient", Pacient);

            if (response.IsSuccessStatusCode)
            {
                await jSRuntime.InvokeVoidAsync("alert", "UserName or password is incorrect");
                Pacient = new PacientRequestVM();
            }
            else
            {
                Message = "Ndodhi nje problem";
            }
        }
    }
}
