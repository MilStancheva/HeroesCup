using System;

namespace ClubsModule.Common
{
    public static class DatetimeExtensions
    {
        public static readonly DateTime UnixTimeStartUtc
            = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
        public static long ToUnixMilliseconds(
            this DateTime source)
        {
            return (long)
                source
                    .ToUniversalTime()
                    .Subtract(UnixTimeStartUtc)
                    .TotalMilliseconds;
        }
        public static DateTime ToUniversalDateTime(
            this long unixTimestamp)
        {
            return UnixTimeStartUtc
                .AddMilliseconds(Convert.ToDouble(unixTimestamp))
                .ToUniversalTime();
        }
    }
}
