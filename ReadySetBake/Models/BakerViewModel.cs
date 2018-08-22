using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ReadySetBake.Models
{
    public class BakerViewModel
    {
        public List<Week> Weeks { get; set; } 
        public List<Baker> Bakers { get; set; }
    }
}