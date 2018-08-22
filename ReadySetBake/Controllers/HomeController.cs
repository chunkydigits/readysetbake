using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using Microsoft.Ajax.Utilities;
using Microsoft.AspNet.Identity;
using ReadySetBake.DAL;
using ReadySetBake.Models;

namespace ReadySetBake.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Initialise()
        {
            var rsb = new RsbInitializer();
            rsb.initialiseSeed();
            return Home();
        }

        [Authorize]
        public ActionResult Home()
        {
            ViewBag.Message = "Ready, Set, Bake!";
            
            var userId = GetId(); 
            var viewModel = new ViewModel { UserId = userId.Value };

            using (var context = new RsbContext())
            {
                
                viewModel.Leagues = context.Leagues.Include("Users").Where(o => o.Users.Any(x => x.UserId == userId)).ToList();
                viewModel.Bakers = context.Bakers.ToList();

                var pastWeeks = context.Weeks.Include("StarBaker").Include("ExitBaker").ToList();
                var lastWeek = pastWeeks.OrderByDescending(o => o.Order).Where(o => o.StarBaker != null && o.ExitBaker != null).FirstOrDefault(o => o.EndDateTime < DateTime.UtcNow);
                viewModel.LastWeekStarBaker = lastWeek.StarBaker?.Name;
                viewModel.LastWeekExitBaker = lastWeek.ExitBaker?.Name;

                var weeks = context.Weeks.ToList();

                viewModel.Weeks = new List<WeekViewModel>();
                int iterator = 1;
                foreach (var week in weeks.OrderBy(o => o.Order))
                {
                    var weekVM = new WeekViewModel { Week = week };
                    weekVM.ActualStarBaker = week.StarBaker != null ? week.StarBaker.Id : (Guid?)null;
                    weekVM.ActualExitBaker = week.ExitBaker != null ? week.ExitBaker.Id : (Guid?)null;
                    weekVM.Order = iterator;

                    viewModel.Weeks.Add(weekVM);
                }

                viewModel.Leagues.Add(new League { Id = Guid.NewGuid(), Users = context.Users.OrderByDescending(o => o.CurrentScore).ToList(), Name = "All Users" });

                if (lastWeek.Order == 1)
                {
                    return View(viewModel);
                }

                var user = context.Users.Include("Predictions").Single(o => o.UserId == userId);
                var predictions = user.Predictions;
                
                viewModel.Score = user.CurrentScore;

                if(user.PreviousScores != null)
                    viewModel.PreviousScore = user.PreviousScores.LastOrDefault();

                iterator = 1;
                foreach (var week in viewModel.Weeks.OrderBy(o => o.Order))
                {
                    if (predictions != null && predictions.Count != 0)
                    {
                        var prediction = predictions.SingleOrDefault(o => o.WeekId == week.Week.Id);
                        if (prediction != null)
                        {
                            if (prediction.StarBakerId != null && prediction.StarBakerId != Guid.Empty)
                                week.PredictedStarBakerId = prediction.StarBakerId;
                            if(prediction.ExitBakerId != null && prediction.ExitBakerId != Guid.Empty)
                                week.PredictedExitBakerId = prediction.ExitBakerId;
                        }
                    }
                }
            }
            return View(viewModel);
        }

        [Authorize]
        public ActionResult Contestants()
        {
            var viewModel = new BakerViewModel();
            

            ViewBag.Message = "The GBBO Contestants 2015";
            using (var context = new RsbContext())
            {
                viewModel.Bakers = context.Bakers.ToList();
                viewModel.Weeks = context.Weeks.ToList();
            }

            return View(viewModel);
        }

        [Authorize]
        public ActionResult Admin()
        {
            ViewBag.Message = "Get Set, Bake!";

            var userId = GetId();
            var viewModel = new ViewModel { UserId = userId.Value };

            using (var context = new RsbContext())
            {
                viewModel.Leagues = context.Leagues.Include("Users").Where(o => o.Users.Any(x => x.UserId == userId)).ToList();
                viewModel.Bakers = context.Bakers.ToList();

                var pastWeeks = context.Weeks.Include("StarBaker").Include("ExitBaker").ToList();
                var lastWeek = pastWeeks.OrderByDescending(o => o.Order).First(o => o.EndDateTime < DateTime.UtcNow);

                if (lastWeek.StarBaker != null)
                    viewModel.LastWeekStarBaker = lastWeek.StarBaker.Name;

                if (lastWeek.ExitBaker != null)
                    viewModel.LastWeekExitBaker = lastWeek.ExitBaker.Name;

                var user = context.Users.Include("Predictions").Single(o => o.UserId == userId);
                var predictions = user.Predictions;
                var weeks = context.Weeks.ToList();

                viewModel.Weeks = new List<WeekViewModel>();
                int iterator = 1;
                foreach (var week in weeks.OrderBy(o => o.Order))
                {
                    var weekVM = new WeekViewModel { Week = week };
                    weekVM.ActualStarBaker = week.StarBaker != null ? week.StarBaker.Id : (Guid?)null;
                    weekVM.ActualExitBaker = week.ExitBaker != null ? week.ExitBaker.Id : (Guid?)null;
                    weekVM.Order = iterator;

                    if (predictions != null && predictions.Count != 0)
                    {
                        var prediction = predictions.SingleOrDefault(o => o.WeekId == week.Id);
                        if (prediction != null)
                        {
                            if (prediction.StarBakerId != null && prediction.StarBakerId != Guid.Empty)
                                weekVM.PredictedStarBakerId = prediction.StarBakerId;
                            if (prediction.ExitBakerId != null && prediction.ExitBakerId != Guid.Empty)
                                weekVM.PredictedExitBakerId = prediction.ExitBakerId;
                        }
                    }

                    viewModel.Weeks.Add(weekVM);
                }
                viewModel.Leagues.Add(new League { Id = Guid.NewGuid(), Users = context.Users.OrderByDescending(o => o.CurrentScore).ToList(), Name = "All Users" });
            }

            return View(viewModel);
        }

        private Guid? GetId()
        {
            var claimsIdentity = User.Identity as ClaimsIdentity;
            if (claimsIdentity != null)
            {
                var userIdClaim = claimsIdentity.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier);

                if (userIdClaim != null)
                {
                    return new Guid(userIdClaim.Value);
                }
            }
            return null;
        }
    }
}