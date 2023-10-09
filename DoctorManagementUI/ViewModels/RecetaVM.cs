namespace DoctorManagementUI.ViewModels
{
    public class RecetaVM
    {
        public int PatientId { get; set; }
        public List<PrescriptionVM> Prescriptions { get; set; }
        public DateTime Date { get; set; }
    }
}
