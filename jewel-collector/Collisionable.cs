using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace jewel_collector
{
    public interface Collisionable
    {
        public void Collide(Robot r);
    }
}