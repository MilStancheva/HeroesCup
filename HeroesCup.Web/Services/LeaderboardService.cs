using ClubsModule.Services.Contracts;
using HeroesCup.Data.Models;
using HeroesCup.Web.Models;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace HeroesCup.Web.Services
{
    public class LeaderboardService : ILeaderboardService
    {
        private readonly IMissionsService missionsService;
        private readonly ISchoolYearService schoolYearService;

        public LeaderboardService(IMissionsService missionsService, ISchoolYearService schoolYearService)
        {
            this.missionsService = missionsService;
            this.schoolYearService = schoolYearService;
        }

        public async Task<ClubListViewModel> GetClubsBySchoolYearAsync(string schoolYear)
        {
            var missions = await this.missionsService.GetMissionsBySchoolYear(schoolYear);
            var clubs = missions
                .GroupBy(m => m.Club)
                .Select(g => new
                {
                    Club = g.Key,
                    Missions = g.ToList()
                })
                .Select(c => new ClubListItem()
                {
                    Id = c.Club.Id,
                    Name = GetClubName(c.Club),
                    Location = c.Club.Location,
                    ClubInitials = GetClubInitials(c.Club.OrganizationName),
                    HeroesCount = GetHeroesCount(c.Club),
                    Points = getClubPoints(c.Missions)

                })
                .OrderByDescending(c => c.Points);

            var model = new ClubListViewModel()
            {
                Clubs = clubs
            };

            return model;
        }

        private string GetClubName(Club club)
        {
            return $"{club.Name} {club.OrganizationType } \"{club.OrganizationName }\"";
        }

        private string GetClubInitials(string organizationName)
        {
            Regex initials = new Regex(@"(\b[a-zA-Z-а-яА-Я])[a-zA-Z-а-яА-Я]* ?");
            return initials.Replace(organizationName, "$1");
        }

        private int GetHeroesCount(Club club)
        {
            return club.Heroes.Count;
        }

        private int getClubPoints(IEnumerable<Mission> missions)
        {
            var points = 0;
            foreach (var mission in missions)
            {
                points += mission.Stars * mission.HeroMissions.Count();
            }

            return points;
        }

        public IEnumerable<string> GetSchoolYears()
        {
            return this.missionsService.GetMissionSchoolYears();
        }

        public string GetCurrentSchoolYear()
        {
            return this.schoolYearService.GetCurrentSchoolYear();
        }
    }
}
