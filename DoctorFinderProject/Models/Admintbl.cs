using System;
using System.Collections.Generic;

#nullable disable

namespace DoctorFinderProject.Models
{
    public partial class Admintbl
    {
        public int AdminId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
