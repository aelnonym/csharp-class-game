using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace jewel_collector
{
    public class Obstacle : Prop
    {

        /// <summary>
        /// Construtor
        /// </summary>
        /// <param name="type"> Tipo do obstaculo </param>
        public Obstacle(string type){
            this.type = type;
            switch(type) {
                case "WATER":
                    this.image = "##";
                    this.value = 100;
                    break;
                case "TREE":
                    this.image = "$$";
                    this.value = 10;
                    break;
                default:
                    this.image = "O?";
                    this.value = 0;
                    break;
            }
        }
    }
}