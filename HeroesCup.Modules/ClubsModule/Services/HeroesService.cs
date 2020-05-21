using ClubsModule.Models;
using ClubsModule.Services.Contracts;
using HeroesCup.Data;
using HeroesCup.Data.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ClubsModule.Services
{
    public class HeroesService : IHeroesService
    {
        private readonly HeroesCupDbContext dbContext;

        public HeroesService(HeroesCupDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<HeroListModel> GetHeroListModelAsync(Guid? ownerId)
        {
            var heroes = new List<Hero>();
            heroes = await this.dbContext.Heroes
                    .Include(h => h.Club)
                    .ToListAsync();

            if (ownerId.HasValue)
            {
                heroes = heroes.Where(h => h.Club.OwnerId == ownerId.Value).ToList();
            }

            if (heroes == null)
            {
                heroes = new List<Hero>();
            }

            var model = new HeroListModel()
            {
                Heroes = heroes.OrderBy(h => h.Name)
                                .Select(m => new HeroListItem()
                                {
                                    Id = m.Id,
                                    Name = m.Name,
                                    ClubId = m.ClubId,
                                    ClubName = m.Club.Name
                                })

            };

            return model;
        }

        public async Task<HeroEditModel> CreateHeroEditModelAsync(Guid? ownerId)
        {
            var clubs = new List<Club>();
            var missions = new List<Mission>();
            if (ownerId.HasValue)
            {
                clubs = await this.dbContext.Clubs.Where(c => c.OwnerId == ownerId.Value).ToListAsync();
                missions = await this.dbContext.Missions.Where(m => m.OwnerId == ownerId.Value).ToListAsync();
            }
            else
            {
                clubs = await this.dbContext.Clubs.ToListAsync();
                missions = await this.dbContext.Missions.ToListAsync();
            }

            return new HeroEditModel()
            {
                Hero = new Hero(),
                Clubs = clubs,
                Missions = missions
            };
        }

        public async Task<HeroEditModel> GetHeroEditModelByIdAsync(Guid id, Guid? ownerId)
        {
            var hero = await this.dbContext.Heroes
                     .Include(x => x.HeroMissions)
                     .ThenInclude(x => x.Mission)
                     .FirstOrDefaultAsync(h => h.Id == id);

            if (hero == null)
            {
                return null;
            }

            if (ownerId.HasValue && hero.Club.OwnerId != ownerId.Value)
            {
                return null;
            }

            var missions = hero.HeroMissions
                .Where(h => h.HeroId == id)
                .Select(x => x.Mission);

            IEnumerable<Club> clubs = null;
            if (ownerId.HasValue)
            {
                clubs = await this.dbContext.Clubs
                    .Where(c => c.OwnerId == ownerId.Value)
                    .ToListAsync();
            }
            else
            {
                clubs = await this.dbContext.Clubs.ToListAsync();
            }

            if (clubs == null)
            {
                clubs = new List<Club>();
            }

            var model = await CreateHeroEditModelAsync(ownerId);
            model.Hero = hero;
            model.Missions = missions;
            model.Clubs = clubs;

            return model;
        }

        public async Task<Guid> SaveHeroEditModelAsync(HeroEditModel model)
        {
            var hero = await this.dbContext.Heroes
                .Include(x => x.HeroMissions)
                .ThenInclude(x => x.Mission)
                .FirstOrDefaultAsync(h => h.Id == model.Hero.Id);

            if (hero == null)
            {
                hero = new Hero();
                hero.Id = model.Hero.Id != Guid.Empty ? model.Hero.Id : Guid.NewGuid();
                this.dbContext.Heroes.Add(hero);
            }

            hero.Name = model.Hero.Name;
            hero.ClubId = model.ClubId;

            if (model.Hero.IsCoordinator)
            {
                var heroesFromClub = await this.dbContext.Heroes.Where(h => h.ClubId == model.ClubId).ToListAsync();
                foreach (var h in heroesFromClub)
                {
                    h.IsCoordinator = false;
                }

                hero.IsCoordinator = model.Hero.IsCoordinator;
            }

            if (model.Missions != null && model.Missions.Any())
            {
                hero.HeroMissions = model.Missions.Select(m => new HeroMission()
                {
                    Mission = m,
                    MissionId = m.Id,
                    Hero = hero,
                    HeroId = hero.Id
                }).ToList();
            }

            await dbContext.SaveChangesAsync();
            return hero.Id;
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            var hero = this.dbContext.Heroes.FirstOrDefault(c => c.Id == id);
            if (hero == null)
            {
                return false;
            }

            this.dbContext.Heroes.Remove(hero);
            await this.dbContext.SaveChangesAsync();
            return true;
        }
    }
}