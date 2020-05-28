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
        public async Task Throw_When_Passed_School_Year_Is_Null()
        {
            // Arrange
            var missionsServiceMock = new Mock<IMissionsService>();
            var schoolYearsServiceMock = new Mock<ISchoolYearService>();
            var imageServiceMock = new Mock<IImagesService>();

            var leaderBoardService = new LeaderboardService(missionsServiceMock.Object, schoolYearsServiceMock.Object, imageServiceMock.Object);
            string schoolYear = null;

            // Act & Assert
            await Assert.ThrowsAsync<ArgumentException>(async () => {
                await leaderBoardService.GetClubsBySchoolYearAsync(schoolYear);
            });
        }

        [Fact]
        public async Task Throw_When_Passed_School_Year_Is_Empty()
        {
            // Arrange
            var missionsServiceMock = new Mock<IMissionsService>();
            var schoolYearsServiceMock = new Mock<ISchoolYearService>();
            var imageServiceMock = new Mock<IImagesService>();

            var leaderBoardService = new LeaderboardService(missionsServiceMock.Object, schoolYearsServiceMock.Object, imageServiceMock.Object);
            string schoolYear = string.Empty;

            // Act & Assert
            await Assert.ThrowsAsync<ArgumentException>(async () => {
                await leaderBoardService.GetClubsBySchoolYearAsync(schoolYear);
            });
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

        [Fact]
        public async Task Return_ClubListViewModel_With_Calculated_Points_For_Every_Club()
        {
            // Arrange
            var missionsServiceMock = new Mock<IMissionsService>();
            var schoolYearsServiceMock = new Mock<ISchoolYearService>();
            var imageServiceMock = new Mock<IImagesService>();
            var schoolYear = "2019 / 2020";
            var clubId = Guid.NewGuid();
            var club = new Club()
            {
                Id = clubId,
                Description = "Club description",
                Location = "Location",
                Name = "Club's name",
                OwnerId = Guid.NewGuid(),
                OrganizationType = "Type of school",
                OrganizationName = "School name"
            };
            var secondClubId = Guid.NewGuid();
            var secondClub = new Club()
            {
                Id = secondClubId,
                Description = "Second club description",
                Location = "Second Location",
                Name = "Second club's name",
                OwnerId = Guid.NewGuid(),
                OrganizationType = "Second Type of school",
                OrganizationName = "Second school name"
            };
            var hero = new Hero()
            {
                Id = Guid.NewGuid(),
                Club = club,
                ClubId = club.Id,
                Name = "Hero",
                IsCoordinator = false
            };
            var secondHero = new Hero()
            {
                Id = Guid.NewGuid(),
                Club = secondClub,
                ClubId = secondClub.Id,
                Name = "Second Hero",
                IsCoordinator = true
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
            var secondMission = new Mission()
            {
                Id = Guid.NewGuid(),
                OwnerId = secondClub.OwnerId,
                Title = "Second mission title",
                Location = "Second mission location",
                Club = secondClub,
                ClubId = secondClub.Id,
                Content = "Second mission content",
                Type = MissionType.HeroesCupMission,
                SchoolYear = schoolYear,
                Stars = 3,
                IsPublished = true,
                StartDate = new DateTime(2020, 5, 28).ToUnixMilliseconds(),
                EndDate = new DateTime(2020, 6, 28).ToUnixMilliseconds()
            };
            var heroMission = new HeroMission()
            {
                Hero = hero,
                Mission = mission
            };
            var secondHeroMission = new HeroMission()
            {
                Hero = secondHero,
                Mission = secondMission
            };
            mission.HeroMissions = new List<HeroMission>() { heroMission };
            hero.HeroMissions = new List<HeroMission>() { heroMission };
            club.Missions = new List<Mission>() { mission };
            club.Heroes = new List<Hero>() { hero };

            secondMission.HeroMissions = new List<HeroMission>() { secondHeroMission };
            secondHero.HeroMissions = new List<HeroMission>() { secondHeroMission };
            secondClub.Missions = new List<Mission>() { secondMission };
            secondClub.Heroes = new List<Hero>() { secondHero };

            missionsServiceMock.Setup(m => m.GetMissionsBySchoolYear(schoolYear)).ReturnsAsync(new List<Mission>()
            {
                mission, 
                secondMission
            });

            var leaderBoardService = new LeaderboardService(missionsServiceMock.Object, schoolYearsServiceMock.Object, imageServiceMock.Object);

            // Act
            var model = await leaderBoardService.GetClubsBySchoolYearAsync(schoolYear);
            var clubListItem = model.Clubs.FirstOrDefault(c => c.Id == clubId);
            var secondClubListItem = model.Clubs.FirstOrDefault(c => c.Id == secondClubId);

            // Assert
            Assert.True(clubListItem.Points > 0);
            Assert.Equal(club.Heroes.Count * mission.Stars, clubListItem.Points);
            Assert.True(secondClubListItem.Points > 0);
            Assert.Equal(secondClub.Heroes.Count * secondMission.Stars, secondClubListItem.Points);
        }
    }
}
