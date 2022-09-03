using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using jewel_collector;

namespace jewel_collector
{
    public class Jewel : Prop
    {
        public Jewel(string type){
            this.type = type;
            switch(type) {
                case "RED":
                    this.value = 100;
                    break;
                case "BLUE":
                    this.value = 10;
                    break;
                case "GREEN":
                    this.value = 50;
                    break;
                default:
                    this.value = 0;
                    break;
            }
        }
    }
}