using System;

namespace ReadySetBake.Models
{
    public class WeekViewModel
    {
        public int Order { get; set; }
        public Week Week { get; set; }
        public Guid PredictedStarBakerId { get; set; }
        public Guid PredictedExitBakerId { get; set; }
        public DateTime EndOfWeekTime { get; set; }
        public Guid? ActualStarBaker { get; set; }
        public Guid? ActualExitBaker { get; set; }
    }
}