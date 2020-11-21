using ClubsModule.Common;
using System;

namespace HeroesCup.Web.Common.Extensions
{
    public static class DateTimeExtensions
    {
        public static bool IsExpired(this long endDate)
        {
            var today = DateTime.Now.Date;
            var expiredMission = false;
            if (today > endDate.ConvertToLocalDateTime().Date)
            {
                expiredMission = true;
            }

            return expiredMission;
        }
    }
}
