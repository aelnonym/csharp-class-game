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
        /// <summary>
        /// Recarga negativa do consumo da radiação na mesma posição do robo r
        /// </summary>
        /// <param name="r"></param>
        public void Recharge(Robot r){
            r.recharge(rechargeAmount);
        }
        /// <summary>
        /// Resolve a colisão com o robo r penalizando a energia
        /// </summary>
        /// <param name="r"></param>
        public void Collide(Robot r){
            r.recharge(collidePenalty);
        }
    }
}