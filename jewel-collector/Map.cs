using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace jewel_collector
{
    public class Map
    {
        private Robot player;
        private Prop[,] map;

        /// <summary>
        ///     Uses direction input to generate map coords
        /// </summary>
        /// <param name="c">Direction</param>
        /// <param name="x">Coord X</param>
        /// <param name="y">Coord Y</param>
        /// <returns>Tuple containing coords</returns>
        public (int,int) directionToCoord(char c, int x, int y){
            switch(c){
                case 'w':
                    return solveRange(x-1, y);
                case 'a':
                    return solveRange(x, y-1);
                case 's':
                    return solveRange(x+1, y);
                case 'd':
                    return solveRange(x, y+1);
                default:
                    return (0, 0);
            }
        }

        /// <summary>
        ///     Limita os valores das coordenadas x e y de acordo com os limites do mapa
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        private (int, int) solveRange(int x, int y){
            int outx = x < 0 ? 0 : (x > this.getMaxX() - 1 ? this.getMaxX() - 1 : x);
            int outy = y < 0 ? 0 : (y > this.getMaxY() - 1 ? this.getMaxY() - 1 : y);
            return (outx, outy);
        }

        /// <summary>
        /// Construtor de Mapa
        /// </summary>
        /// <param name="sizeX">Limite vertical do mapa</param>
        /// <param name="sizeY">Limite horizontal do mapa</param>
        public Map(int sizeX, int sizeY){
            this.map = new Prop[sizeX,sizeY];
        }

        private int getMaxX(){
            return this.map.GetLength(0);
        }

        private int getMaxY(){
            return this.map.GetLength(1);
        }

        public int moveProp(Prop prop, char direction){
            (int x, int y) pos = directionToCoord(direction, prop.X, prop.Y);
            Prop intent = getProp(pos);
            Console.WriteLine($"Posicao {pos} : {intent}\n");
            
            if (intent == null){
                this.map[prop.X, prop.Y] = null;
                this.map[pos.x, pos.y] = prop;
                prop.X = pos.x;
                prop.Y = pos.y;
            } else {
                return -1;
            }
            return 0;
        }

        public int movePlayer(char direction){
            return this.moveProp(this.player, direction);
        }

        public void setPlayer(Robot robot, int x, int y){
            this.player = robot;
            robot.X = x;
            robot.Y = y;
            map[x, y] = robot;
        }

        public void insertProp(Prop prop, int posX, int posY){
            map[posX,posY] = prop;
            prop.X = posX;
            prop.Y = posY;
        }

        public Prop getProp(int posX, int posY){
            if(posX >= 0 && posX <= this.getMaxX() - 1 && posY >= 0 && posY <= this.getMaxY() - 1){
                return this.map[posX,posY];
            }
            return null;
        }

        public Prop getProp((int X, int Y) pos){
            Console.WriteLine(pos);
            if(pos.X >= 0 && pos.X <= this.getMaxX() - 1 && pos.Y >= 0 && pos.Y <= this.getMaxY() - 1){
                return this.map[pos.X, pos.Y];
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
        
        public void show(bool debug = false){
            for (int i = 0; i < this.map.GetLength(0); i++){
                for (int j = 0; j < this.map.GetLength(1); j++) {
                    Console.Write(this.map[i,j] != null?this.map[i,j].ToString():"-- ");
                }
                Console.Write("\n");
                if(debug){
                    for (int j = 0; j < this.map.GetLength(1); j++) {
                        Console.Write($"{i}{j} ");
                    }
                    Console.Write("\n");
                }
            }
            Console.WriteLine($"Bag total items: {this.player.getBackpackSize()} | Bag total value: {this.player.getBackpackValue()}");
        }


    }
}