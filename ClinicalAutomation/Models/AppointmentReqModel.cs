using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ClinicalAutomation.Models
{
    public class AppointmentReqModel
    {
        public int AppointmentRequestId { get; set; }
        public string PatientName { get; set; }
        public int PatientId { get; set; }
        public string Criticality { get; set; }
        public string Reason { get; set; }
        public DateTime ScheduledDate { get; set; }
        public string Status { get; set; } // Pending by default
    }
}