using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace jewel_collector
{
    public class Prop
    {

        /// <summary>
        /// Posição X
        /// </summary>
        /// <value></value>
        public int X { get; set;}

        /// <summary>
        /// Posição Y
        /// </summary>
        /// <value></value>
        public int Y  { get; set; }


        /// <summary>
        /// Nome do Prop
        /// </summary>
        /// <value></value>
        public string name { get; set; }

        /// <summary>
        /// Imagem que será exibida no mapa
        /// </summary>
        /// <value></value>
        public string image { get; set; }

        /// <summary>
        /// Valor do prop
        /// </summary>
        /// <value></value>
        public int value{get; set;}

        public ConsoleColor textColor{get; set;}

        public ConsoleColor backgroundColor{get; set;}

        public string type{get; set;}

        /// <summary>
        /// Deixa bonito para exibição no mapa
        /// </summary>
        /// <returns></returns>
        public override string ToString() {
            return $"{this.image} ";
        }

    }
}