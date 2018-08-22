using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Microsoft.AspNet.Identity;
using ReadySetBake.DAL;
using ReadySetBake.Models;

namespace ReadySetBake.Controllers.Api
{
    public class HomeController : ApiController
    {
        [Route("api/Result/Score")]
        [HttpGet]
        public bool GetRunScoring()
        {
            bool returnValue;
            try
            {
                using (var context = new RsbContext())
                {
                    var week = context.Weeks.Include("StarBaker").Include("ExitBaker").OrderBy(o => o.Order).First(o => o.Scored == false && o.EndDateTime < DateTime.UtcNow);
                    ScoreWeek(context, week);

                    returnValue = true;
                }
            }
            catch
            {
                returnValue = false;
            }
            return returnValue;
        }

        private void ScoreWeek(RsbContext context, Week week)
        {
            var users = context.Users.Include("Predictions");
            foreach (var user in users)
            {
                var currentScore = user.CurrentScore;
                var newScore = user.CurrentScore;
                var weekPrediction = user.Predictions.SingleOrDefault(o => o.WeekId == week.Id);
                if (weekPrediction == null)
                    break;

                if (week.ExitBaker.Id == weekPrediction.ExitBakerId)
                    newScore++;
                if (week.ExitBaker.Id == weekPrediction.StarBakerId)
                    newScore--;
                if (week.StarBaker.Id == weekPrediction.StarBakerId)
                    newScore++;
                if (week.StarBaker.Id == weekPrediction.ExitBakerId)
                    newScore--;

                if (user.PreviousScores == null)
                    user.PreviousScores = new List<int>();

                user.PreviousScores.Add(currentScore);
                user.CurrentScore = newScore;
            }

            week.Scored = true;
            context.SaveChanges();
        }

        [Route("api/Result/ScoreAll")]
        [HttpGet]
        public bool RunAllScoring()
        {
            try
            {
                List<Week> weeks; 
                using (RsbContext context = new RsbContext())
                {
                    weeks = context.Weeks.Include("StarBaker").Include("ExitBaker").OrderBy(o => o.Order).Where(o => o.StarBaker != null && o.ExitBaker != null && !o.Scored).ToList();
                }

                foreach (var week in weeks)
                {
                    var context = new RsbContext();
                    ScoreWeek(context, week);
                }

                return true;
            }
            catch (Exception ex)
            {
               return false;
            }
        }

        [Route("api/Result/Star/{weekId}/{bakerId}")]
        public bool GetStar(Guid weekId, Guid bakerId)
        {
            using (var context = new RsbContext())
            {
                var week = context.Weeks.SingleOrDefault(o => o.Id == weekId);
                var starBaker = context.Bakers.Single(o => o.Id == bakerId);

                if (week != null && starBaker != null)
                {
                    week.StarBaker = starBaker;
                    context.SaveChanges();
                    return true;
                }
                return false;
            }
        }

        [Route("api/Result/Exit/{weekId}/{bakerId}")]
        public bool GetExit(Guid weekId, Guid bakerId)
        {
            using (var context = new RsbContext())
            {
                var week = context.Weeks.SingleOrDefault(o => o.Id == weekId);
                var exitBaker = context.Bakers.Single(o => o.Id == bakerId);

                if (week != null && exitBaker != null)
                {
                    week.ExitBaker = exitBaker;
                    context.SaveChanges();

                    return true;
                }
                return false;
            }
        }

        [Route("api/Prediction/Star/{userId}/{weekId}/{bakerId}")]
        public bool GetStar(Guid userId, Guid weekId, Guid bakerId)
        {
            using (var context = new RsbContext())
            {
                var week = context.Weeks.SingleOrDefault(o => o.Id == weekId);

                if (week == null || week.EndDateTime < DateTime.UtcNow)
                    return false;
                
                var user = context.Users.Include("Predictions").Single(o => o.UserId == userId);
                var pred = user.Predictions.SingleOrDefault(o => o.WeekId == weekId);
                
                if (pred == null)
                {
                    pred = new Prediction
                    {
                        StarBakerId = bakerId,
                        PredictionId = Guid.NewGuid(),
                        WeekId = weekId
                    };
                    user.Predictions.Add(pred);
                }
                else
                {
                    pred.StarBakerId = bakerId;
                    if (pred.ExitBakerId == pred.StarBakerId)
                        pred.ExitBakerId = Guid.Empty;
                }
                context.SaveChanges();
                return true;
            }
        }

        [Route("api/Prediction/Exit/{userId}/{weekId}/{bakerId}")]
        public bool GetExit(Guid userId, Guid weekId, Guid bakerId)
        {
            using (var context = new RsbContext())
            {
                var week = context.Weeks.SingleOrDefault(o => o.Id == weekId);

                if (week == null || week.EndDateTime < DateTime.UtcNow)
                    return false;

                var user = context.Users.Include("Predictions").Single(o => o.UserId == userId);
                var pred = user.Predictions.SingleOrDefault(o => o.WeekId == weekId);

                if (pred == null)
                {
                    pred = new Prediction
                    {
                        ExitBakerId = bakerId,
                        PredictionId = Guid.NewGuid(),
                        WeekId = weekId
                    };
                    user.Predictions.Add(pred);
                }
                else
                {
                    pred.ExitBakerId = bakerId;

                    if (pred.ExitBakerId == pred.StarBakerId)
                        pred.StarBakerId = Guid.Empty;
                }
                context.SaveChanges();
                return true;
            }
        }

        [Route("api/League/Create/{userId}/{name}")]
        public bool GetLeagueCreate(Guid userId, string name)
        {
            using (var context = new RsbContext())
            {
                var code = new Random().Next(0,9999).ToString(); // TODO: Generate code util 
                var league = new League {Code = code, Name = name, Users = new List<User>(), Id = Guid.NewGuid()};
                league.Users.Add(context.Users.Single(o => o.UserId == userId));
                context.Leagues.Add(league);
                context.SaveChanges();
                return true;
            }
        }

        [Route("api/League/Join/{userId}/{code}")]
        public bool GetLeagueJoin(Guid userId, string code)
        {
            using (var context = new RsbContext())
            {
                var league = context.Leagues.SingleOrDefault(o => o.Code == code);
                if (league == null)
                    return false;
                league.Users.Add(context.Users.Single(o => o.UserId == userId));
                context.SaveChanges();
                return true;
            }
        }

        [Route("api/League/Leave/{userId}/{leagueId}")]
        public bool GetLeagueLeave(Guid userId, Guid leagueId)
        {
            using (var context = new RsbContext())
            {
                var league = context.Leagues.Single(o => o.Id == leagueId);
                league.Users.RemoveAll(o => o.UserId == userId);
                context.SaveChanges();
                return true;
            }
        }
    }
}