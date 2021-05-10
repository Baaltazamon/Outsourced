using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

#nullable disable

namespace Outsourced.Models
{
    public partial class ProjectNews
    {
        public int Id { get; set; }
        public string Header { get; set; }
        public string SubHeader { get; set; }
        public string Description { get; set; }
        public string BackGround { get; set; }
        [DisplayFormat(DataFormatString = "{0:d}")]
        public DateTime DateRelease { get; set; }
        public string Administrator { get; set; }
    }
}
