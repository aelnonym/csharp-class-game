using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace jewel_collector
{
    public class Map
    {
        private Prop[,] map;

        public Map(int sizeX, int sizeY){
            this.map = new Prop[sizeX,sizeY];
        }

        private int getMaxX(){
            return this.map.GetLength(0);
        }

        private int getMaxY(){
            return this.map.GetLength(1);
        }

        public void insertProp(Prop prop, int posX, int posY){
            map[posX,posY] = prop;
        }

        public Prop getProp(int posX, int posY){
            if(posX >= 0 && posX < this.getMaxX() && posY <= 0 && posY < this.getMaxY()){
                return this.map[posX,posY];
            }
            return null;
        }

        public Jewel removeJewel(int posX, int posY){
            Jewel j = (Jewel) this.map[posX, posY];
            this.map[posX, posY] = null;
            return j;
        }

        public List<Prop> getNeighborhood(int posX, int posY){
            List<Prop> output = new List<Prop>();
            output.Add(this.getProp(posX + 1, posY));
            output.Add(this.getProp(posX - 1, posY));
            output.Add(this.getProp(posX, posY + 1));
            output.Add(this.getProp(posX, posY - 1));
            return output;
        }
        
        public void toString(){
            for (int i = 0; i < this.map.GetLength(0); i++){
                for (int j = 0; j < this.map.GetLength(1); j++)
                    Console.Write(this.map[i,j] != null?this.map[i,j].ToString():"-- ");
                Console.Write("\n");
            }
        }


    }
}