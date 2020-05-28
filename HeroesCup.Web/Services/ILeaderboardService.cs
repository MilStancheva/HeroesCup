using HeroesCup.Web.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HeroesCup.Web.Services
{
    public interface ILeaderboardService
    {
        string GetCurrentSchoolYear();

        IEnumerable<string> GetSchoolYears();

        Task<ClubListViewModel> GetClubsBySchoolYearAsync(string schoolYear);
    }
}