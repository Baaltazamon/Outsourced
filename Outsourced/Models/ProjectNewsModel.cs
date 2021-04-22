using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Outsourced.Models
{
    public class ProjectNewsModel
    {
        public int Id { get; set; }
        public string Header { get; set; }
        public string SubHeader { get; set; }
        public DateTime DateRelease { get; set; }
        public string Description { get; set; }
        public string BackGround { get; set; }
    }
}
