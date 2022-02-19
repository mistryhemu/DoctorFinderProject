using System;
using System.Collections.Generic;

#nullable disable

namespace DoctorFinderProject.Models
{
    public partial class Doctortbl
    {
        public Doctortbl()
        {
            Appointmenttbls = new HashSet<Appointmenttbl>();
        }

        public int DoctorId { get; set; }
        public int HospitalId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string PhoneNo { get; set; }
        public string ProfileImage { get; set; }
        public string Degree { get; set; }
        public string Speciality { get; set; }

        public virtual Hospitaltbl Hospital { get; set; }
        public virtual ICollection<Appointmenttbl> Appointmenttbls { get; set; }
    }
}
