namespace DoctorManagementUI.ViewModels
{
    public class AppointmentRequestVM
    {
        public int PatientId { get; set; }
        public DateTime AppointmentDate { get; set; }

        public string StartTime { get; set; }
        public string EndTime { get; set; }
    }
}
