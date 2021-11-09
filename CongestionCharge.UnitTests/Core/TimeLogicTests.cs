using CongestionCharge.Core;
using CongestionCharge.DTOs;
using System;
using Xunit;

namespace CongestionCharge.UnitTests.Core
{
    public class TimeLogicTests
    {
        [Fact]
        public void Advance_LessThanChargeEnd_ReturnsBeforeChargeEnd()
        {
            // Arrange
            var charge = new Charge(
                Name: "Weekend",
                ApplicableOn: DayOfWeekHelper.Weekend,
                From: TimeSpan.Zero,
                To: TimeSpan.Zero,
                RatePerHour: 0.00m);

            var timeLogic = new TimeLogic(charge);
            var to = new DateTime(2021, 11, 06, 13, 00, 00);

            // Act
            var result = timeLogic.Advance(
                from: new DateTime(2021, 11, 06, 12, 00, 00),
                to: to);

            // Assert
            Assert.Equal(to, result);
        }

        [Fact]
        public void Advance_MoreThanChargeEnd_ReturnsChargeEnd()
        {
            // Arrange
            var charge = new Charge(
                Name: "Weekend",
                ApplicableOn: DayOfWeekHelper.Weekend,
                From: TimeSpan.Zero,
                To: TimeSpan.Zero,
                RatePerHour: 0.00m);

            var timeLogic = new TimeLogic(charge);

            // Act
            var result = timeLogic.Advance(
                from: new DateTime(2021, 11, 06, 12, 00, 00),
                to: new DateTime(2021, 11, 07, 13, 00, 00));

            // Assert
            Assert.Equal(new DateTime(2021, 11, 07, 00, 00, 00), result);
        }

        [Fact]
        public void IsOvernight_FromMoreThanTo_ReturnsTrue()
        {
            // Arrange
            var charge = new Charge(
                Name: "Weekend",
                ApplicableOn: DayOfWeekHelper.Weekend,
                From: new TimeSpan(hours: 20, minutes: 00, seconds: 00),
                To: new TimeSpan(hours: 06, minutes: 00, seconds: 00),
                RatePerHour: 0.00m);

            var timeLogic = new TimeLogic(charge);

            // Act
            var result = timeLogic.IsOvernight();

            // Assert
            Assert.True(result);
        }

        [Fact]
        public void IsOvernight_FromLessThanTo_ReturnsFalse()
        {
            // Arrange
            var charge = new Charge(
                Name: "Weekend",
                ApplicableOn: DayOfWeekHelper.Weekend,
                From: new TimeSpan(hours: 06, minutes: 00, seconds: 00),
                To: new TimeSpan(hours: 20, minutes: 00, seconds: 00),
                RatePerHour: 0.00m);

            var timeLogic = new TimeLogic(charge);

            // Act
            var result = timeLogic.IsOvernight();

            // Assert
            Assert.False(result);
        }

        [Fact]
        public void IsAllDay_FromEqualsTo_ReturnsTrue()
        {
            // Arrange
            var charge = new Charge(
                Name: "Weekend",
                ApplicableOn: DayOfWeekHelper.Weekend,
                From: TimeSpan.Zero,
                To: TimeSpan.Zero,
                RatePerHour: 0.00m);

            var timeLogic = new TimeLogic(charge);

            // Act
            var result = timeLogic.IsAllDay();

            // Assert
            Assert.True(result);
        }

        [Fact]
        public void IsAllDay_FromNotEqualsTo_ReturnsFalse()
        {
            // Arrange
            var charge = new Charge(
                Name: "Weekend",
                ApplicableOn: DayOfWeekHelper.Weekend,
                From: new TimeSpan(hours: 06, minutes: 00, seconds: 00),
                To: new TimeSpan(hours: 20, minutes: 00, seconds: 00),
                RatePerHour: 0.00m);

            var timeLogic = new TimeLogic(charge);

            // Act
            var result = timeLogic.IsAllDay();

            // Assert
            Assert.False(result);
        }
    }
}
