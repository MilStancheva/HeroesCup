using ClubsModule.Services.Contracts;
using System;

namespace ClubsModule.Services
{
    public class SchoolYearService : ISchoolYearService
    {
        public string GetCurrentSchoolYear()
        {
            return CalculateSchoolYear(DateTime.UtcNow);
        }

        public string CalculateSchoolYear(DateTime startDate)
        {
            var startYear = getStartSchoolYear(startDate);
            var endYear = getEndSchoolYear(int.Parse(startYear));

            return $"{startYear} / {endYear}";
        }

        private string getEndSchoolYear(int startYear)
        {
            return (startYear + 1).ToString();
        }

        private string getStartSchoolYear(DateTime? startDate)
        {
            var month = startDate.Value.Month;
            if (month >= 8 && month <= 12)
            {
                var startYear = startDate.Value.Year;
                return startYear.ToString();
            }

            if (month >= 1 && month <= 7)
            {
                var startYear = startDate.Value.Year - 1;
                return startYear.ToString();
            }

            return startDate.Value.Year.ToString();
        }
    }
}
