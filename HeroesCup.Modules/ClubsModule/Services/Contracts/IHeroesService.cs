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

        Task<HeroListModel> GetHeroListModelAsync();

        Task<HeroEditModel> CreateHeroEditModel();

        Task<HeroEditModel> GetHeroEditModelByIdAsync(Guid id);

        Task<Guid> SaveHeroEditModel(HeroEditModel model); 
    }
}
