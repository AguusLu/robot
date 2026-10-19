using Microsoft.EntityFrameworkCore;
using RobotApi.Models;
using System.Collections.Generic;
using System.Reflection.Emit;

namespace RobotApi.Data
{
    public class TorneoRobotsContext : DbContext
    {
        public TorneoRobotsContext(
            DbContextOptions<TorneoRobotsContext> options)
            : base(options)
        {
        }

        public DbSet<RobotEntity> Robots { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<RobotEntity>()
                .ToTable("Robots");
        }
    }
}