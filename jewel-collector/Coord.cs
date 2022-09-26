using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace jewel_collector
{
    public class Coord : Tuple<int, int>
    {
        public int x {get;private set;}
        public int y {get;private set;}
        public Coord(int item1, int item2) : base(item1, item2) {
            this.x = item1;
            this.y = item2;
        }

    }
}