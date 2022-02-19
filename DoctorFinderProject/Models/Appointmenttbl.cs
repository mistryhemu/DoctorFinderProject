using System;
using System.Collections.Generic;

#nullable disable

namespace DoctorFinderProject.Models
{
    public partial class Appointmenttbl
    {
        public int AptId { get; set; }
        public int HospitalId { get; set; }
        public int PateintId { get; set; }
        public int DoctorId { get; set; }
        public DateTime AptDateTime { get; set; }
        public int? AptStatus { get; set; }

        public virtual Doctortbl Doctor { get; set; }
        public virtual Hospitaltbl Hospital { get; set; }
        public virtual Pateinttbl Pateint { get; set; }
    }
}
