using System;
using System.Collections.Generic;

#nullable disable

namespace Outsourced.Models
{
    public partial class TypeRequest
    {
        public TypeRequest()
        {
            Requests = new HashSet<Request>();
        }

        public string NameTypeRequest { get; set; }

        public virtual ICollection<Request> Requests { get; set; }
    }
}
