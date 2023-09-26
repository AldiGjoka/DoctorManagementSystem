using DoctorManagementUI.ViewModels;
using Microsoft.AspNetCore.Components;
using System.Net.Http.Json;

namespace DoctorManagementUI.Components
{
    public partial class AppointmentComponent
    {
        [Parameter]
        public List<AppointmentResponseVM>? Appointment { get; set; }
    }
}
