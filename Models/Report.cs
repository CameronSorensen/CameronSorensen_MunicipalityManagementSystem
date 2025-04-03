using System;

namespace MunicipalityManagementSystem.Models
{
    public class Report
    {
        public int ReportID { get; set; }
        public int CitizenID { get; set; }
        public string ReportType { get; set; }
        public string Details { get; set; }
        public DateTime SubmissionDate { get; set; } = DateTime.Now;
        public string Status { get; set; } = "Under Review";
    }
}
