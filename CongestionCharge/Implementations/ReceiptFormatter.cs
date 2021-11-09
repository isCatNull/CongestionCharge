using CongestionCharge.Interfaces;
using System;
using System.Globalization;

namespace CongestionCharge.Implementations
{
    public class ReceiptFormatter : IReceiptFormatter
    {
        private readonly static CultureInfo UkCultureInfo = CultureInfo.GetCultureInfo("en-GB");

        public string Format(TimeSpan duration)
        {
            return $"{(int)duration.TotalHours}h {duration:%m}m";
        }

        public string Format(decimal amount)
        {
            return amount.ToString("C", UkCultureInfo);
        }
    }
}
