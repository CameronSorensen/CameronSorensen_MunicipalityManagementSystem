using System;

namespace MunicipalityManagementSystem.Models
{
    public class Staff
    {
        public int StaffID { get; set; }
        public string FullName { get; set; }
        public string Position { get; set; }
        public string Department { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public DateTime HireDate { get; set; }
    }
}
