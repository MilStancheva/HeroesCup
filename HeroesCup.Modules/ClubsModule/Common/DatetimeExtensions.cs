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

        public static DateTime ConvertToLocalDateTime(this long timestamp)
        {
            return timestamp.ToUniversalDateTime().ToLocalTime();
        }

        public static DateTime StartOfTheDay(this DateTime dateTime) => new DateTime(
                dateTime.Year,
                dateTime.Month,
                dateTime.Day,
                0, 0, 0, 0, dateTime.Kind);

        public static DateTime EndOfTheDay(this DateTime dateTime) => new DateTime(
                dateTime.Year,
                dateTime.Month,
                dateTime.Day,
                23, 59, 59, 0, dateTime.Kind);
    }
}