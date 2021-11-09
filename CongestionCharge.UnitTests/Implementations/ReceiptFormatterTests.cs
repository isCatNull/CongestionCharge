using CongestionCharge.Implementations;
using Xunit;
using System.Collections.Generic;
using System;

namespace CongestionCharge.UnitTests.Implementations
{
    public class ReceiptFormatterTests
    {
        private readonly ReceiptFormatter _receiptFormatter;

        public ReceiptFormatterTests()
        {
            _receiptFormatter = new ReceiptFormatter();
        }

        public static IEnumerable<object[]> AmountData = new[]
        {
            new object[]{ 0.00m },
            new object[]{ 0.90m },
            new object[]{ 1.00m },
            new object[]{ 10.00m },
            new object[]{ 101.90m }
        };

        [Theory]
        [MemberData(nameof(AmountData))]
        public void Format_PositiveAmount_AddsPoundSign(decimal amount)
        {
            // Act
            var result = _receiptFormatter.Format(amount);

            // Assert
            Assert.Equal($"£{amount}", result);
        }

        [Theory]
        [InlineData(0, 0)]
        [InlineData(0, 1)]
        [InlineData(1, 0)]
        [InlineData(1, 1)]
        [InlineData(1, 10)]
        [InlineData(10, 2)]
        [InlineData(10, 22)]
        [InlineData(72, 32)]
        public void Format_Duration_ReturnsExactDigits(int hours, int minutes)
        {
            // Arrange
            var duration = new TimeSpan(hours: hours, minutes: minutes, seconds: 00);

            // Act
            var result = _receiptFormatter.Format(duration);

            // Assert
            Assert.Equal($"{hours}h {minutes}m", result);
        }
    }
}
