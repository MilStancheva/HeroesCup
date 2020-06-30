using System.Threading.Tasks;

namespace HeroesCup.Web.Services
{
    public interface IPageInitializer
    {
        Task SeedStarPageAsync();

        Task SeedAboutPageAsync();

        Task SeedResourcesPageAsync();

        Task SeedEventsPageAsync();

        Task SeedMissionsPageAsync();
    }
}