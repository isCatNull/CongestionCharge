using CongestionCharge.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CongestionCharge.Core
{
    public class ChargeFinder
    {
        private readonly IEnumerable<Charge> _charges;

        public ChargeFinder(IEnumerable<Charge> charges)
        {
            _charges = charges;
        }

        public Charge Find(DateTime instant)
        {
            return _charges.Single(charge => IsChargeApplicable(charge, instant));
        }

        private static bool IsChargeApplicable(Charge charge, DateTime instant)
        {
            return IsDayOfWeekApplicable(charge, instant) &&
                   IsTimeOfDayApplicable(charge, instant);
        }

        private static bool IsDayOfWeekApplicable(Charge charge, DateTime instant)
        {
            return charge.ApplicableOn.Contains(instant.DayOfWeek.ToString());
        }

        private static bool IsTimeOfDayApplicable(Charge charge, DateTime instant)
        {
            return new TimeLogic(charge).IsAllDay() ||
                   IsWithinChargeTimesOnSameDay(charge, instant) ||
                   IsWithinChargeTimesOvernight(charge, instant);
        }

        private static bool IsWithinChargeTimesOnSameDay(Charge charge, DateTime instant)
        {
            return instant.TimeOfDay >= charge.From && instant.TimeOfDay < charge.To;
        }

        private static bool IsWithinChargeTimesOvernight(Charge charge, DateTime instant)
        {
            var timeLogic = new TimeLogic(charge);
            return (timeLogic.IsOvernight() && instant.TimeOfDay >= charge.From) ||
                   (timeLogic.IsOvernight() && instant.TimeOfDay < charge.To);
        }
    }
}
