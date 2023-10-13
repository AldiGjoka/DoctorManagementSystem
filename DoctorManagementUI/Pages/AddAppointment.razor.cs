using DoctorManagementUI.ViewModels;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using System.Net.Http.Json;

namespace DoctorManagementUI.Pages
{
    public partial class AddAppointment
    {
        public AppointmentRequestVM Appointment;

        [Parameter]
        public string PatientId { get; set; }

        [Inject]
        public HttpClient _httpClient { get; set; }

        [Inject]
        private IJSRuntime jSRuntime { get; set; }

        protected override async Task OnInitializedAsync()
        {
            Appointment = new AppointmentRequestVM();
            Appointment.AppointmentDate = DateTime.Now;
        }

        protected async void HandleValidSubmit()
        {
            Appointment.PatientId = Int32.Parse(PatientId);


            var response = await _httpClient.PostAsJsonAsync<AppointmentRequestVM>($"https://localhost:7137/api/appointment/{PatientId}", 
                Appointment);

            if (response.IsSuccessStatusCode)
            {
                await jSRuntime.InvokeVoidAsync("alert", "U ruajt me sukses");
            }
            else
            {
                await jSRuntime.InvokeVoidAsync("alert", "Ndodhi nje problem");
            }
        }
    }
}
