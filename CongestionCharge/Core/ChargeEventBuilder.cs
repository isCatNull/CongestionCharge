using CongestionCharge.DTOs;
using CongestionCharge.Interfaces;
using System;
using System.Collections.Generic;

namespace CongestionCharge.Core
{
    public class ChargeEventBuilder
    {
        private readonly ChargeFinder _chargeFinder;
        private readonly ITimeZoneLogic _timeZoneLogic;

        public ChargeEventBuilder(ChargeFinder chargeFinder, ITimeZoneLogic timeZoneLogic)
        {
            _chargeFinder = chargeFinder;
            _timeZoneLogic = timeZoneLogic;
        }

        public IEnumerable<ChargeEvent> Build(DateTime from, DateTime to)
        {
            var chargeEvents = new List<ChargeEvent>();
            var currentInstant = from;

            var charge = _chargeFinder.Find(currentInstant);
            var nextInstant = new TimeLogic(charge).Advance(currentInstant, to);

            while (currentInstant != nextInstant)
            {
                var difference = _timeZoneLogic.Difference(currentInstant, nextInstant);
                var amount = charge.RatePerHour * (decimal)difference.TotalHours;

                var chargeEvent = new ChargeEvent(Duration: difference, Name: charge.Name, Amount: amount);
                chargeEvents.Add(chargeEvent);

                currentInstant = nextInstant;
                charge = _chargeFinder.Find(currentInstant);
                nextInstant = new TimeLogic(charge).Advance(currentInstant, to);
            }

            return chargeEvents;
        }
    }
}
