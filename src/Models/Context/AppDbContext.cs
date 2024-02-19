using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Models.Entity.Fight;
using Models.Entity.Fight.Configurations;
using Models.Entity.User;

namespace Models.Context
{
    public class AppDbContext :DbContext
    {
        public DbSet<User> User { get; set; }
        public DbSet<Airport> Airport { get; set; }
        public DbSet<Flight> Flight { get; set; }
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }
        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    optionsBuilder.UseSqlServer("ConnectionStrings:DefaultConnection");
        //}
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfiguration(new FlightConfiguration());
        }


    }
}
