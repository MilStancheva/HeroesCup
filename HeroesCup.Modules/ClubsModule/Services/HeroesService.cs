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

        public async Task Create(Hero hero)
        {
            this.dbContext.Heroes.Add(hero);
            await dbContext.SaveChangesAsync();
        }

        public async Task<IEnumerable<Hero>> GetAll()
        {
            return await dbContext.Heroes.ToListAsync();
        }

        public async Task<Hero> GetById(Guid id)
        {
            return await dbContext.Heroes.FirstOrDefaultAsync(h => h.Id == id);
        }

        public async Task Update(Hero hero)
        {
            this.dbContext.Heroes.Update(hero);
            await dbContext.SaveChangesAsync();
        }
        public async Task<HeroListModel> GetHeroListModelAsync()
        {
            var heroes = await this.dbContext.Heroes
                .Include(h => h.Club)
                .ToListAsync();

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
                                    OrganizationName = m.Club.Name
                                })

            };

            return model;
        }

        public async Task<HeroEditModel> CreateHeroEditModel()
        {
            var clubs = await this.dbContext.Clubs.ToListAsync();
            return new HeroEditModel()
            {
                Hero = new Hero(),
                Missions = new List<Mission>(),
                Clubs = clubs
            };
        }

        public async Task<HeroEditModel> GetHeroEditModelByIdAsync(Guid id)
        {
            var hero = await this.dbContext.Heroes
                .Include(x => x.HeroMissions)
                .ThenInclude(x => x.Mission)
                .FirstOrDefaultAsync(h => h.Id == id);

            if (hero == null)
            {
                return null;
            }

            var missions = hero.HeroMissions
                .Where(h => h.HeroId == id)
                .Select(x => x.Mission);

            var clubs = await this.dbContext.Clubs.ToListAsync();
            if (clubs == null)
            {
                clubs = new List<Club>();
            }

            var model = await CreateHeroEditModel();
            model.Hero = hero;
            model.Missions = missions;
            model.Clubs = clubs;

            return model;
        }

        public async Task<Guid> SaveHeroEditModel(HeroEditModel model)
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
    }
}
