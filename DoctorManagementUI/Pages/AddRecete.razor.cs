using DoctorManagementUI.Auth;
using DoctorManagementUI.ViewModels;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.JSInterop;
using System.Net.Http.Json;

namespace DoctorManagementUI.Pages
{
    public partial class AddRecete
    {
        public RecetaVM Receta { get; set; }

        public List<PrescriptionVM> Prescriptions { get; set; }

        public int divCount = 0;

        public string Message { get; set; }

        [Parameter]
        public string PacientId { get; set; }

        [Inject]
        public HttpClient _httpClient { get; set; }

        [Inject]
        private AuthenticationStateProvider authenticationStateProvider { get; set; }

        [Inject]
        NavigationManager _navigationManager { get; set; }

        [Inject]
        private IJSRuntime jSRuntime { get; set; }


        protected override async Task OnInitializedAsync()
        {
            Receta = new RecetaVM();
            Receta.Prescriptions = new List<PrescriptionVM>();
            var pres = new PrescriptionVM();
            Receta.Prescriptions.Add(pres);
            var presTwo = new PrescriptionVM();
            Receta.Prescriptions.Add(presTwo);
        }


        protected async void HandleValidSubmit()
        {
            var date = DateTime.Now;
            Receta.Date = date;

            Receta.PatientId = Int32.Parse(PacientId);

            foreach(var pres in Receta.Prescriptions)
            {
                pres.Date = date;
            }

            var response = await _httpClient.PostAsJsonAsync<RecetaVM>($"https://localhost:7137/api/receta/{PacientId}", Receta);

            if (response.IsSuccessStatusCode)
            {
                await jSRuntime.InvokeVoidAsync("alert", "U ruajt me sukses");
                Receta = new RecetaVM();
            }
            else
            {
                await jSRuntime.InvokeVoidAsync("alert", "Ndodhi nje problem");
                Message = "Ndodhi nje problem";
            }
        }

    }
}
