using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace jewel_collector
{
    public class CheatWatcher
    {

        delegate void Cheat();

        static event Cheat OnCheatFound;

        private int where = 0;

        static string[] KonamiCode = {"UpArrow", "UpArrow", "DownArrow", "DownArrow", "LeftArrow", "RightArrow", "LeftArrow", "RightArrow", "B", "A", "Enter"};

        /// <summary>
        /// Registra qual robô ganhará vantagens quando o codigo for acionado
        /// </summary>
        /// <param name="player"></param>
        public CheatWatcher(Robot player){
            OnCheatFound += player.KonamiCode;
        }

        /// <summary>
        ///     Verifica a tecla pressionada e se estiver de acordo com algum codigo avança para a proxima tecla esperada
        /// </summary>
        /// <param name="key">Tecla pressionada</param>
        public void registerKey(string key){
            if(KonamiCode[where] == key){
                where++;
                if(where == KonamiCode.Length) {
                    OnCheatFound();
                    where = 0;
                }
            } else {
                where = 0;
            }
        }

    }
}