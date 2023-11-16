using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab4
{
    internal class Matriz
    {
       
        public int cantidadFil { get; set; }

        
        public int cantidadCol { get; set; }

        
        public int[,] valores { get; set; }

        
        public int cantidadOsos { get; set; }

       
        public Matriz(int cantidadFil, int cantidadCol, int cantidadOsos)
        {
            this.cantidadFil = cantidadFil;
            this.cantidadCol = cantidadCol;
            this.valores = new int[cantidadFil, cantidadCol];
            asignarImagenRnd(cantidadOsos);
        }

        
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
