using ClubsModule.Models;
using ClubsModule.Services.Contracts;
using HeroesCup.Data;
using HeroesCup.Data.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace ClubsModule.Services
{
    public class ClubsService : IClubsService
    {

        private readonly HeroesCupDbContext dbContext;
        private readonly IImagesService imagesService;

        public ClubsService(HeroesCupDbContext dbContext, IImagesService imagesService)
        {
            this.dbContext = dbContext;
            this.imagesService = imagesService;
        }

        public async Task<ClubEditModel> CreateClubEditModelAsync(Guid? ownerId)
        {
            var missions = new List<Mission>();
            var heroes = new List<Hero>();
            if (ownerId.HasValue)
            {
                missions = await this.dbContext.Missions.Where(m => m.OwnerId == ownerId.Value).ToListAsync();
                heroes = await this.dbContext.Heroes.Where(h => h.Club.OwnerId == ownerId.Value).ToListAsync();
            }
            else
            {
                missions = await this.dbContext.Missions.ToListAsync();
                heroes = await this.dbContext.Heroes.ToListAsync();
            }

            var model = new ClubEditModel()
            {
                Club = new Club(),
                Missions = missions != null ? missions : new List<Mission>(),
                Heroes = heroes != null ? heroes : new List<Hero>()
            };

            model.Club.OwnerId = ownerId.HasValue ? ownerId.Value : Guid.Empty;
            return model;
        }

        public async Task<ClubEditModel> GetClubEditModelByIdAsync(Guid id, Guid? ownerId)
        {
            Club club = null;
            if (ownerId.HasValue)
            {
                club = await this.dbContext.Clubs
                 .Where(c => c.OwnerId == ownerId.Value)
                 .Include(c => c.ClubImages)
                 .ThenInclude(ci => ci.Image)
                 .FirstOrDefaultAsync(c => c.Id == id);
            }
            else
            {
                club = await this.dbContext.Clubs
                    .Include(c => c.ClubImages)
                    .ThenInclude(ci => ci.Image)
                    .FirstOrDefaultAsync(c => c.Id == id);
            }

            if (club == null)
            {
                return null;
            }

            var missions = club.Missions;

            var model = await CreateClubEditModelAsync(ownerId);
            model.Club = club;
            var coordinator = await this.GetClubCoordinatorAsync(club.Id);
            if (coordinator != null)
            {
                model.Coordinator = coordinator;
                model.CoordinatorId = model.Coordinator.Id;
            }

            if (club.ClubImages != null && club.ClubImages.Count > 0)
            {
                var clubImage = await this.imagesService.GetClubImage(club.Id);
                model.LogoSrc = this.imagesService.GetImageSource(clubImage.Image.ContentType, clubImage.Image.Bytes);
            }

            model.Missions = club.Missions;
            model.Heroes = club.Heroes;

            return model;
        }

        public async Task<ClubListModel> GetClubListModelAsync(Guid? ownerId)
        {
            var clubs = new List<Club>();
            if (ownerId.HasValue)
            {
                clubs = await this.dbContext.Clubs
                     .Include(c => c.Heroes)
                     .Where(c => c.OwnerId == ownerId.Value)
                     .ToListAsync();
            }
            else
            {
                clubs = await this.dbContext.Clubs
                    .Include(c => c.Heroes)
                    .ToListAsync();
            }

            if (clubs == null)
            {
                clubs = new List<Club>();
            }

            var model = new ClubListModel()
            {
                Clubs = clubs.OrderBy(h => h.Name)
                                .Select(c => new ClubListItem()
                                {
                                    Id = c.Id,
                                    Name = c.Name,
                                    OrganizationType = c.OrganizationType,
                                    OrganizationName = c.OrganizationName,
                                    HeroesCount = c.Heroes != null ? c.Heroes.Count() : 0
                                })

            };

            return model;
        }

        public async Task<Guid> SaveClubEditModelAsync(ClubEditModel model)
        {
            var club = await this.dbContext.Clubs
                .Include(c => c.ClubImages)
                .Include(c => c.Heroes)
                .Include(c => c.Missions)
                .FirstOrDefaultAsync(h => h.Id == model.Club.Id && h.OwnerId == model.Club.OwnerId);

            if (club == null)
            {
                club = new Club();
                club.Id = model.Club.Id != Guid.Empty ? model.Club.Id : Guid.NewGuid();
                club.OwnerId = model.Club.OwnerId;
                this.dbContext.Clubs.Add(club);
            }

            club.Name = model.Club.Name;
            club.Location = model.Club.Location;
            club.OrganizationType = model.Club.OrganizationType;
            club.OrganizationName = model.Club.OrganizationName;
            club.Description = model.Club.Description;


            // set club's heroes
            if (model.HeroesIds != null && model.HeroesIds.Any())
            {
                club.Heroes = new List<Hero>();
                foreach (var heroId in model.HeroesIds)
                {
                    var hero = this.dbContext.Heroes.FirstOrDefault(h => h.Id == heroId);
                    club.Heroes.Add(hero);
                }
            }

            // set clubs coordinator
            if (model.CoordinatorId != null && model.CoordinatorId != Guid.Empty)
            {
                var newCoordinator = this.dbContext.Heroes.FirstOrDefault(h => h.Id == model.CoordinatorId);
                foreach (var hero in club.Heroes)
                {
                    hero.IsCoordinator = false;
                }

                newCoordinator.IsCoordinator = true;
            }

            // set club's missions
            if (model.MissionsIds != null && model.MissionsIds.Any())
            {
                club.Missions = new List<Mission>();
                foreach (var missionId in model.MissionsIds)
                {
                    var mission = this.dbContext.Missions.FirstOrDefault(h => h.Id == missionId);
                    club.Missions.Add(mission);
                }
            }

            // set club logo
            if (model.UploadedLogo != null)
            {
                var image = new Image();
                var bytes = this.imagesService.GetByteArrayFromImage(model.UploadedLogo);
                var filename = this.imagesService.GetFilename(model.UploadedLogo);
                var contentType = this.imagesService.GetFileContentType(model.UploadedLogo);
                image.Bytes = bytes;
                image.Filename = filename;
                image.ContentType = contentType;

                await this.imagesService.CreateClubImageAsync(image, club);
            }

            await dbContext.SaveChangesAsync();
            return club.Id;
        }

        public async Task<Hero> GetClubCoordinatorAsync(Guid clubId)
        {
            Hero coordinator = null;
            var club = await this.dbContext.Clubs.FirstOrDefaultAsync(c => c.Id == clubId);
            if (club == null)
            {
                return null;
            }

            if (club.Heroes != null && club.Heroes.Count > 0)
            {
                coordinator = club.Heroes.FirstOrDefault(c => c.IsCoordinator);
            }

            return coordinator;
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            var club = this.dbContext.Clubs.FirstOrDefault(c => c.Id == id);
            if (club == null)
            {
                return false;
            }

            this.dbContext.Clubs.Remove(club);
            await this.dbContext.SaveChangesAsync();
            return true;
        }
    }
}