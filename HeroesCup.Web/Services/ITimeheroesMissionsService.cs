using HeroesCup.Data.Models;
using System.Collections.Generic;

namespace HeroesCup.Web.Services
{
    public interface ITimeheroesMissionsService
    {
        IEnumerable<MissionIdea> GetMissionIdeas();
    }
}
