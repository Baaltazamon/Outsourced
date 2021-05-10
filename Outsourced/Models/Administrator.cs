using System;
using System.Collections.Generic;

#nullable disable

namespace Outsourced.Models
{
    public partial class Administrator
    {
        public Administrator()
        {
            Requests = new HashSet<Request>();
        }

        public string UserName { get; set; }
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string Password { get; set; }

        public virtual ICollection<Request> Requests { get; set; }
    }
}
