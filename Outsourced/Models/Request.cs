using System;
using System.Collections.Generic;

#nullable disable

namespace Outsourced.Models
{
    public partial class Request
    {
        public int Id { get; set; }
        public string Type { get; set; }
        public string Wishes { get; set; }
        public string TechnicalTask { get; set; }
        public string UserRequest { get; set; }
        public string Administrator { get; set; }

        public virtual Administrator AdministratorNavigation { get; set; }
        public virtual TypeRequest TypeNavigation { get; set; }
        public virtual User UserRequestNavigation { get; set; }
    }
}
