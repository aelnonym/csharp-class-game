using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace jewel_collector
{
    public class CoordOutOfBoundsException : Exception {}

    public class OccupiedPositionException : Exception {}

    public class RanOutOfEnergyException : Exception {}

    public class UnrecognizedDirection : Exception {}

    public class StuckException : Exception {}
}