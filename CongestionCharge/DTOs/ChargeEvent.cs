using System;

namespace CongestionCharge.DTOs
{
    public record ChargeEvent(TimeSpan Duration, string Name, decimal Amount);
}
