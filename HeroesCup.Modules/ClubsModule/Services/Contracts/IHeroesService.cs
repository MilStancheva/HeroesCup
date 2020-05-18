using ClubsModule.Models;
using System;
using System.Threading.Tasks;

namespace ClubsModule.Services.Contracts
{
    public interface IHeroesService
    {
        Task<HeroListModel> GetHeroListModelAsync(Guid? ownerId);

        Task<HeroEditModel> CreateHeroEditModelAsync(Guid? ownerId);

        Task<HeroEditModel> GetHeroEditModelByIdAsync(Guid id, Guid? ownerId);

        Task<Guid> SaveHeroEditModelAsync(HeroEditModel model);

        Task<bool> DeleteAsync(Guid id);
    }
}