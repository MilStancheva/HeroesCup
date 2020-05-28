using ClubsModule.Common;
using ClubsModule.Services.Contracts;
using HeroesCup.Data.Models;
using HeroesCup.Web.Services;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace HeroesCup.Web.UnitTests.Leaderboard
{
    public class GetClubsBySchoolYearAsync_Should
    {
        [Fact]
        public async Task Return_ClubListViewModel_With_Clubs_List_Not_Null_When_There_Are_No_Clubs()
        {
            // Arrange
            var missionsServiceMock = new Mock<IMissionsService>();
            var schoolYearsServiceMock = new Mock<ISchoolYearService>();
            var imageServiceMock = new Mock<IImagesService>();

            var leaderBoardService = new LeaderboardService(missionsServiceMock.Object, schoolYearsServiceMock.Object, imageServiceMock.Object);
            var schoolYear = "2019 / 2020";

            // Act
            var model = await leaderBoardService.GetClubsBySchoolYearAsync(schoolYear);

            // Assert
            Assert.NotNull(model.Clubs);
        }

        [Fact]
        public async Task Return_ClubListViewModel_With_Clubs_List_Count_Equal_To_The_Clubs_For_The_Passed_School_Year()
        {
            // Arrange
            var missionsServiceMock = new Mock<IMissionsService>();
            var schoolYearsServiceMock = new Mock<ISchoolYearService>();
            var imageServiceMock = new Mock<IImagesService>();
            var schoolYear = "2019 / 2020";
            var club = new Club()
            {
                Id = Guid.NewGuid(),
                Description = "Club description",
                Location = "Location",
                Name = "Club's name",
                OwnerId = Guid.NewGuid(),
                OrganizationType = "Type of school",
                OrganizationName = "School name"                
            };
            var hero = new Hero()
            {
               Id = Guid.NewGuid(),
               Club = club,
               ClubId = club.Id,
               Name = "Hero",
               IsCoordinator = false
            };
            var mission = new Mission()
            {
                Id = Guid.NewGuid(),
                OwnerId = club.OwnerId,
                Title = "Mission title",
                Location = "Mission location",
                Club = club,
                ClubId = club.Id,
                Content = "Mission content",
                Type = MissionType.HeroesCupMission,
                SchoolYear = schoolYear,
                Stars = 2,
                IsPublished = true,
                StartDate = new DateTime(2020, 5, 28).ToUnixMilliseconds(),
                EndDate = new DateTime(2020, 6, 28).ToUnixMilliseconds()                
            };
            var heroMission = new HeroMission()
            {
                Hero = hero,
                Mission = mission
            };
            mission.HeroMissions = new List<HeroMission>() { heroMission };
            hero.HeroMissions = new List<HeroMission>() { heroMission };
            club.Missions = new List<Mission>() { mission };
            club.Heroes = new List<Hero>() { hero };
            missionsServiceMock.Setup(m => m.GetMissionsBySchoolYear(schoolYear)).ReturnsAsync(new List<Mission>()
            {
                mission
            });

            var leaderBoardService = new LeaderboardService(missionsServiceMock.Object, schoolYearsServiceMock.Object, imageServiceMock.Object);

            // Act
            var model = await leaderBoardService.GetClubsBySchoolYearAsync(schoolYear);

            // Assert
            Assert.True(model.Clubs.Count() > 0);
        }
    }
}
