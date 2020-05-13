using ClubsModule.Models;
using ClubsModule.Services.Contracts;
using HeroesCup.Data;
using HeroesCup.Data.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClubsModule.Services
{
    public class ClubsService : IClubsService
    {

        private readonly HeroesCupDbContext dbContext;

        public ClubsService(HeroesCupDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public ClubEditModel CreateClubEditModel()
        {
            return new ClubEditModel()
            {
                Club = new Club(),
                Missions = new List<Mission>(),
                Heroes = new List<Hero>()
            };
        }

        public async Task<ClubEditModel> GetClubEditModelByIdAsync(Guid id)
        {
            var club = await this.dbContext.Clubs.FirstOrDefaultAsync(c => c.Id == id);

            if (club == null)
            {
                return null;
            }

            var missions = club.Missions;

            var model = CreateClubEditModel();
            model.Club = club;
            model.Missions = club.Missions;
            model.Heroes = club.Heroes;

            return model;
        }

        public async Task<ClubListModel> GetClubListModelAsync()
        {
            var clubs = await this.dbContext.Clubs.ToListAsync();
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
                                    OrganizationName = c.OrganizationName
                                })

            };

            return model;
        }

        public async Task<bool> SaveClubEditModel(ClubEditModel model)
        {
            var club = await this.dbContext.Clubs
                .FirstOrDefaultAsync(h => h.Id == model.Club.Id);

            if (club == null)
            {
                club = new Club();
                club.Id = model.Club.Id != Guid.Empty ? model.Club.Id : Guid.NewGuid();
                this.dbContext.Clubs.Add(club);
            }

            club.Name = model.Club.Name;


            if (model.Heroes != null && model.Heroes.Any())
            {
                club.Heroes = model.Heroes.Select(m => new Hero()
                {
                    Id = m.Id != Guid.Empty ? m.Id : Guid.NewGuid(),
                    Club = club,
                    Name = m.Name,
                    IsCoordinator = m.IsCoordinator
                }).ToList();
            }

            if (model.Missions != null && model.Missions.Any())
            {
                club.Missions = model.Missions.Select(m => new Mission()
                {
                    Id = m.Id != Guid.Empty ? m.Id : Guid.NewGuid(),
                    ClubId = club.Id,
                    Club = club,
                    Content = m.Content,
                    Image = m.Image,
                    Location = m.Location,
                    Stars = m.Stars,
                    Title = m.Title,
                    Type = MissionType.HeroesCup,
                    StartDate = m.StartDate,
                    EndDate = m.EndDate,
                    SchoolYear = this.getSchoolYear(m.StartDate)
                }).ToList();
            }

            await dbContext.SaveChangesAsync();
            return true;
        }

        private int getSchoolYear(long startDate)
        {
            throw new NotImplementedException();
        }
    }
}
