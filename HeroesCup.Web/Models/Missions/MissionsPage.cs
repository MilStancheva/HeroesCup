using ClubsModule.Models;
using HeroesCup.Web.Models;
using Piranha.AttributeBuilder;
using Piranha.Models;
using System.Collections.Generic;

namespace HeroesCup.Models
{
    [PageType(Title = "Missions page", UseBlocks = false)]
    [PageTypeRoute(Title = "Missions", Route = "/missions")]
    public class MissionsPage : Page<MissionsPage>
    {
        public IEnumerable<MissionViewModel> Missions { get; set; }

        public IEnumerable<MissionIdeaViewModel> MissionIdeas { get; set; }

        public IDictionary<string, int> MissionsPerLocation { get; set; }

        public int MissionsCount { get; set; }

        public string SelectedLocation { get; set; }

        public MissionsPage()
        {
            Missions = new List<MissionViewModel>();
            MissionIdeas = new List<MissionIdeaViewModel>();
            MissionsPerLocation = new Dictionary<string, int>();
        }
    }
}