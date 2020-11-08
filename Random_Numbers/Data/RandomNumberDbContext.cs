using Microsoft.EntityFrameworkCore;
using Random_Numbers.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Random_Numbers.Data
{
    public class RandomNumberDbContext: DbContext
    {
        public RandomNumberDbContext(DbContextOptions<RandomNumberDbContext> options): base(options)
        {

        }

        public DbSet<Match> Matches { get; set; }
        public DbSet<Player> Players { get; set; }
        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(RandomNumberDbContext).Assembly);
            base.OnModelCreating(modelBuilder);
        }
    }
}
