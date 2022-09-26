using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace jewel_collector
{
    public class Robot : Prop
    {
        private List<Prop> backpack;

        public int energy {get; private set;}

        private Map map;

        public Robot(Map map){
            this.backpack = new List<Prop>();
            this.map = map;
            this.image = "ME";
            this.energy = 5;
        }
        
        public void collect(List<Prop> jewels){
            jewels.ForEach((jewel) => {
                if(jewel is Jewel){
                    if(jewel is Rechargeable) ((Rechargeable) jewel).Recharge(this);
                    backpack.Add(map.removeJewel((Jewel) jewel));
                    //map.removeJewel((Jewel) jewel);
                }
            });
        }

        public void recharge(int qtd){
            this.energy += qtd;
        }

        public void collect(){
            collect(this.map.getNeighborhood(this.X, this.Y));
        }

        public void collect(Jewel jewel){
            backpack.Add(jewel);
            map.removeJewel(jewel.X, jewel.Y);
        }

        public int getBackpackSize(){
            return backpack.Count;
        }

        public int getBackpackValue(){
            int value = 0;
            this.backpack.ForEach((prop) =>{
                value += prop.value;
            });
            return value;
        }

        public void move(char direction){
            Console.WriteLine(direction);
            try{
                int value = map.movePlayer(direction);
                if (value == 0){
                    energy--;
                }
            }
            catch (OccupiedPositionException e)
            {
                Console.WriteLine($"Position is occupied");
            }
            catch (OutOfMapException e)
            {
                Console.WriteLine($"Position is out of map");
            }
            catch (Exception e)
            {
                Console.WriteLine($"Position is prohibit");
            }    
        }
    }
}