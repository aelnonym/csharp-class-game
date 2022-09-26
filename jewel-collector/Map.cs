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

        private int jewelRemaining;

        /// <summary>
        ///     Uses direction input to generate map coords
        /// </summary>
        /// <param name="c">Direction</param>
        /// <param name="x">Coord X</param>
        /// <param name="y">Coord Y</param>
        /// <returns>Tuple containing coords</returns>
        public (int,int) directionToCoord(char c, int x, int y){
            switch(c){
                case 'W':
                    return solveRange(x-1, y);
                case 'A':
                    return solveRange(x, y-1);
                case 'S':
                    return solveRange(x+1, y);
                case 'D':
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
        public Map(int sizeX=10, int sizeY=10, int level=1){
            int x = sizeX <= 30 ? sizeX : 30;
            int y = sizeY <= 30 ? sizeY : 30;

            this.map = new Prop[x,y];

            if (level == 1) this.generateFirst();
            else this.generateRandom();
        }

        private void generateFirst(){
            // Crie e Insira Joias
            var mapa = this;

            mapa.insertProp(new Jewel("RED"), 1, 9);
            mapa.insertProp(new Jewel("RED"), 8, 8);
            mapa.insertProp(new Jewel("GREEN"), 9, 1);
            mapa.insertProp(new Jewel("GREEN"), 7, 6);
            mapa.insertProp(new BlueJewel(), 3, 4);
            mapa.insertProp(new BlueJewel(), 2, 1);


            // Crie e insira Obstaculos
            foreach(int i in Enumerable.Range(0, 7)){
                mapa.insertProp(new Water(), 5, i);
            }

            mapa.insertProp(new Tree(), 5, 9);
            mapa.insertProp(new Tree(), 3, 9);
            mapa.insertProp(new Tree(), 8, 3);
            mapa.insertProp(new Tree(), 2, 5);
            mapa.insertProp(new Tree(), 1, 4);

            this.jewelRemaining = 6;
        }

        private void generateRandom(){
            Random r = new Random(1);
            int jewelsToBeInserted = (this.getMaxY()/2) + 1;
            this.jewelRemaining = jewelsToBeInserted;
            int obtaclesToBeInserted = 10;
            int radiationToBeInserted = 1;


            while(jewelsToBeInserted != 0){
                int xRandom = r.Next(0, this.getMaxX());
                int yRandom = r.Next(0, this.getMaxY());
                // Console.WriteLine($"Trying to add at ({xRandom},{yRandom})");

                if(this.map[xRandom, yRandom] == null){
                    // Console.WriteLine("Added Jewel");
                    if(jewelsToBeInserted > 3){
                        this.insertProp(new BlueJewel(), xRandom, yRandom);
                    } else {
                        this.insertProp(new Jewel("GREEN"), xRandom, yRandom);
                    }
                    jewelsToBeInserted--;
                } else {
                    // Console.WriteLine($"Collision at ({xRandom},{yRandom})");
                }
            }

            while(obtaclesToBeInserted != 0){
                int xRandom = r.Next(0, this.getMaxX());
                int yRandom = r.Next(0, this.getMaxY());

                if(this.map[xRandom, yRandom] == null){
                    // Console.WriteLine("Added Tree");
                    this.insertProp(new Tree(), xRandom, yRandom);
                    obtaclesToBeInserted--;
                } else {
                    // Console.WriteLine($"Collision at ({xRandom},{yRandom})");
                }
            }

            while(radiationToBeInserted != 0){
                int xRandom = r.Next(0, this.getMaxX());
                int yRandom = r.Next(0, this.getMaxY());

                if(this.map[xRandom, yRandom] == null){
                    // Console.WriteLine("Added Tree");
                    this.insertProp(new Radiation(), xRandom, yRandom);
                    radiationToBeInserted--;
                } else {
                    // Console.WriteLine($"Collision at ({xRandom},{yRandom})");
                }
            }
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
            // Console.WriteLine($"Posicao {pos} : {intent}\n");
            
            if (intent == null){
                this.map[prop.X, prop.Y] = null;
                this.map[pos.x, pos.y] = prop;
                prop.X = pos.x;
                prop.Y = pos.y;
            } else {
                if(intent is Radiation){
                    if(prop is Robot){
                        ((Radiation) intent).Recharge((Robot) prop);
                    }
                    this.map[prop.X, prop.Y] = null;
                    this.map[pos.x, pos.y] = prop;
                    prop.X = pos.x;
                    prop.Y = pos.y;
                    return 0;
                }
                return -1;
            }
            return 0;
        }

        private void collide(){
            var vizinhanca = this.getNeighborhood(this.player.X, this.player.Y);
            foreach(Prop p in vizinhanca){
                if(p is Collisionable){
                    ((Collisionable) p).Collide(this.player);
                }
            }
        }

        public int movePlayer(char direction){
            int status = this.moveProp(this.player, direction);
            if(status == 0) {
                this.collide();
            }
            return status; 
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
            this.jewelRemaining--;
            return j;
        }

        public Jewel removeJewel(Jewel jewel){
            this.map[jewel.X, jewel.Y] = null;
            this.jewelRemaining--;
            return jewel;
        }

        public List<Prop> getNeighborhood(int posX, int posY){
            List<Prop> output = new List<Prop>();
            output.Add(this.getProp(posX + 1, posY));
            output.Add(this.getProp(posX - 1, posY));
            output.Add(this.getProp(posX, posY + 1));
            output.Add(this.getProp(posX, posY - 1));
            return output;
        }

        public bool isDone(){
            if (this.jewelRemaining == 0) return true;
            return false;
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
            Console.WriteLine($"Bag total items: {this.player.getBackpackSize()} | Bag total value: {this.player.getBackpackValue()} | ");
            Console.WriteLine($"Jewels remaining: {this.jewelRemaining} | Energy remaining: {this.player.energy}");
        }


    }
}