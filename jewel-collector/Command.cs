using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace jewel_collector
{
    public class Command
    {

        public event EventHandler keyPressed;

        public virtual void OnKeyPressed(EventArgs e){
            keyPressed?.Invoke(this, e);
        } 
        public Command(Jewel a) {
            if(a is Collectable cc){
                // Usando como Collectable
                cc.Equals(a);
                // Usando como Jewel
                a.Equals(cc);
            }
        }
    }
} 