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

        public DbSet<HeroMission> HeroMissions { get; set; }

        public DbSet<Story> Stories { get; set; }

        public DbSet<Image> Images { get; set; }

        public DbSet<ClubImage> ClubImages { get; set; }

        public DbSet<MissionImage> MissionImages { get; set; }

        public DbSet<StoryImage> StoryImages { get; set; }

        public DbSet<MissionIdea> MissionIdeas { get; set; }

        public DbSet<MissionIdeaImage> MissionIdeaImages { get; set; }

        public DbSet<MissionContent> MissionContents { get; set; }

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

            modelBuilder.Entity<ClubImage>()
                .HasKey(h => new { h.ClubId, h.ImageId });

            modelBuilder.Entity<ClubImage>()
                .HasOne(hm => hm.Club)
                .WithMany(h => h.ClubImages)
                .HasForeignKey(hm => hm.ClubId);

            modelBuilder.Entity<ClubImage>()
                .HasOne(hm => hm.Image)
                .WithMany(m => m.ClubImages)
                .HasForeignKey(hm => hm.ImageId);

            modelBuilder.Entity<MissionImage>()
                .HasKey(h => new { h.MissionId, h.ImageId });

            modelBuilder.Entity<MissionImage>()
                .HasOne(hm => hm.Mission)
                .WithMany(h => h.MissionImages)
                .HasForeignKey(hm => hm.MissionId);

            modelBuilder.Entity<MissionImage>()
                .HasOne(hm => hm.Image)
                .WithMany(m => m.MissionImages)
                .HasForeignKey(hm => hm.ImageId);

            modelBuilder.Entity<MissionIdeaImage>()
               .HasKey(h => new { h.MissionIdeaId, h.ImageId });

            modelBuilder.Entity<MissionIdeaImage>()
                .HasOne(hm => hm.MissionIdea)
                .WithMany(h => h.MissionIdeaImages)
                .HasForeignKey(hm => hm.MissionIdeaId);

            modelBuilder.Entity<MissionIdeaImage>()
                .HasOne(hm => hm.Image)
                .WithMany(m => m.MissionIdeaImages)
                .HasForeignKey(hm => hm.ImageId);

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

            modelBuilder.Entity<Mission>()
                .HasOne(h => h.Story)
                .WithOne(s => s.Mission)
                .HasForeignKey<Story>(m => m.MissionId);

            modelBuilder.Entity<StoryImage>()
                .HasKey(h => new { h.StoryId, h.ImageId });

            modelBuilder.Entity<StoryImage>()
                .HasOne(hm => hm.Image)
                .WithMany(m => m.StoryImages)
                .HasForeignKey(hm => hm.ImageId);

            modelBuilder.Entity<Mission>()
                .HasIndex(m => m.Slug)
                .IsUnique(unique: true);

            modelBuilder.Entity<MissionIdea>()
                .HasIndex(m => m.Title)
                .IsUnique(unique: true);
        }
    }
}