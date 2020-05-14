using HeroesCup.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace HeroesCup.Data
{
    public class HeroesCupDbContext : DbContext
    {
        public HeroesCupDbContext(DbContextOptions<HeroesCupDbContext> options)
            : base(options)
        {

        }

        public DbSet<Hero> Heroes { get; set; }

        public DbSet<SchoolClub> SchoolClubs { get; set; }

        public DbSet<Mission> Missions { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Hero>()
                .HasOne(h => h.SchoolClub)
                .WithMany(c => c.Heroes)
                .HasForeignKey(h => h.SchoolClubId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Mission>()
                .HasOne(h => h.SchoolClub)
                .WithMany(c => c.Missions)
                .HasForeignKey(h => h.SchoolClubId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<HeroMission>()
            .HasKey(h => new { h.HeroId, h.MissionId });

            modelBuilder.Entity<HeroMission>()
                .HasOne(hm => hm.Hero)
                .WithMany(h => h.HeroMissions)
                .HasForeignKey(hm => hm.HeroId);

            modelBuilder.Entity<HeroMission>()
                .HasOne(hm => hm.Mission)
                .WithMany(m => m.HeroMissions)
                .HasForeignKey(hm => hm.MissionId);

        }
    }
}