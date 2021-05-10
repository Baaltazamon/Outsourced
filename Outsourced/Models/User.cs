using System;
using System.Collections.Generic;

#nullable disable

namespace Outsourced.Models
{
    public partial class User
    {
        public User()
        {
            Requests = new HashSet<Request>();
        }

        public string Email { get; set; }
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }

        public virtual ICollection<Request> Requests { get; set; }
    }
}
