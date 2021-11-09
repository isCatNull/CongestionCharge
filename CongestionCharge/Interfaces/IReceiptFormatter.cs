using System;

namespace CongestionCharge.Interfaces
{
    public interface IReceiptFormatter
    {
        public string Format(TimeSpan duration);
        public string Format(decimal amount);
    }
}
