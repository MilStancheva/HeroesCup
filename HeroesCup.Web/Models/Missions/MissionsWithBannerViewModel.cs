using System.Collections.Generic;

namespace HeroesCup.Web.Models.Missions
{
    public class MissionsWithBannerViewModel
    {
        public IEnumerable<MissionViewModel> Missions { get; set; }

        public int MissionsCountPerPage { get; set; }
    }
}
