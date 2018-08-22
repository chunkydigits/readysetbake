using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ReadySetBake.Models
{
    public class Prediction
    {
        public Guid PredictionId { get; set; }
        public Guid WeekId { get; set; }
        public Guid StarBakerId { get; set; }
        public Guid ExitBakerId { get; set; }
    }
}