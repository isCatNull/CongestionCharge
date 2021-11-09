using System;

namespace CongestionCharge.Interfaces
{
    public interface ITimeZoneLogic
    {
        TimeSpan Difference(DateTime from, DateTime to);
    }
}
