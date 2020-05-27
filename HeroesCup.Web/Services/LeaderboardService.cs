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
        private readonly IImagesService imagesService;

        public LeaderboardService(IMissionsService missionsService, ISchoolYearService schoolYearService, IImagesService imageService)
        {
            this.missionsService = missionsService;
            this.schoolYearService = schoolYearService;
            this.imagesService = imageService;
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
                .Select(c =>
                {
                    IEnumerable<MissionViewModel> clubMissions = c.Club.Missions
                    .OrderByDescending(m => m.StartDate)
                    .Select(m => new MissionViewModel()
                    {
                        Id = m.Id,
                        Title = m.Title,
                        Club = m.Club,
                        ImageSrc = GetMissionImageSource(m)
                    });

                    IEnumerable<HeroViewModel> clubHeroes = c.Club.Heroes.Select(h => new HeroViewModel()
                    {
                        HeroInitials = GetClubInitials(h.Name),
                        IsCoordinator = h.IsCoordinator,
                        Name = h.Name
                    });

                    return new ClubListItem()
                    {
                        Id = c.Club.Id,
                        Name = GetClubName(c.Club),
                        Location = c.Club.Location,
                        ClubInitials = GetClubInitials(c.Club.OrganizationName),
                        ClubImageSrc = GetClubImageSource(c.Club),
                        HeroesCount = GetHeroesCount(c.Club),
                        Points = getClubPoints(c.Missions),
                        Club = c.Club,
                        Missions = clubMissions,
                        Heroes = clubHeroes,
                        Coordinator = clubHeroes.FirstOrDefault(h => h.IsCoordinator)
                    };
                })
                .OrderByDescending(c => c.Points);

            var model = new ClubListViewModel()
            {
                Clubs = clubs
            };

            return model;
        }

        private string GetMissionImageSource(Mission mission)
        {
            if (mission.MissionImages != null && mission.MissionImages.Count > 0)
            {
                var missionImage = this.imagesService.GetMissionImage(mission.Id).Result;
                return this.imagesService.GetImageSource(missionImage.Image.ContentType, missionImage.Image.Bytes);
            }

            return null;
        }

        private string GetClubImageSource(Club club)
        {
            if (club.ClubImages != null && club.ClubImages.Count > 0)
            {
                var clubImage = this.imagesService.GetClubImage(club.Id).Result;
                return this.imagesService.GetImageSource(clubImage.Image.ContentType, clubImage.Image.Bytes);
            }

            return null;
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
            return this.missionsService.GetMissionSchoolYears().OrderBy(x => x);
        }

        public string GetCurrentSchoolYear()
        {
            return this.schoolYearService.GetCurrentSchoolYear();
        }
    }
}
