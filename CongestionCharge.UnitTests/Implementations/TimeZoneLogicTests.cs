using CongestionCharge.Implementations;
using System;
using Xunit;

namespace CongestionCharge.UnitTests.Implementations
{
    public class TimeZoneLogicTests
    {
        private readonly TimeZoneLogic _timeZoneLogic;

        public TimeZoneLogicTests()
        {
            _timeZoneLogic = new TimeZoneLogic();
        }

        [Fact]
        public void Difference_DstChangeBackOneHour_ReturnsOneHourLonger()
        {
            // Act
            var difference = _timeZoneLogic.Difference(
                from: new DateTime(2021, 10, 31, 00, 00, 00),
                to: new DateTime(2021, 10, 31, 04, 00, 00));

            // Assert
            Assert.Equal(TimeSpan.FromHours(5), difference);
        }

        [Fact]
        public void Difference_DstChangeForwardOneHour_ReturnsOneHourShorter()
        {
            // Act
            var difference = _timeZoneLogic.Difference(
                from: new DateTime(2021, 03, 28, 00, 00, 00),
                to: new DateTime(2021, 03, 28, 04, 00, 00));

            // Assert
            Assert.Equal(TimeSpan.FromHours(3), difference);
        }

        [Fact]
        public void Difference_NoDstChange_ReturnsDifference()
        {
            // Act
            var difference = _timeZoneLogic.Difference(
                from: new DateTime(2021, 06, 06, 00, 00, 00),
                to: new DateTime(2021, 06, 06, 06, 00, 00));

            // Assert
            Assert.Equal(TimeSpan.FromHours(6), difference);
        }
    }
}
