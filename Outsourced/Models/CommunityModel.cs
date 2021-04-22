using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Outsourced.Models
{
    public class CommunityModel
    {
        public int Id { get; set; }
        public string Header { get; set; }
        public string TypeCommunity { get; set; }
        public DateTime DateWebinar { get; set; }
        public string Description { get; set; }

        public string ColorType
        {
            get { return ColorType; }
            set
            {
                if (TypeCommunity == "WEB community")
                    ColorType = "company-blue";
                else if (TypeCommunity == "Developer community")
                    ColorType = "company-pink";
                else
                    ColorType = "company-green";
            }
        }

    }
}
