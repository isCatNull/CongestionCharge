using CongestionCharge.DTOs;
using System.Collections.Generic;

namespace CongestionCharge.Interfaces
{
    public interface IReceiptPrinter
    {
        string Print(IEnumerable<ChargeEvent> chargeEvents);
    }
}
