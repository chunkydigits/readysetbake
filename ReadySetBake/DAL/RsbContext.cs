using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using ReadySetBake.Models;

namespace ReadySetBake.DAL
{
    public class RsbContext : DbContext
    {
        public RsbContext()
            : base("RsbContext")
        {

        }

        public DbSet<Week> Weeks { get; set; }
        public DbSet<League> Leagues { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<UserLeagueLink> UserLeagueLinks { get; set; }
        public DbSet<Baker> Bakers { get; set; }
        public DbSet<Prediction> Predictions { get; set; }
    }
}