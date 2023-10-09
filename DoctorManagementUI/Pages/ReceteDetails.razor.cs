using DoctorManagementUI.ViewModels;
using Microsoft.AspNetCore.Components;
using System.Net.Http.Json;

namespace DoctorManagementUI.Pages
{
    public partial class ReceteDetails
    {
        public List<RecetaVM> Recete;

        [Parameter]
        public string PatientId { get; set; }

        [Inject]
        public HttpClient _httpClient { get; set; }

        [Inject]
        NavigationManager _navigationManager { get; set; }

        protected override async Task OnInitializedAsync()
        {
            Recete = new List<RecetaVM>();

            try
            {

                var response = await _httpClient.GetFromJsonAsync<List<RecetaVM>>($"https://localhost:7137/api/receta/{PatientId}");

                Recete = response;
            }
            catch (Exception ex)
            {

                throw;
            }
        }
    }
}
