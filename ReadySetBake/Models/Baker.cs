using System;
using System.Collections.Generic;

namespace ReadySetBake.Models
{
    public class Baker
    {
        public Guid Id { get; set; }
        public string Name { get; set; }

        public string FirstName
        {
            get { return Name.Split(' ')[0]; }
        }

        public int StarBakerWeeksTotal { get; set; }
        public virtual List<Week> Weeks { get; set; } 
        public Week ExitWeek { get; set; }
    }
}