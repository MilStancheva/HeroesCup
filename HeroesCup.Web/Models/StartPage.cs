using HeroesCup.Models.Regions;
using Piranha.AttributeBuilder;
using Piranha.Extend;
using Piranha.Extend.Fields;
using Piranha.Models;
using System;
using System.Collections.Generic;

namespace HeroesCup.Models
{
    [PageType(Title = "Start page")]
    [PageTypeRoute(Title = "Start", Route = "/")]
    public class StartPage : Page<StartPage>
    {
        [Region(ListTitle = "Carousel")]
        public IList<HeroRegion> Carousel { get; set; }

        [Region(ListTitle = "Heroes Count")]
        public NumberField HeroesCount { get; set; }

        [Region(ListTitle = "Teams Count")]
        public NumberField TeamsCount { get; set; }

        [Region(ListTitle = "Missions Count")]
        public NumberField MissionsCount { get; set; }

        [Region(ListTitle = "Hours Count")]
        public NumberField HoursCount { get; set; }

        /// <summary>
        /// Gets/sets the available linked missions.
        /// </summary>
        public IList<LinkMissionPost> LinkedMissions { get; set; }

        public IList<SchoolClubPost> SchoolClubs { get; set; }

        public IList<String> SchoolYears { get; set; }

        public String SelectedSchoolYear { get; set; }

        /// <summary>
        /// Default constructor.
        /// </summary>
        public StartPage() {
            LinkedMissions = new List<LinkMissionPost>();
            SchoolClubs = new List<SchoolClubPost>();
        }
    }
}