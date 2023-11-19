using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab4
{
   /// <summary>
   /// Clase que administra la información de los jugadores.
   /// </summary>
    public class Jugadores
    {
        /// <summary>
        /// Setter y getter del puntaje que obtuvo el jugador.
        /// </summary>
        public int puntaje { get; set; }

        /// <summary>
        /// Setter y getter del nombre del jugador.
        /// </summary>
        public string nombre { get; set; }

        /// <summary>
        /// Setter y getter de la fecha en la que jugó.
        /// </summary>
        public string fecha { get; set; }
    }
}
