using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

#nullable disable

namespace Outsourced.Models
{
    public partial class Community
    {
        public int Id { get; set; }
        public string Header { get; set; }
        public string Type { get; set; }
        [DisplayFormat(DataFormatString = "{0:d}")]
        public DateTime DateWebinar { get; set; }
        public string Description { get; set; }
        public string Administrator { get; set; }

        public virtual Administrator AdministratorNavigation { get; set; }
    }
}
