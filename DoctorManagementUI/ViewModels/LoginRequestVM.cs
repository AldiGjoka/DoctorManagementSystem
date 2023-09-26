using System.Text.Json.Serialization;

namespace DoctorManagementUI.ViewModels
{
    public class LoginRequestVM
    {
        [JsonPropertyName("email")]
        public string Email { get; set; }
        [JsonPropertyName("password")]
        public string Password { get; set; }
    }
}
