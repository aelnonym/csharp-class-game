using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace jewel_collector
{
    public class Tree : Obstacle, Rechargeable, Collisionable {

        private int rechargeAmount = 3;

        public Tree() : base("TREE") {}

        /// <summary>
        /// Recarrega o robo r com a quantia definida pelas arvores
        /// </summary>
        /// <param name="r"></param>
        public void Recharge(Robot r)
        {
            r.recharge(this.rechargeAmount);
        }

        /// <summary>
        /// Resolve a colisão com arvore gerando uma recarga no robo
        /// </summary>
        /// <param name="r"></param>
        public void Collide(Robot r){
            this.Recharge(r);
        }
    }
}