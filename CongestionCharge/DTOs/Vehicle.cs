using System.Collections.Generic;

namespace CongestionCharge.DTOs
{
    public record Vehicle(string Type, IEnumerable<Charge> Charges);
}
