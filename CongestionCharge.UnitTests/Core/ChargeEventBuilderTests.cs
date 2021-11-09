using CongestionCharge.Implementations;
using Xunit;
using System;
using CongestionCharge.DTOs;
using CongestionCharge.Interfaces;
using System.Collections.Generic;
using CongestionCharge.Core;
using System.Linq;

namespace CongestionCharge.UnitTests.Core
{
    public class ChargeEventBuilderTests
    {
        private readonly ITimeZoneLogic _timeZoneLogic;

        public ChargeEventBuilderTests()
        {
            _timeZoneLogic = new TimeZoneLogic();
        }

        [Fact]
        public void Build_MultipleChargeTimeSpan_ReturnsCorrectChargeEventCount()
        {
            // Arrange
            var charges = new List<Charge>()
            {
                new Charge(
                    Name: "AM",
                    ApplicableOn: DayOfWeekHelper.Weekdays,
                    From: new TimeSpan(hours: 07, minutes: 00, seconds: 00),
                    To: new TimeSpan(hours: 12, minutes: 00, seconds: 00),
                    RatePerHour: 1.00m),
                new Charge(
                    Name: "PM",
                    ApplicableOn: DayOfWeekHelper.Weekdays,
                    From: new TimeSpan(hours: 12, minutes: 00, seconds: 00),
                    To: new TimeSpan(hours: 19, minutes: 00, seconds: 00),
                    RatePerHour: 2.00m),
                new Charge(
                    Name: "Night",
                    ApplicableOn: DayOfWeekHelper.Weekdays,
                    From: new TimeSpan(hours: 19, minutes: 00, seconds: 00),
                    To: new TimeSpan(hours: 07, minutes: 00, seconds: 00),
                    RatePerHour: 0.00m)
            };

            var chargeFinder = new ChargeFinder(charges);
            var chargeEventBuilder = new ChargeEventBuilder(chargeFinder, _timeZoneLogic);

            // Act
            var chargeEvents = chargeEventBuilder.Build(
                from: new DateTime(2021, 11, 08, 00, 00, 00),
                to: new DateTime(2021, 11, 08, 22, 00, 00));

            // Assert
            Assert.Equal(4, chargeEvents.Count());
        }
    }
}
