namespace DoctorManagementUI.ViewModels
{
    public class DoctorResponseVM
    {
        public int doctorId { get; set; }
        public string PersonalNumber { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Specialization { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public AddressVM Address { get; set; }
    }

    public class AddressVM
    {
        public string State { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public string StreetName { get; set; }
        public string PostalCode { get; set; }
    }
}
