using System;
using System.Collections.Generic;

#nullable disable

namespace DoctorFinderProject.Models
{
    public partial class Hospitaltbl
    {
        public Hospitaltbl()
        {
            Doctortbls = new HashSet<Doctortbl>();
        }

        public int HospitalId { get; set; }
        public string HospitalName { get; set; }
        public string Address { get; set; }
        public int StateId { get; set; }
        public int CityId { get; set; }
        public string ProfileImage { get; set; }
        public string ContactNo { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Description { get; set; }

        public virtual Citytbl City { get; set; }
        public virtual Statetbl State { get; set; }
        public virtual ICollection<Doctortbl> Doctortbls { get; set; }
    }
}
