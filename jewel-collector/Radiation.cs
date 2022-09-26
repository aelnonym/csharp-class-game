using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace jewel_collector
{
    public class Radiation : Prop, Rechargeable, Collisionable
    {
        private int rechargeAmount = -30;
        private int collidePenalty = -10;

        public Radiation(){
            this.image = "!!";
        }
        
        public void Recharge(Robot r){
            r.recharge(rechargeAmount);
        }

        public void Collide(Robot r){
            r.recharge(collidePenalty);
        }
    }
}