using HeroesCup.Models.Regions;
using HeroesCup.Web.Models;
using Piranha.AttributeBuilder;
using Piranha.Extend;
using Piranha.Models;
using System.Collections.Generic;

namespace HeroesCup.Models
{
    [PageType(Title = "Start page")]
    [PageTypeRoute(Title = "Start", Route = "/")]
    public class StartPage : Page<StartPage>
    {
        [Region(ListTitle = "Carousel")]
        public IList<HeroRegion> Carousel { get; set; }

        public int HeroesCount { get; set; }

        public int ClubsCount { get; set; }

        public int MissionsCount { get; set; }

        public int HoursCount { get; set; }

        /// <summary>
        /// Gets/sets the available linked missions.
        /// </summary>
        public IList<LinkMissionPost> LinkedMissions { get; set; }

        public ClubListViewModel Clubs { get; set; }

        public IEnumerable<string> SchoolYears { get; set; }

        public string SelectedSchoolYear { get; set; }

        /// <summary>
        /// Default constructor.
        /// </summary>
        public StartPage()
        {
            LinkedMissions = new List<LinkMissionPost>();
            SchoolYears = new List<string>();
            Clubs = new ClubListViewModel();
        }
    }
}