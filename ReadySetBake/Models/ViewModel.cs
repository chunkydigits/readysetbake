using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ReadySetBake.Models
{
    public class ViewModel
    {
        public Guid UserId { get; set; }
        public List<League> Leagues { get; set; }
        public List<Baker> Bakers { get; set; }
        public List<WeekViewModel> Weeks { get; set; }
        public string LastWeekStarBaker { get; set; }
        public string LastWeekExitBaker { get; set; }
        public int Score { get; set; }
        public int? PreviousScore { get; set; }
    }
}