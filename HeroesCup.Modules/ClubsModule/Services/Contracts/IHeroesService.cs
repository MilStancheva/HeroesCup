using ClubsModule.Models;
using System;
using System.Threading.Tasks;

namespace ClubsModule.Services.Contracts
{
    public interface IHeroesService
    {
        Task<HeroListModel> GetHeroListModelAsync(Guid? ownerId);

        Task<HeroEditModel> CreateHeroEditModel(Guid? ownerId);

        Task<HeroEditModel> GetHeroEditModelByIdAsync(Guid id, Guid? ownerId);

        Task<Guid> SaveHeroEditModel(HeroEditModel model); 
    }
}
