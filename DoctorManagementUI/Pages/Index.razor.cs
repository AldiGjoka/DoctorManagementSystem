using DoctorManagementUI.Auth;
using DoctorManagementUI.ViewModels;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using System.Net.Http.Json;

namespace DoctorManagementUI.Pages
{
    public partial class Index
    {
        public List<AppointmentResponseVM>? appointments;

        [Inject]
        public HttpClient _httpClient { get; set; }

        [Inject]
        private AuthenticationStateProvider authenticationStateProvider { get; set; }

        protected override async Task OnInitializedAsync()
        {
            var doctorId = await ((CustomAuthenticationStateProvider)authenticationStateProvider).GetDoctorId();

            if (!string.IsNullOrEmpty(doctorId))
            {
                appointments = await _httpClient
                .GetFromJsonAsync<List<AppointmentResponseVM>>($"https://localhost:7137/api/appointment/doctor/{doctorId}");
            }
        }
    }
}
