using CongestionCharge.Implementations;
using System.Collections.Generic;
using Xunit;

namespace CongestionCharge.UnitTests.Implementations
{
    public class RoundingLogicTests
    {
        private readonly RoundingLogic _roundingLogic;

        public RoundingLogicTests()
        {
            _roundingLogic = new RoundingLogic();
        }

        public static IEnumerable<object[]> AmountData = new[]
        {
            new object[]{ 0.81m },
            new object[]{ 0.85m }
        };

        [Theory]
        [MemberData(nameof(AmountData))]
        public void Round_LessOrEqualToHalfIncrement_ReturnsIncrement(decimal amount)
        {
            // Act
            var result = _roundingLogic.Round(amount);

            // Assert
            Assert.Equal(0.80m, result);
        }

        [Fact]
        public void Round_MoreThanHalfIncrement_ReturnsNextIncrement()
        {
            // Act
            var result = _roundingLogic.Round(0.87m);

            // Assert
            Assert.Equal(0.90m, result);
        }
    }
}
