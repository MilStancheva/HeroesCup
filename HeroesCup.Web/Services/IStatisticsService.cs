namespace HeroesCup.Web.Services
{
    public interface IStatisticsService
    {
        int GetAllHeroesCount();

        int GetAllMissionsCount();

        int GetAllClubsCount();

        int GetAllHoursCount();
    }
}
