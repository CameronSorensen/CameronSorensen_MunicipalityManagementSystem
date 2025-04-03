using System;

namespace MunicipalityManagementSystem.Models
{
    public class Citizen
    {
        public int CitizenID { get; set; }
        public string FullName { get; set; }
        public string Address { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public DateTime RegistrationDate { get; set; } = DateTime.Now;
    }
}
