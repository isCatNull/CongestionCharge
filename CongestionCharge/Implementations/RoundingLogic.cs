using CongestionCharge.Interfaces;
using System;

namespace CongestionCharge.Implementations
{
    public class RoundingLogic : IRoundingLogic
    {
        private static readonly decimal HalfIncrement = 0.05m;

        public decimal Round(decimal value)
        {
            var roundedDownValue = decimal.Round(value, decimals: 1, mode: MidpointRounding.ToZero);

            if (value - roundedDownValue > HalfIncrement)
            {
                return decimal.Round(value, decimals: 1);
            }
            else
            {
                return roundedDownValue;
            }
        }
    }
}
