using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace jewel_collector
{
    public class Prop
    {
        public int X { get; set;}
        public int Y  { get; set; }

        public string name { get; set; }

        public string image { get; set; }

        public int value{get; set;}

        public string type{get; set;}

        public override string ToString() {
            return $"{this.image} ";
        }

    }
}