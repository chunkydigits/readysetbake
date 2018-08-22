using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ReadySetBake.Models
{
    public class User
    {
        public Guid UserId { get; set; }
        public string Name { get; set; }
        public int CurrentScore { get; set; }
        public List<int> PreviousScores { get; set; }
        public virtual List<League> Leagues { get; set; }
        public List<Prediction> Predictions { get; set; }
        public Guid CurrentStarBakerGuid { get; set; }
        public Guid CurrentExitBakerGuid { get; set; }
    }
}