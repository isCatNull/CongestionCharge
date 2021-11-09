using CongestionCharge.Core;
using CongestionCharge.DTOs;
using System;
using System.Collections.Generic;
using Xunit;

namespace CongestionCharge.UnitTests.Core
{
    public class ChargeFinderTests
    {
        [Fact]
        public void Find_DayOfWeek_ReturnsCorrectDayOfWeekCharge()
        {
            // Arrange
            var charges = new List<Charge>()
            {
                new Charge(
                    Name: "Weekdays",
                    ApplicableOn: DayOfWeekHelper.Weekdays,
                    From: TimeSpan.Zero,
                    To: TimeSpan.Zero,
                    RatePerHour: 1.00m),
                new Charge(
                    Name: "Weekend",
                    ApplicableOn: DayOfWeekHelper.Weekend,
                    From: TimeSpan.Zero,
                    To: TimeSpan.Zero,
                    RatePerHour: 0.00m)
            };

            var chargeFinder = new ChargeFinder(charges);

            // Act
            var result = chargeFinder.Find(new DateTime(2021, 11, 08));

            // Assert
            Assert.Equal("Weekdays", result.Name);
        }

        [Fact]
        public void Find_TimeOfDay_ReturnsCorrectTimeOfDayCharge()
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
                    RatePerHour: 2.00m)
            };

            var chargeFinder = new ChargeFinder(charges);

            // Act
            var result = chargeFinder.Find(new DateTime(2021, 11, 08, 11, 30, 00));

            // Assert
            Assert.Equal("AM", result.Name);
        }
    }
}
