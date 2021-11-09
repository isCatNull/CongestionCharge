using System.Collections.Generic;

namespace CongestionCharge.DTOs
{
    public record Config(IEnumerable<Vehicle> Vehicles);
}
