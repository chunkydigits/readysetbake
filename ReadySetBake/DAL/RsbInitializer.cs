using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ReadySetBake.Models;
using ReadySetBake.DAL;

namespace ReadySetBake.DAL
{
    public class RsbInitializer : System.Data.Entity.DropCreateDatabaseAlways<RsbContext>
    {
        public void initialiseSeed()
        {
            this.Seed(new RsbContext());
        }

        protected override void Seed(RsbContext context)
        {
            var weeks = new List<Week>
            {
                new Week
                {
                    Id = Guid.NewGuid(),
                    Name = "Week 1",
                    Order = 1, 
                    StartDateTime = new DateTime(2016, 8, 24, 20, 0, 0), 
                    EndDateTime = new DateTime(2016, 8, 24, 19, 59, 59)
                },
                new Week
                {
                    Id = Guid.NewGuid(),
                    Name = "Week 2",
                    Order = 2, 
                    StartDateTime = new DateTime(2016, 8, 24, 20, 0, 0), 
                    EndDateTime = new DateTime(2016, 8, 31, 19, 59, 59)
                },
                new Week
                {
                    Id = Guid.NewGuid(),
                    Name = "Week 3",
                    Order = 3, 
                    StartDateTime = new DateTime(2016, 8, 31, 20, 0, 0), 
                    EndDateTime = new DateTime(2016, 9, 7, 19, 59, 59)
                },
                new Week
                {
                    Id = Guid.NewGuid(), 
                    Name = "Week 4",
                    Order = 4, 
                    StartDateTime = new DateTime(2016, 9, 7, 20, 0, 0), 
                    EndDateTime = new DateTime(2016, 9, 14, 19, 59, 59)
                },
                new Week
                {
                    Id = Guid.NewGuid(),
                    Name = "Week 5",
                    Order = 5, 
                    StartDateTime = new DateTime(2016, 9, 14, 20, 0, 0), 
                    EndDateTime = new DateTime(2016, 9, 21, 19, 59, 59)
                },
                new Week
                {
                    Id = Guid.NewGuid(),
                    Name = "Week 6",
                    Order = 6, 
                    StartDateTime = new DateTime(2016, 9, 21, 20, 0, 0), 
                    EndDateTime = new DateTime(2016, 9, 28, 19, 59, 59)
                },
                new Week
                {
                    Id = Guid.NewGuid(),
                    Name = "Week 7",
                    Order = 7, 
                    StartDateTime = new DateTime(2016, 9, 28, 20, 0, 0), 
                    EndDateTime = new DateTime(2016, 10, 5, 19, 59, 59)
                },
                new Week
                {
                    Id = Guid.NewGuid(),
                    Name = "Quarter Final",
                    Order = 8, 
                    StartDateTime = new DateTime(2016, 10, 5, 20, 0, 0), 
                    EndDateTime = new DateTime(2016, 10, 12, 19, 59, 59)
                },
                new Week
                {
                    Id = Guid.NewGuid(),
                    Name = "Semi Final",
                    Order = 9, 
                    StartDateTime = new DateTime(2016, 10, 12, 20, 0, 0), 
                    EndDateTime = new DateTime(2016, 10, 19, 19, 59, 59)
                },
                new Week
                {
                    Id = Guid.NewGuid(),
                    Name = "Final",
                    Order = 10, 
                    StartDateTime = new DateTime(2016, 10, 19, 20, 0, 0), 
                    EndDateTime = new DateTime(2016, 10, 26, 19, 59, 59)
                },
            };

            weeks.ForEach(s => context.Weeks.Add(s));

            var bakers = new List<Baker>
            {
                new Baker
                {
                    Id = Guid.NewGuid(),
                    Name = "Andrew",
                    Weeks = new List<Week> {weeks.OrderBy(o => o.Order).First()}
                },
                new Baker
                {
                    Id = Guid.NewGuid(),
                    Name = "Benjamina",
                    Weeks = new List<Week> {weeks.OrderBy(o => o.Order).First()}
                },
                new Baker
                {
                    Id = Guid.NewGuid(),
                    Name = "Candice",
                    Weeks = new List<Week> {weeks.OrderBy(o => o.Order).First()}
                },
                new Baker
                {
                    Id = Guid.NewGuid(),
                    Name = "Jane",
                    Weeks = new List<Week> {weeks.OrderBy(o => o.Order).First()}
                },
                new Baker
                {
                    Id = Guid.NewGuid(),
                    Name = "Kate",
                    Weeks = new List<Week> {weeks.OrderBy(o => o.Order).First()}
                },
                new Baker
                {
                    Id = Guid.NewGuid(),
                    Name = "Lee",
                    Weeks = new List<Week> {weeks.OrderBy(o => o.Order).First()}
                },
                new Baker
                {
                    Id = Guid.NewGuid(),
                    Name = "Louise",
                    Weeks = new List<Week> {weeks.OrderBy(o => o.Order).First()}
                },
                new Baker
                {
                    Id = Guid.NewGuid(),
                    Name = "Michael",
                    Weeks = new List<Week> {weeks.OrderBy(o => o.Order).First()}
                },
                new Baker
                {
                    Id = Guid.NewGuid(),
                    Name = "Rav",
                    Weeks = new List<Week> {weeks.OrderBy(o => o.Order).First()}
                },
                new Baker
                {
                    Id = Guid.NewGuid(),
                    Name = "Selasi",
                    Weeks = new List<Week> {weeks.OrderBy(o => o.Order).First()}
                },
                new Baker
                {
                    Id = Guid.NewGuid(),
                    Name = "Tom",
                    Weeks = new List<Week> {weeks.OrderBy(o => o.Order).First()}
                },
                new Baker
                {
                    Id = Guid.NewGuid(),
                    Name = "Val",
                    Weeks = new List<Week> {weeks.OrderBy(o => o.Order).First()}
                }
            };

            bakers.ForEach(s => context.Bakers.Add(s));

            var user1 = new User
            {
                UserId = new Guid("09e010a5-eac6-42bc-bfdd-1edf3cb71b82"), 
                Name = "Andy Brown", 
                Predictions = new List<Prediction>()
            };

            var user2 = new User
            {
                UserId = new Guid("bf75a638-b322-4027-8b4c-bdb33b790e38"), 
                Name = "Louise Brown", 
                Predictions = new List<Prediction>()
            };

            var league1 = new League
            {
                Id = Guid.NewGuid(),
                Code = "BOB111",
                Name = "Test League 1",
                Users = new List<User> { user1 }
            };

            var league2 = new League
            {
                Id = Guid.NewGuid(),
                Code = "BOB112",
                Name = "Test League 2",
                Users = new List<User> { user1, user2 }
            };

            context.Users.Add(user1);
            context.Leagues.Add(league1);
            context.Leagues.Add(league2);
            context.SaveChanges();

                                                             var week2Id = context.Weeks
                                             .Single(o => o.Order == 2).Id;
                                                             var week2User1StarId = context.Bakers.Single(o => o.Name == "Candice").Id;
                                                             var week2User1ExitId = context.Bakers.Single(o => o.Name == "Andrew").Id;
                                                             var week2User2StarId = context.Bakers.Single(o => o.Name == "Benjamina").Id;
                                                             var week2User2ExitId = context.Bakers.Single(o => o.Name == "Selasi").Id;
            user1.Predictions.Add(new Prediction { ExitBakerId = week2User1ExitId, 
                                                   StarBakerId = week2User1StarId, 
                                                        WeekId = week2Id, PredictionId = Guid.NewGuid() });
            user2.Predictions.Add(new Prediction { ExitBakerId = week2User2ExitId, 
                                                   StarBakerId = week2User2StarId, 
                                                        WeekId = week2Id, PredictionId = Guid.NewGuid() });


                                                             var week3Id = context.Weeks
                                             .Single(o => o.Order == 3).Id;
                                                             var week3User1StarId = context.Bakers.Single(o => o.Name == "Andrew").Id;
                                                             var week3User1ExitId = context.Bakers.Single(o => o.Name == "Candice").Id;
                                                             var week3User2StarId = context.Bakers.Single(o => o.Name == "Candice").Id;
                                                             var week3User2ExitId = context.Bakers.Single(o => o.Name == "Selasi").Id;
            user1.Predictions.Add(new Prediction { ExitBakerId = week3User1ExitId, 
                                                   StarBakerId = week3User1StarId, 
                                                        WeekId = week3Id, PredictionId = Guid.NewGuid() });
            user2.Predictions.Add(new Prediction { ExitBakerId = week3User2ExitId, 
                                                   StarBakerId = week3User2StarId, 
                                                        WeekId = week3Id, PredictionId = Guid.NewGuid() });
           



                                                             var week4Id = context.Weeks
                                             .Single(o => o.Order == 4).Id;
                                                             var week4User1StarId = context.Bakers.Single(o => o.Name == "Andrew").Id;
                                                             var week4User1ExitId = context.Bakers.Single(o => o.Name == "Selasi").Id;
                                                             var week4User2StarId = context.Bakers.Single(o => o.Name == "Selasi").Id;
                                                             var week4User2ExitId = context.Bakers.Single(o => o.Name == "Kate").Id;
            user1.Predictions.Add(new Prediction { ExitBakerId = week4User1ExitId, 
                                                   StarBakerId = week4User1StarId, 
                                                        WeekId = week4Id, PredictionId = Guid.NewGuid()});
            user2.Predictions.Add(new Prediction { ExitBakerId = week4User2ExitId, 
                                                   StarBakerId = week4User2StarId, 
                                                        WeekId = week4Id, PredictionId = Guid.NewGuid() });
           



                                                             var week5Id = context.Weeks
                                             .Single(o => o.Order == 5).Id;
                                                             var week5User1StarId = context.Bakers.Single(o => o.Name == "Lee").Id;
                                                             var week5User1ExitId = context.Bakers.Single(o => o.Name == "Val").Id;
                                                             var week5User2StarId = context.Bakers.Single(o => o.Name == "Kate").Id;
                                                             var week5User2ExitId = context.Bakers.Single(o => o.Name == "Selasi").Id;
            user1.Predictions.Add(new Prediction { ExitBakerId = week5User1ExitId, 
                                                   StarBakerId = week5User1StarId, 
                                                        WeekId = week5Id, PredictionId = Guid.NewGuid()});
            user2.Predictions.Add(new Prediction { ExitBakerId = week5User2ExitId, 
                                                   StarBakerId = week5User2StarId, 
                                                        WeekId = week5Id, PredictionId = Guid.NewGuid() });


//                                                             var week6Id = context.Weeks
//                                             .Single(o => o.Order == 6).Id;
//                                                             var week6User1StarId = context.Bakers.Single(o => o.Name == "Ian Cumming").Id;
//                                                             var week6User1ExitId = context.Bakers.Single(o => o.Name == "Mat Riley").Id;
//                                                             var week6User2StarId = context.Bakers.Single(o => o.Name == "Flora Shedden").Id;
//                                                             var week6User2ExitId = context.Bakers.Single(o => o.Name == "Paul").Id;
//            user1.Predictions.Add(new Prediction { ExitBakerId = week6User1ExitId, 
//                                                   StarBakerId = week6User1StarId, 
//                                                        WeekId = week6Id, PredictionId = Guid.NewGuid()});
//            user2.Predictions.Add(new Prediction { ExitBakerId = week6User2ExitId, 
//                                                   StarBakerId = week6User2StarId, 
//                                                        WeekId = week6Id, PredictionId = Guid.NewGuid() });
//
//            
//
//
//                                                             var week7Id = context.Weeks
//                                             .Single(o => o.Order == 7).Id;
//                                                             var week7User1StarId = context.Bakers.Single(o => o.Name == "Flora Shedden").Id;
//                                                             var week7User1ExitId = context.Bakers.Single(o => o.Name == "Ian Cumming").Id;
//                                                             var week7User2StarId = context.Bakers.Single(o => o.Name == "Flora Shedden").Id;
//                                                             var week7User2ExitId = context.Bakers.Single(o => o.Name == "Paul").Id;
//            user1.Predictions.Add(new Prediction { ExitBakerId = week7User1ExitId, 
//                                                   StarBakerId = week7User1StarId, 
//                                                        WeekId = week7Id, PredictionId = Guid.NewGuid()});
//            user2.Predictions.Add(new Prediction { ExitBakerId = week7User2ExitId, 
//                                                   StarBakerId = week7User2StarId, 
//                                                        WeekId = week7Id, PredictionId = Guid.NewGuid() });
//
//            
//
//
//                                                             var week8Id = context.Weeks
//                                             .Single(o => o.Order == 8).Id;
//                                                             var week8User1StarId = context.Bakers.Single(o => o.Name == "Flora Shedden").Id;
//                                                             var week8User1ExitId = context.Bakers.Single(o => o.Name == "Tamal Ray").Id;
//                                                             var week8User2StarId = context.Bakers.Single(o => o.Name == "Ian Cumming").Id;
//                                                             var week8User2ExitId = context.Bakers.Single(o => o.Name == "Paul").Id;
//            user1.Predictions.Add(new Prediction { ExitBakerId = week8User1ExitId, 
//                                                   StarBakerId = week8User1StarId, 
//                                                        WeekId = week8Id, PredictionId = Guid.NewGuid()});
//            user2.Predictions.Add(new Prediction { ExitBakerId = week8User2ExitId, 
//                                                   StarBakerId = week8User2StarId, 
//                                                        WeekId = week8Id, PredictionId = Guid.NewGuid() });
//           
//            context.SaveChanges();

            context.Weeks.Single(o => o.Order == 1).StarBaker = context.Bakers.Single(o => o.Name == "Andrew");
            context.Weeks.Single(o => o.Order == 1).ExitBaker = context.Bakers.Single(o => o.Name == "Benjamina");

            context.Weeks.Single(o => o.Order == 2).StarBaker = context.Bakers.Single(o => o.Name == "Andrew");
            context.Weeks.Single(o => o.Order == 2).ExitBaker = context.Bakers.Single(o => o.Name == "Louise");
            
            context.Weeks.Single(o => o.Order == 3).StarBaker = context.Bakers.Single(o => o.Name == "Kate");
            context.Weeks.Single(o => o.Order == 3).ExitBaker = context.Bakers.Single(o => o.Name == "Selasi");
            
            context.Weeks.Single(o => o.Order == 4).StarBaker = context.Bakers.Single(o => o.Name == "Lee");
            context.Weeks.Single(o => o.Order == 4).ExitBaker = context.Bakers.Single(o => o.Name == "Val");

            context.Weeks.Single(o => o.Order == 5).StarBaker = context.Bakers.Single(o => o.Name == "Jane");
            context.Weeks.Single(o => o.Order == 5).ExitBaker = context.Bakers.Single(o => o.Name == "Lee");
//
//            context.Weeks.Single(o => o.Order == 6).StarBaker = context.Bakers.Single(o => o.Name == "Mat Riley");
//            context.Weeks.Single(o => o.Order == 6).ExitBaker = context.Bakers.Single(o => o.Name == "Alvin Magallanes");
//
//            context.Weeks.Single(o => o.Order == 7).StarBaker = context.Bakers.Single(o => o.Name == "Tamal Ray");
//            context.Weeks.Single(o => o.Order == 7).ExitBaker = context.Bakers.Single(o => o.Name == "Mat Riley");
//
//            context.Weeks.Single(o => o.Order == 8).StarBaker = context.Bakers.Single(o => o.Name == "Nadiya Begum");
//            context.Weeks.Single(o => o.Order == 8).ExitBaker = context.Bakers.Single(o => o.Name == "Paul");

//            // Run Scoring 
            
            context.SaveChanges();
        }
    }
}