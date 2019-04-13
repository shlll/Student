using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Student.Models
{
    public class Students
    {
        public int Id { get; set; }
        public string StudentName { get; set; }
        public virtual ApplicationUser User { get; set; }
        public string UserId { get; set; }
        public virtual List<StudentAddress> Address { get; set; }
    }
}