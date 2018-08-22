using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ReadySetBake.Models
{
    public class Week
    {
        public Guid Id { get; set; }
        public int Order { get; set; }
        public string Name { get; set; }
        public Baker StarBaker { get; set; }
        public Baker ExitBaker { get; set; }
        public DateTime StartDateTime { get; set; }
        public DateTime EndDateTime { get; set; }
        public bool Scored { get; set; }
    }
}