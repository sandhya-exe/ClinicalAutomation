//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace ClinicalAutomation.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Appointment
    {
        public int AppointmentID { get; set; }
        public Nullable<int> PatientID { get; set; }
        public Nullable<int> PhysicianID { get; set; }
        public Nullable<System.DateTime> AppointmentDate { get; set; }
        public string Criticality { get; set; }
        public string Reason { get; set; }
        public string Note { get; set; }
        public string Status { get; set; }
        public Nullable<System.DateTime> ScheduleDateTime { get; set; }

        public virtual patient patient { get; set; }
        public virtual physician physician { get; set; }
    }
}
