using CongestionCharge.Implementations;
using Xunit;
using System.Collections.Generic;
using System;
using CongestionCharge.DTOs;

namespace CongestionCharge.UnitTests.Implementations
{
    public class ChargeEventAggregatorTests
    {
        private readonly ChargeEventAggregator _chargeEventAggregator;

        public ChargeEventAggregatorTests()
        {
            _chargeEventAggregator = new ChargeEventAggregator();
        }

        public static IEnumerable<object[]> ChargeEventData = new[]
        {
            new object[]
            {
                new List<ChargeEvent>()
            },
            new object[]
            {
                new List<ChargeEvent>(){ new ChargeEvent(Duration: TimeSpan.FromHours(2), Name: "AM", Amount: 3.50m) }
            },
            new object[]
            {
                new List<ChargeEvent>(){ new ChargeEvent(Duration: TimeSpan.FromHours(2), Name: "PM", Amount: 3.50m) }
            },
            new object[]
            {
                new List<ChargeEvent>()
                {
                    new ChargeEvent(Duration: TimeSpan.FromHours(2), Name: "AM", Amount: 3.50m),
                    new ChargeEvent(Duration: TimeSpan.FromHours(5), Name: "AM", Amount: 5.50m)
                }
            },
            new object[]
            {
                new List<ChargeEvent>()
                {
                    new ChargeEvent(Duration: TimeSpan.FromHours(2), Name: "PM", Amount: 3.50m),
                    new ChargeEvent(Duration: TimeSpan.FromHours(5), Name: "PM", Amount: 5.50m)
                }
            },
            new object[]
            {
                new List<ChargeEvent>()
                {
                    new ChargeEvent(Duration: TimeSpan.FromHours(2), Name: "PM", Amount: 3.50m),
                    new ChargeEvent(Duration: TimeSpan.FromHours(2), Name: "AM", Amount: 3.50m),
                    new ChargeEvent(Duration: TimeSpan.FromHours(5), Name: "AM", Amount: 5.50m),
                    new ChargeEvent(Duration: TimeSpan.FromHours(5), Name: "PM", Amount: 5.50m)
                }
            },
            new object[]
            {
                new List<ChargeEvent>()
                {
                    new ChargeEvent(Duration: TimeSpan.FromHours(2), Name: "Irrelevant", Amount: 3.50m),
                    new ChargeEvent(Duration: TimeSpan.FromHours(2), Name: "Irrelevant", Amount: 3.50m),
                    new ChargeEvent(Duration: TimeSpan.FromHours(5), Name: "PM", Amount: 5.50m)
                }
            }
        };

        [Theory]
        [MemberData(nameof(ChargeEventData))]
        public void Aggregate_ChargeEvents_ReturnsAmPm(IEnumerable<ChargeEvent> chargeEvents)
        {
            // Act
            var aggregatedChargeEvents = _chargeEventAggregator.Aggregate(chargeEvents);

            // Assert
            Assert.Collection(
                aggregatedChargeEvents,
                aggregatedChargeEvent => Assert.Equal("AM", aggregatedChargeEvent.Name),
                aggregatedChargeEvent => Assert.Equal("PM", aggregatedChargeEvent.Name));
        }
    }
}
