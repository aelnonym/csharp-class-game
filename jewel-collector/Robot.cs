using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace jewel_collector
{
    public class Robot : Prop
    {
        /// <summary>
        /// Inventário do robo
        /// </summary>
        private List<Prop> backpack;

        /// <summary>
        /// Energia do robo
        /// </summary>
        /// <value></value>
        public int energy { get; private set; }

        private bool cheating = false;

        /// <summary>
        /// Referencia para o mapa aonde o robo está
        /// </summary>
        private Map map;

        public Robot(Map map)
        {
            this.backpack = new List<Prop>();
            this.map = map;
            this.image = "ME";
            this.energy = 5;
            this.backgroundColor = ConsoleColor.Black;
            this.textColor = ConsoleColor.White;
        }

        /// <summary>
        /// Escreve no console se KonamiKode estiver ativo
        /// </summary>
        public void status()
        {
            if (cheating)
            {
                string msg = "KonamiCode Enabled!";
                for(int i = 0; i < msg.Length; i++){
                    Console.ForegroundColor = (System.ConsoleColor)((map.level + i) % 16);
                    Console.BackgroundColor = (System.ConsoleColor)((map.level + 2 + i) % 16);
                    Console.Write(msg[i]);
                }
                Console.ResetColor();
                Console.WriteLine();
            }
        }
        /// <summary>
        /// Verifica se o KonamiKode está ativo e se a tecla passada como parâmetro é a tecla escondida
        /// </summary>
        /// <param name="move"></param>
        /// <returns></returns>
        public bool checkCheatMove(string move)
        {
            if (cheating && move == "O")
            {
                return true;
            }
            return false;
        }
        /// <summary>
        /// Adiciona todas as jóias de uma lista para o inventário, inclusive carregando a energia caso a jóia possibilite
        /// </summary>
        /// <param name="jewels"></param>
        public void collect(List<Prop> jewels)
        {
            jewels.ForEach((jewel) =>
            {
                if (jewel is Jewel)
                {
                    if (jewel is Rechargeable) ((Rechargeable)jewel).Recharge(this);
                    backpack.Add(map.removeJewel((Jewel)jewel));
                    Console.WriteLine($"Added {jewel.image}(Value: {jewel.value}) to bag.");
                }
            });
        }
        /// <summary>
        /// Recarrega a energia do robô com a quantidade informada
        /// </summary>
        /// <param name="qtd">quantia de energia</param>
        public void recharge(int qtd)
        {
            this.energy += qtd;
        }
        /// <summary>
        /// Retorna quantas joias existem no inventário
        /// </summary>
        /// <returns></returns>
        public int getBackpackSize()
        {
            return backpack.Count;
        }
        /// <summary>
        /// Retorna a soma dos valores das joias no inventário
        /// </summary>
        /// <returns></returns>
        public int getBackpackValue()
        {
            int value = 0;
            this.backpack.ForEach((prop) =>
            {
                value += prop.value;
            });
            return value;
        }
        /// <summary>
        /// Ativa KonamiCode
        /// </summary>
        public void KonamiCode()
        {
            this.cheating = true;
        }
        /// <summary>
        /// Retorna se o robô tem energia suficiente para alguma ação, incluindo coleta da vizinhança
        /// </summary>
        /// <returns></returns>
        public bool HasEnergy()
        {
            return energy >= 0 || this.cheating;
        }
        /// <summary>
        /// Tenta mover o robô na direção desejada
        /// </summary>
        /// <param name="direction">char representando a direção a ser movida</param>
        public void move(char direction)
        {
            try
            {
                bool success = map.movePlayer(direction);
                if (success)
                {
                    energy--;
                }
            }
            catch (OccupiedPositionException e)
            {
                Console.WriteLine($"Position is occupied");
            }
            catch (CoordOutOfBoundsException e)
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