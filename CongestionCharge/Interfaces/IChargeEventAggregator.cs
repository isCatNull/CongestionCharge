using CongestionCharge.DTOs;
using System.Collections.Generic;

namespace CongestionCharge.Interfaces
{
    public interface IChargeEventAggregator
    {
        IEnumerable<ChargeEvent> Aggregate(IEnumerable<ChargeEvent> chargeEvents);
    }
}
