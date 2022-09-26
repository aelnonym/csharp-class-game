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
                    this.image = "JR";
                    this.value = 100;
                    this.backgroundColor = ConsoleColor.Red;
                    this.textColor = ConsoleColor.White;
                    break;
                case "BLUE":
                    this.image = "JB";
                    this.value = 10;
                    this.backgroundColor = ConsoleColor.Blue;
                    this.textColor = ConsoleColor.White;
                    break;
                case "GREEN":
                    this.image = "JG";
                    this.value = 50;
                    this.backgroundColor = ConsoleColor.Green;
                    this.textColor = ConsoleColor.White;
                    break;
                default:
                    this.image = "J?";
                    this.value = 0;
                    this.textColor = ConsoleColor.Magenta;
                    break;
            }
        }
    }
}