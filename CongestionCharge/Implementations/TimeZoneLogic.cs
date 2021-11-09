using CongestionCharge.Interfaces;
using System;

namespace CongestionCharge.Implementations
{
    public class TimeZoneLogic : ITimeZoneLogic
    {
        private static readonly TimeZoneInfo _timeZoneInfo = TimeZoneInfo.FindSystemTimeZoneById("GMT Standard Time");

        public TimeSpan Difference(DateTime from, DateTime to)
        {
            var toAndOffset = new DateTimeOffset(to, _timeZoneInfo.GetUtcOffset(to));
            var fromAndOffset = new DateTimeOffset(from, _timeZoneInfo.GetUtcOffset(from));

            return toAndOffset - fromAndOffset;
        }
    }
}
