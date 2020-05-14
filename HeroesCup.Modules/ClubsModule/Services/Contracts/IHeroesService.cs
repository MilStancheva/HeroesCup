using ClubsModule.Models;
using HeroesCup.Data.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ClubsModule.Services.Contracts
{
    public interface IHeroesService
    {
        Task Create(Hero hero);

        Task Update(Hero hero);

        Task<IEnumerable<Hero>> GetAll();

        Task<Hero> GetById(Guid id);

        Task<HeroListModel> GetHeroListModelAsync(Guid? ownerId);

        Task<HeroEditModel> CreateHeroEditModel(Guid? ownerId);

        Task<HeroEditModel> GetHeroEditModelByIdAsync(Guid id, Guid? ownerId);

        Task<Guid> SaveHeroEditModel(HeroEditModel model); 
    }
}
