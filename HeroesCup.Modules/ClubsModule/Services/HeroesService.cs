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
            var heroes = await GetAll();
            var model = new HeroListModel()
            {
                Heroes = heroes.OrderBy(h => h.Name)
                                .Select(m => new HeroListItem()
                                {
                                    Id = m.Id,
                                    Name = m.Name,
                                    SchoolClubName = m.SchoolClub.Name
                                })

            };

            return model;
        }

        public HeroEditModel CreateHeroEditModel()
        {
            return new HeroEditModel()
            {
                Hero = new Hero(),
                Missions = new List<Mission>()
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

            var model = CreateHeroEditModel();
            model.Hero = hero;
            model.Missions = missions;

            return model;
        }

        public async Task<bool> SaveHeroEditModel(HeroEditModel model)
        {
            var hero = await this.dbContext.Heroes
                .Include(x => x.HeroMissions)
                .ThenInclude(x => x.Mission)
                .FirstOrDefaultAsync(h => h.Id == model.Hero.Id);

            if (hero == null)
            {
                hero = new Hero();
                this.dbContext.Heroes.Add(hero);
            }

            hero.Id = model.Hero.Id != Guid.Empty ? model.Hero.Id : Guid.NewGuid();
            hero.Name = model.Hero.Name;
            hero.SchoolClub = model.Hero.SchoolClub;
            hero.IsCoordinator = model.Hero.IsCoordinator;
            hero.HeroMissions = model.Missions.Select(m => new HeroMission()
            {
                Mission = m,
                MissionId = m.Id,
                Hero = hero,
                HeroId = hero.Id
            }).ToList();

            dbContext.Heroes.Update(hero);
            await dbContext.SaveChangesAsync();
            return true;
        }
    }
}
