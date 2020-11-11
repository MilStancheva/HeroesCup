using HeroesCup.Models.Regions;
using HeroesCup.Web.Models;
using Piranha.AttributeBuilder;
using Piranha.Extend;
using Piranha.Models;
using System.Collections.Generic;

namespace HeroesCup.Models
{
    [PageType(Title = "Start page")]
    [ContentTypeRoute(Title = "Start", Route = "/")]
    public class StartPage : Page<StartPage>, ISocialNetworkPost
    {
        [Region(ListTitle = "Carousel")]
        public IList<HeroRegion> Carousel { get; set; }

        public int HeroesCount { get; set; }

        public int ClubsCount { get; set; }

        public int MissionsCount { get; set; }

        public int HoursCount { get; set; }

        /// <summary>
        /// Gets/sets the available Timeheroes missions.
        /// </summary>
        public IEnumerable<MissionViewModel> Missions { get; set; }

        public ClubListViewModel Clubs { get; set; }

        public IEnumerable<string> SchoolYears { get; set; }

        public string SelectedSchoolYear { get; set; }

        public SocialNetworksMetaData SocialNetworksMetaData { get; set; }

        /// <summary>
        /// Default constructor.
        /// </summary>
        public StartPage()
        {
            Missions = new List<MissionViewModel>();
            SchoolYears = new List<string>();
            Clubs = new ClubListViewModel();
        }
    }
}