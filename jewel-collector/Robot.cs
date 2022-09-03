using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace jewel_collector
{
    public class Robot : Prop
    {
        private List<Prop> backpack;

        public Robot(){
            this.backpack = new List<Prop>();
        }
        
        public void collect(List<Prop> jewels){
            jewels.ForEach((jewel) => {
                backpack.Add(jewel);
            });
        }

        public int getValue(){
            int value = 0;
            this.backpack.ForEach((prop) =>{
                value += prop.value;
            });
            return value;
        }
    }
}