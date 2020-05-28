using ClubsModule.Services;
using Xunit;

namespace HeroesCup.ClubsModule.UnitTests.SchoolYear
{
    public class CalculateSchoolYear_Should
    {
        [Fact]
        public void Return_A_Valid_School_Year_When_Passed_Date_Is_In_May()
        {
            var schoolYearService = new SchoolYearService();
            var schoolYear = schoolYearService.CalculateSchoolYear(new System.DateTime(2020, 5, 27));

            Assert.Equal("2019 / 2020", schoolYear);
        }

        [Fact]
        public void Return_A_Valid_School_Year_When_Passed_Date_Is_In_The_Middle_Of_The_SchoolYear()
        {
            var schoolYearService = new SchoolYearService();
            var schoolYear = schoolYearService.CalculateSchoolYear(new System.DateTime(2020, 1, 27));

            Assert.Equal("2019 / 2020", schoolYear);
        }

        [Fact]
        public void Return_A_Valid_School_Year_When_Passed_Date_Is_In_The_End_Of_The_SchoolYear()
        {
            var schoolYearService = new SchoolYearService();
            var schoolYear = schoolYearService.CalculateSchoolYear(new System.DateTime(2020, 7, 27));

            Assert.Equal("2019 / 2020", schoolYear);
        }

        [Fact]
        public void Return_A_Valid_School_Year_When_Passed_Date_Is_In_The_Start_Of_The_SchoolYear()
        {
            var schoolYearService = new SchoolYearService();
            var schoolYear = schoolYearService.CalculateSchoolYear(new System.DateTime(2020, 8, 27));

            Assert.Equal("2020 / 2021", schoolYear);
        }
    }
}