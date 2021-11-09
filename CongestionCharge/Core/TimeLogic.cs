using CongestionCharge.DTOs;
using System;

namespace CongestionCharge.Core
{
    public class TimeLogic
    {
        private readonly Charge _charge;

        public TimeLogic(Charge charge)
        {
            _charge = charge;
        }

        public DateTime Advance(DateTime from, DateTime to)
        {
            var localChargeTo = new DateTime(
                from.Year, from.Month, from.Day, _charge.To.Hours, _charge.To.Minutes, _charge.To.Seconds);

            if (from > localChargeTo && IsOvernight() || IsAllDay())
            {
                localChargeTo = localChargeTo.AddDays(1);
            }

            return to > localChargeTo
                ? localChargeTo
                : to;
        }

        public bool IsOvernight()
        {
            return _charge.To < _charge.From;
        }

        public bool IsAllDay()
        {
            return _charge.To == _charge.From;
        }
    }
}
