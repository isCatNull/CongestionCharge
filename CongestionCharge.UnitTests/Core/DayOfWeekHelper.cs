using System.Collections.Generic;

namespace CongestionCharge.UnitTests.Core
{
    public static class DayOfWeekHelper
    {
        public static readonly ISet<string> Weekdays = new HashSet<string> { "Monday", "Tuesday", "Wednesday", "Thursday", "Friday" };
        public static readonly ISet<string> Weekend = new HashSet<string> { "Saturday", "Sunday" };
    }
}
