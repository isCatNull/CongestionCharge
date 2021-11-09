using CongestionCharge.DTOs;
using CongestionCharge.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CongestionCharge.Implementations
{
    public class ChargeEventAggregator : IChargeEventAggregator
    {
        private static readonly IEnumerable<string> _relevantChargeEventNames = new List<string>() { "AM", "PM" };

        public IEnumerable<ChargeEvent> Aggregate(IEnumerable<ChargeEvent> chargeEvents)
        {
            var nameToEvents = chargeEvents.ToLookup(chargeEvent => chargeEvent.Name);
            var groupedChargeEvents = new List<ChargeEvent>();

            foreach (var grouping in nameToEvents)
            {
                var duration = GetDuration(grouping);
                var amount = GetAmount(grouping);
                var chargeEvent = new ChargeEvent(Duration: duration, Name: grouping.Key, Amount: amount);
                groupedChargeEvents.Add(chargeEvent);
            }

            var relevantChargeEvents = FilterRelevantChargeEvents(groupedChargeEvents);
            var allRelevantChargeEvents = AddMissingRelevantChargeEvents(relevantChargeEvents);
            return SortChargeEventsAlphabetically(allRelevantChargeEvents);
        }

        private static TimeSpan GetDuration(IEnumerable<ChargeEvent> chargeEvents)
        {
            var ticks = chargeEvents.Sum(chargeEvent => chargeEvent.Duration.Ticks);
            return TimeSpan.FromTicks(ticks);
        }

        private static decimal GetAmount(IEnumerable<ChargeEvent> chargeEvents)
        {
            return chargeEvents.Sum(chargeEvent => chargeEvent.Amount);
        }

        private static IEnumerable<ChargeEvent> FilterRelevantChargeEvents(IEnumerable<ChargeEvent> chargeEvents)
        {
            return chargeEvents.Where(chargeEvent => _relevantChargeEventNames.Contains(chargeEvent.Name));
        }

        private static IEnumerable<ChargeEvent> AddMissingRelevantChargeEvents(IEnumerable<ChargeEvent> chargeEvents)
        {
            var existingNames = chargeEvents.Select(chargeEvent => chargeEvent.Name);
            var missingNames = _relevantChargeEventNames.Except(existingNames);

            var missingChargeEvents = new List<ChargeEvent>();

            foreach (var name in missingNames)
            {
                var chargeEvent = new ChargeEvent(Duration: TimeSpan.Zero, Name: name, Amount: 0.00m);
                missingChargeEvents.Add(chargeEvent);
            }

            return chargeEvents.Union(missingChargeEvents);
        }

        private static IEnumerable<ChargeEvent> SortChargeEventsAlphabetically(IEnumerable<ChargeEvent> chargeEvents)
        {
            return chargeEvents.OrderBy(chargeEvent => chargeEvent.Name);
        }
    }
}
