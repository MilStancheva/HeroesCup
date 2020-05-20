using System;
using System.Threading.Tasks;

namespace ClubsModule.Services.Contracts
{
    public interface IPointsService
    {
        Task<int?> getClubPoints(Guid clubId, string schoolYear);
    }
}
