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

        public DbSet<Club> Clubs { get; set; }

        public DbSet<Mission> Missions { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Club>()
                .HasMany(c => c.Heroes)
                .WithOne(c => c.Club)
                .HasForeignKey(h => h.ClubId);

            modelBuilder.Entity<Club>()
               .HasMany(c => c.Missions)
               .WithOne(c => c.Club)
               .HasForeignKey(m => m.ClubId);

            modelBuilder.Entity<Hero>()
                .HasOne(h => h.Club)
                .WithMany(c => c.Heroes)
                .HasForeignKey(c => c.ClubId);

            modelBuilder.Entity<Mission>()
                .HasOne(h => h.Club)
                .WithMany(c => c.Missions)
                .HasForeignKey(m => m.ClubId);

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
