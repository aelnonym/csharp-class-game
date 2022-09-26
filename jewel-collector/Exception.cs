using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace jewel_collector
{
    public class OutOfMapException : Exception {}

    public class OccupiedPositionException : Exception {}

    public class RanOutOfEnergyException : Exception {}
}