using System;
using System.Collections.Generic;

#nullable disable

namespace DoctorFinderProject.Models
{
    public partial class Statetbl
    {
        public Statetbl()
        {
            Citytbls = new HashSet<Citytbl>();
            Hospitaltbls = new HashSet<Hospitaltbl>();
            Pateinttbls = new HashSet<Pateinttbl>();
        }

        public int StateId { get; set; }
        public string StateName { get; set; }

        public virtual ICollection<Citytbl> Citytbls { get; set; }
        public virtual ICollection<Hospitaltbl> Hospitaltbls { get; set; }
        public virtual ICollection<Pateinttbl> Pateinttbls { get; set; }
    }
}
