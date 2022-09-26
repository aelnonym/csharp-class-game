using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace jewel_collector
{
    public class BlueJewel : Jewel, Rechargeable
    {
        public BlueJewel() : base("BLUE"){}

        private int rechargeAmount = 5;

        public void Recharge(Robot r){
            r.recharge(this.rechargeAmount);
        }
    }
}