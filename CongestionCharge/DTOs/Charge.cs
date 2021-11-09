using System;
using System.Collections.Generic;

namespace CongestionCharge.DTOs
{
    public record Charge(string Name, ISet<string> ApplicableOn, TimeSpan From, TimeSpan To, decimal RatePerHour);
}
