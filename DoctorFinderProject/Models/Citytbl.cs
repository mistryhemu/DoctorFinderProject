using System;
using System.Collections.Generic;

#nullable disable

namespace DoctorFinderProject.Models
{
    public partial class Citytbl
    {
        public Citytbl()
        {
            Hospitaltbls = new HashSet<Hospitaltbl>();
            Pateinttbls = new HashSet<Pateinttbl>();
        }

        public int CityId { get; set; }
        public string CityName { get; set; }
        public int StateId { get; set; }

        public virtual Statetbl State { get; set; }
        public virtual ICollection<Hospitaltbl> Hospitaltbls { get; set; }
        public virtual ICollection<Pateinttbl> Pateinttbls { get; set; }
    }
}
