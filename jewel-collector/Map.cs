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

        public int level{get; set;}

        private static Random _rand = new Random(Guid.NewGuid().GetHashCode());

        private char[] directionsAllowed = {'W', 'A', 'S', 'D'};

        private int jewelAmount;

        /// <summary>
        /// Converte uma direção e coordenadas x,y em uma coordenada na direção escolhida
        /// </summary>
        /// <param name="c">char representando direção</param>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        public Coord directionToCoord(char c, int x, int y){
            switch(c){
                case 'W':
                    return solveRange(x-1, y);
                case 'A':
                    return solveRange(x, y-1);
                case 'S':
                    return solveRange(x+1, y);
                case 'D':
                    return solveRange(x, y+1);
            }
            throw new UnrecognizedDirection();
        }

        /// <summary>
        ///     Limita os valores das coordenadas x e y de acordo com os limites do mapa
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        private Coord solveRange(int x, int y){
            int outx = x < 0 ? 0 : (x > this.getMaxX() - 1 ? this.getMaxX() - 1 : x);
            int outy = y < 0 ? 0 : (y > this.getMaxY() - 1 ? this.getMaxY() - 1 : y);
            return new Coord(outx, outy);
        }

        /// <summary>
        /// Construtor de Mapa
        /// </summary>
        /// <param name="sizeX">Limite vertical do mapa</param>
        /// <param name="sizeY">Limite horizontal do mapa</param>
        /// <param name="level">Nivel do mapa</param>
        public Map(int sizeX=10, int sizeY=10, int level=1){
            int x = sizeX <= 30 ? sizeX : 30;
            int y = sizeY <= 30 ? sizeY : 30;

            this.level = level;
            this.map = new Prop[x,y];

            if (level == 1) this.generateFirst();
            else this.generateRandom();
        }

        /// <summary>
        /// Gera o mapa inicial, sempre o mesmo
        /// </summary>
        private void generateFirst(){
            // Crie e Insira Joias
            var mapa = this;

            mapa.insertProp(new RedJewel(), 1, 9);
            mapa.insertProp(new RedJewel(), 8, 8);
            mapa.insertProp(new GreenJewel(), 9, 1);
            mapa.insertProp(new GreenJewel(), 7, 6);
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

            this.jewelAmount = 6;
        }

        /// <summary>
        /// Verifica se o espaço está em branco ou se será ocupado futuramente pelo jogador
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        private bool emptySpace(int x, int y){
            return this.map[x, y] == null && (x != 0 && y != 0);
        }

        /// <summary>
        /// Gera corpos de água de acordo com o nível atual, propagando para os vizinhos com chance menor
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="chance">chance de gerar uma água no ponto escolhido</param>
        private void generateWaterPool(int x, int y, int chance){
            int roll = _rand.Next(0, 100);
            if(roll < chance){
                if(emptySpace(x, y)) {
                    this.insertProp(new Water(), x, y);
                    var nextSteps = this.getNeighborhoodCoord(x, y);
                    foreach(var coord in nextSteps){
                        if(!(coord.x == x && coord.y == y)){
                            generateWaterPool(coord.x, coord.y, (chance - 25) + (this.getMaxX()/10)*2 );
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Gera mapa aleatório de acordo com o nivel atual
        /// </summary>
        private void generateRandom(){
            Random r = _rand;
            int jewelsToBeInserted = (this.getMaxY()/2) + 1;

            this.jewelAmount = jewelsToBeInserted;

            int obtaclesToBeInserted = 10;
            int radiationToBeInserted = this.getMaxX() / 10;

            int xRandom = r.Next(0, this.getMaxX());
            int yRandom = r.Next(0, this.getMaxY());

            generateWaterPool(xRandom, yRandom, 100);


            while(jewelsToBeInserted != 0){
                xRandom = r.Next(0, this.getMaxX());
                yRandom = r.Next(0, this.getMaxY());

                if(emptySpace(xRandom, yRandom)){
                    if(jewelsToBeInserted > jewelAmount/2){
                        this.insertProp(new BlueJewel(), xRandom, yRandom);
                    } else {
                        this.insertProp(jewelsToBeInserted % 2 == 0 ? new GreenJewel() : new RedJewel(), xRandom, yRandom);
                    }
                    jewelsToBeInserted--;
                }
            }

            while(obtaclesToBeInserted != 0){
                xRandom = r.Next(0, this.getMaxX());
                yRandom = r.Next(0, this.getMaxY());

                if(emptySpace(xRandom, yRandom)){
                    this.insertProp(new Tree(), xRandom, yRandom);
                    obtaclesToBeInserted--;
                }
            }

            while(radiationToBeInserted != 0){
                xRandom = r.Next(0, this.getMaxX());
                yRandom = r.Next(0, this.getMaxY());

                if(emptySpace(xRandom, yRandom)){
                    this.insertProp(new Radiation(), xRandom, yRandom);
                    radiationToBeInserted--;
                }
            }
        }
        /// <summary>
        /// Retorna o valor máximo para X
        /// </summary>
        /// <returns></returns>
        private int getMaxX(){
            return this.map.GetLength(0);
        }

        /// <summary>
        /// Retorna o valor máximo para Y
        /// </summary>
        /// <returns></returns>
        private int getMaxY(){
            return this.map.GetLength(1);
        }

        /// <summary>
        /// Move o Prop na direção escolhida e resolve colisão caso o Prop seja um robo
        /// </summary>
        /// <param name="prop"></param>
        /// <param name="direction"></param>
        /// <returns>Sempre retorna verdadeiro ou joga Exception</returns>
        private bool moveProp(Prop prop, char direction){
            Coord pos = directionToCoord(direction, prop.X, prop.Y);
            Prop intent = getProp(pos);
            
            if (intent == null){
                removeProp(prop);
                insertProp(prop, pos.x, pos.y);
            } else {
                if(intent is Radiation){
                    if(prop is Robot){
                        ((Radiation) intent).Recharge((Robot) prop);
                    }
                    removeProp(prop);
                    insertProp(prop, pos.x, pos.y);
                    return true;
                }
                if(intent is Robot) throw new CoordOutOfBoundsException();
                throw new OccupiedPositionException();
                // return false;
            }
            return true;
        }
        /// <summary>
        /// Verifica se os Props na vizinhança do Robo no mapa tem algum evento de colisão e resolve
        /// </summary>
        private void collide(){
            var vizinhanca = this.getNeighborhood(this.player.X, this.player.Y);
            foreach(Prop p in vizinhanca){
                if(p is Collisionable){
                    ((Collisionable) p).Collide(this.player);
                }
            }
        }

        /// <summary>
        /// Tenta mover o robo na direção escolhida e obtendo sucesso chama a verificação de colisão
        /// </summary>
        /// <param name="direction"></param>
        /// <returns></returns>
        public bool movePlayer(char direction){
            bool status = this.moveProp(this.player, direction);
            if(status) {
                this.collide();
            }
            return status; 
        }
        /// <summary>
        /// Coloca o robô no mapa
        /// </summary>
        /// <param name="robot"></param>
        /// <param name="x"></param>
        /// <param name="y"></param>
        public void setPlayer(Robot robot, int x, int y){
            this.player = robot;
            insertProp(robot, x, y);
        }

        /// <summary>
        /// Tenta inserir um Prop na posição selecionada, não sendo possivel retorna Exception
        /// </summary>
        /// <param name="prop"></param>
        /// <param name="posX"></param>
        /// <param name="posY"></param>
        private void insertProp(Prop prop, int posX, int posY){
            if(map[posX, posY] != null){
                throw new OccupiedPositionException();
            }
            map[posX,posY] = prop;
            prop.X = posX;
            prop.Y = posY;
        }

        /// <summary>
        /// Apaga a referencia de prop no mapa
        /// </summary>
        /// <param name="prop"></param>
        private void removeProp(Prop prop){
            this.map[prop.X, prop.Y] = null;
        }

        /// <summary>
        /// Tenta pegar o que tem nessa posição do mapa, se existir retorna esta Prop
        /// </summary>
        /// <param name="posX"></param>
        /// <param name="posY"></param>
        /// <returns></returns>
        public Prop getProp(int posX, int posY){
            if(posX >= 0 && posX <= this.getMaxX() - 1 && posY >= 0 && posY <= this.getMaxY() - 1){
                return this.map[posX,posY];
            }
            return null;
        }

        /// <summary>
        /// Tenta pegar o que tem nessa Coord do mapa, se existir retorna esta Prop, senão retorna CoordOutOfBoundsException
        /// </summary>
        /// <param name="pos"></param>
        /// <returns></returns>
        public Prop getProp(Coord pos){
            // Console.WriteLine(pos);
            if(pos.x >= 0 && pos.x <= this.getMaxX() - 1 && pos.y >= 0 && pos.y <= this.getMaxY() - 1){
                return this.map[pos.x, pos.y];
            }
            throw new CoordOutOfBoundsException();
        }

        /// <summary>
        /// Remove a joia na posição e reduz o contador de joias do mapa
        /// </summary>
        /// <param name="posX"></param>
        /// <param name="posY"></param>
        /// <returns></returns>
        public Jewel removeJewel(int posX, int posY){
            Jewel j = (Jewel) this.map[posX, posY];
            this.map[posX, posY] = null;
            this.jewelAmount--;
            return j;
        }

        /// <summary>
        /// Remove referencia para joia do mapa e reduz o contador de joias do mapa
        /// </summary>
        /// <param name="jewel"></param>
        /// <returns></returns>
        public Jewel removeJewel(Jewel jewel){
            this.map[jewel.X, jewel.Y] = null;
            this.jewelAmount--;
            return jewel;
        }


        /// <summary>
        /// Retorna uma lista com Props que estão na vizinhança da posição enviada como parametro
        /// </summary>
        /// <param name="posX"></param>
        /// <param name="posY"></param>
        /// <returns></returns>
        public List<Prop> getNeighborhood(int posX, int posY){
            List<Prop> output = new List<Prop>();
            output.Add(this.getProp(posX + 1, posY));
            output.Add(this.getProp(posX - 1, posY));
            output.Add(this.getProp(posX, posY + 1));
            output.Add(this.getProp(posX, posY - 1));
            return output;
        }


        /// <summary>
        /// Retorna uma lista de coordenadas vizinhas a posição enviada como parâmetro
        /// </summary>
        /// <param name="posX"></param>
        /// <param name="posY"></param>
        /// <returns></returns>
        public List<Coord> getNeighborhoodCoord(int posX, int posY){
            List<Coord> output = new List<Coord>();
            foreach(var direction in directionsAllowed){
                try {
                    output.Add(directionToCoord(direction, posX, posY));
                } catch {

                }
            }
            return output;
        }

        /// <summary>
        /// Retorna verdadeiro se acabaram as joias a serem coletadas
        /// </summary>
        /// <returns></returns>
        public bool isDone(){
            if (this.jewelAmount == 0) return true;
            return false;
        }
        

        /// <summary>
        /// Escreve no console o estado atual do mapa, nível atual e outras informações referentes ao robo
        /// </summary>
        /// <param name="debug"></param>
        public void show(bool debug = false){
            
            for (int i = 0; i < this.getMaxX(); i++){
                for (int j = 0; j < this.getMaxY(); j++) {
                    if(this.map[i,j] == null){
                        Console.ForegroundColor = ConsoleColor.Gray;
                        Console.BackgroundColor = ConsoleColor.White;
                    } else {
                        Console.ForegroundColor = this.map[i,j].textColor;
                        Console.BackgroundColor = this.map[i,j].backgroundColor;
                    }
                    Console.Write(this.map[i,j] != null?this.map[i,j].ToString():"-- ");
                }
                Console.ResetColor();
                Console.Write("\n");
                if(debug){
                    for (int j = 0; j < this.map.GetLength(1); j++) {
                        Console.Write($"{i}{j} ");
                    }
                    Console.Write("\n");
                }
            }
            Console.WriteLine($"Level: {this.level}");
            this.player.status();
            Console.WriteLine($"Bag total items: {this.player.getBackpackSize()} | Bag total value: {this.player.getBackpackValue()} ");
            Console.WriteLine($"Jewels remaining: {this.jewelAmount} | Energy remaining: {this.player.energy}");
        }


    }
}