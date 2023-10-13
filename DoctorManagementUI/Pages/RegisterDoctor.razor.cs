using DoctorManagementUI.ViewModels;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using System.Net.Http.Json;

namespace DoctorManagementUI.Pages
{
    public partial class RegisterDoctor
    {
        public RegisterDoctorVM registerDoctorVM { get; set; }
        public SaveDoctorIdentity saveIdentityRequest { get; set; }
        public RegisterDoctorRequest registerDoctorRequest { get; set; }

        [Inject]
        public HttpClient _httpClient { get; set; }

        [Inject]
        private IJSRuntime jSRuntime { get; set; }

        [Inject]
        public NavigationManager _navigationManager { get; set; }

        protected override async Task OnInitializedAsync()
        {
            registerDoctorVM = new RegisterDoctorVM();
            saveIdentityRequest = new SaveDoctorIdentity();
            registerDoctorRequest = new RegisterDoctorRequest();
        }

        protected async void HandleValidSubmit()
        {
            saveIdentityRequest.FirstName = registerDoctorVM.FirstName;
            saveIdentityRequest.LastName = registerDoctorVM.LastName;
            saveIdentityRequest.Email = registerDoctorVM.Email;
            saveIdentityRequest.Password = registerDoctorVM.Password;
            saveIdentityRequest.UserName = registerDoctorVM.PersonalNumber;

            registerDoctorRequest.PersonalNumber = registerDoctorVM.PersonalNumber;
            registerDoctorRequest.FirstName = registerDoctorVM.FirstName;
            registerDoctorRequest.LastName = registerDoctorVM.LastName;
            registerDoctorRequest.Specialization = registerDoctorVM.Specialization;
            registerDoctorRequest.Email = registerDoctorVM.Email;
            registerDoctorRequest.PhoneNumber = registerDoctorVM.PhoneNumber;
            registerDoctorRequest.State = registerDoctorVM.State;
            registerDoctorRequest.City = registerDoctorVM.City;
            registerDoctorRequest.Country = registerDoctorVM.Country;
            registerDoctorRequest.StreetName = registerDoctorVM.StreetName;
            registerDoctorRequest.PostalCode = registerDoctorVM.PostalCode;

            var registerDoctorResponse = await _httpClient.PostAsJsonAsync<SaveDoctorIdentity>("https://localhost:7137/register", saveIdentityRequest);


            if (registerDoctorResponse.IsSuccessStatusCode)
            {
                var saveDoctorResponse = await _httpClient.PostAsJsonAsync<RegisterDoctorRequest>("https://localhost:7137/api/doctor", registerDoctorRequest);

                if (saveDoctorResponse.IsSuccessStatusCode)
                {
                    await jSRuntime.InvokeVoidAsync("alert", "U regjistruat me sukses");
                    _navigationManager.NavigateTo("/login", true);
                }
            }
            else
            {
                await jSRuntime.InvokeVoidAsync("alert", "Ndodhi nje problem");
            }
        }


    }
}
