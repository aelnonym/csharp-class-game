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
            this.setType = type;
            switch(type) {
                case "RED":
                    this.value = 100;
                    break;
                case "BLUE":
                    this.value = 10;
                    break;
                case "GREEN":
                    this.setValue(50);
                    break;
                default:
                    this.setValue(0);
                    break;
            }
        }
    }
}