using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ReadySetBake.Models
{
    public class League
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public virtual List<User> Users { get; set; } 
    }
}