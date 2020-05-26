using System;

namespace ClubsModule.Services.Contracts
{
    public interface ISchoolYearService
    {
        string CalculateSchoolYear(DateTime startDate);

        string GetCurrentSchoolYear();
    }
}
