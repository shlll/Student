using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Student.Models
{
    public class StudentAddress
    {
        public int Id { get; set; }
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public string City { get; set; }
        public int Zipcode { get; set; }
        public string State { get; set; }
        public string Country { get; set; }
        public virtual Students Student { get; set; }
        public int StudentId { get; set; }
        public virtual ApplicationUser User { get; set; }
        public string UserId { get; set; }

    }
}