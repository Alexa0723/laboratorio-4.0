using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab4
{
    /// <summary>
    /// Clase que administra la información de la matriz
    /// </summary>
    internal class Matriz
    {
        /// <summary>
        /// Getter y setter de la cantidad de filas que va a tener la matriz
        /// </summary>
        public int cantidadFil { get; set; }

        /// <summary>
        /// Getter y setter de la cantidad de columnas que va a tener la matriz
        /// </summary>
        public int cantidadCol { get; set; }

        /// <summary>
        /// Getter y setter de los valores que va a tener cada elemento de la matriz
        /// </summary>
        public int[,] valores { get; set; }

        /// <summary>
        /// Getter y setter de la cantidad de diferentes elementos que va a tener la matriz
        /// </summary>
        public int cantidadOsos { get; set; }

        /// <summary>
        /// Constructor de la clase
        /// </summary>
        public Matriz(int cantidadFil, int cantidadCol, int cantidadOsos)
        {
            this.cantidadFil = cantidadFil;
            this.cantidadCol = cantidadCol;
            this.valores = new int[cantidadFil, cantidadCol];
            asignarImagenRnd(cantidadOsos);
        }

        /// <summary>
        /// Asigna el valor a cada elemento de la matriz de manera aleatoria
        /// </summary>
        /// <param name="cantidadOsos">Cantidad de elementos diferentes</param>
        private void asignarImagenRnd(int cantidadOsos)
        {
            Random rand = new Random();
            for (int i = 0; i < valores.GetLength(0); i++)
            {
                for (int j = 0; j < valores.GetLength(1); j++)
                {
                    valores[i, j] = rand.Next(cantidadOsos);
                }
            }
        }

    }
}
