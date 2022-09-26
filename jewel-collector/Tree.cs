using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace jewel_collector
{
    public class Tree : Obstacle, Rechargeable, Collisionable {

        private int rechargeAmount = 3;

        public Tree() : base("TREE") {}

        public void Recharge(Robot r)
        {
            r.recharge(this.rechargeAmount);
        }

        public void Collide(Robot r){
            this.Recharge(r);
        }
    }
}