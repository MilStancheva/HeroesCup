using Piranha.Extend;
using Piranha.Extend.Fields;
using Piranha.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HeroesCup.Models.Regions
{
    public class BlogMissionRegion
    {
        /// <summary>
        /// Gets/sets the blog mission post organizator.
        /// </summary>
        [Field(Options = FieldOption.HalfWidth)]
        public StringField OrganizedBy { get; set; }

        /// <summary>
        /// Gets/sets the blog mission post location.
        /// </summary>
        [Field(Options = FieldOption.HalfWidth)]
        public StringField Location { get; set; }

        /// <summary>
        /// Gets/sets the blog mission start date.
        /// </summary>
        [Field(Options = FieldOption.HalfWidth)]
        public DateField StartDate { get; set; }

        /// <summary>
        /// Gets/sets the blog mission end date.
        /// </summary>
        [Field(Options = FieldOption.HalfWidth)]
        public DateField EndDate { get; set; }

        public TimeSpan Duration
        {
            get
            {
                return DateTime.Parse(this.EndDate.Value.ToString()).ToUniversalTime() - 
                    DateTime.Parse(this.EndDate.Value.ToString()).ToUniversalTime();
            }
        }

        public String SchoolYear
        {
            get
            {
                var startYear = getStartSchoolYear(this.StartDate.Value);
                var endYear = getEndSchoolYear(DateTime.Parse(startYear));

                return $"{startYear}-{endYear}";
            }
        }

        private String getEndSchoolYear(DateTime? value)
        {
            return (value.Value.Year + 1).ToString();
        }

        private String getStartSchoolYear(DateTime? value)
        {
            var month = value.Value.Month;
            if (month >= 8 && month <= 12)
            {
                var startYear = value.Value.Year;
                return startYear.ToString();
            }
            
            if (month >= 1 && month <= 7)
            {
                var startYear = value.Value.Year - 1;
                return startYear.ToString();
            }

            return value.Value.Year.ToString();
        }
    }
}
