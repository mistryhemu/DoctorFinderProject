using System;
using System.Collections.Generic;

#nullable disable

namespace DoctorFinderProject.Models
{
    public partial class Pateinttbl
    {
        public int PateintId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Mobile { get; set; }
        public string Password { get; set; }
        public string ProfileImage { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public string Address { get; set; }
        public int StateId { get; set; }
        public int CityId { get; set; }

        public virtual Citytbl City { get; set; }
        public virtual Statetbl State { get; set; }
    }
}
