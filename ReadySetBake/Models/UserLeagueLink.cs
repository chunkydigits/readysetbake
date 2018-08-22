using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ReadySetBake.Models
{
    public class UserLeagueLink
    {
        public Guid UserLeagueLinkId { get; set; }
        public Guid UserId { get; set; }
        public Guid LeagueId { get; set; }
        public int CurrentPosition { get; set; }
        public int PreviousPosition { get; set; }
    }
}