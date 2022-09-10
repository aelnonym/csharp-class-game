using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using jewel_collector;

namespace jewel_collector
{
    public class Jewel : Prop, Collectable
    {
        public Jewel(string type){
            this.type = type;
            switch(type) {
                case "RED":
                    this.image = "JR";
                    this.value = 100;
                    break;
                case "BLUE":
                    this.image = "JB";
                    this.value = 10;
                    break;
                case "GREEN":
                    this.image = "JG";
                    this.value = 50;
                    break;
                default:
                    this.image = "J?";
                    this.value = 0;
                    break;
            }
        }
    }
}