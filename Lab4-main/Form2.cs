using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Lab4
{
    public partial class Form2 : Form
    {
        private Matriz nuevaMat;
        private PictureBox[,] matPicBox;

        private PictureBox selectedCandy1 = null;
        private PictureBox selectedCandy2 = null;

        public Form2()
        {
            InitializeComponent();
            nuevaMat = new Matriz(8, 8, 6); // Asumiendo que Matriz tiene un constructor apropiado
            matrizPictureBox();
        }

        public void matrizPictureBox()
        {
            int filas = nuevaMat.cantidadFil;
            int columnas = nuevaMat.cantidadCol;

            matPicBox = new PictureBox[filas, columnas];

            int y = 25;
            for (int i = 0; i < filas; i++)
            {
                int x = 25;
                for (int j = 0; j < columnas; j++)
                {
                    PictureBox pictureAux = new PictureBox();

                    pictureAux.Size = new Size(50, 50);
                    pictureAux.Location = new Point(x, y);
                    pictureAux.Name = ($"pb{i},{j}");
                    pictureAux.Image = imagenOsos(nuevaMat.valores[i, j]);
                    pictureAux.Click += pictureAux_Click;
                    pictureAux.SizeMode = PictureBoxSizeMode.StretchImage;
                    matPicBox[i, j] = pictureAux;

                    Controls.Add(pictureAux);
                    x += 50;
                }

                y += 50;

            }
        }
        private void pictureAux_Click(object sender, EventArgs e)
        {

            if (selectedCandy1 == null)
            {
                // Primer caramelo seleccionado
                selectedCandy1 = sender as PictureBox;
            }
            else
            {
                // Segundo caramelo seleccionado
                selectedCandy2 = sender as PictureBox;

                // Verificar si son adyacentes
                if (AreAdjacent(selectedCandy1, selectedCandy2))
                {
                    // Realizar intercambio si es posible
                    SwapCandies(selectedCandy1, selectedCandy2);

                    // Verificar coincidencias y actualizar el juego
                    CheckForMatches();

                    // Reiniciar selecciones
                    selectedCandy1 = null;
                    selectedCandy2 = null;
                }
                else
                {
                    // No son adyacentes, resetear selección
                    selectedCandy1 = null;
                    selectedCandy2 = null;
                }
            }
        }

        private Image imagenOsos(int num)
        {
            switch (num)
            {
                case 0:
                    return global::Lab4.Properties.Resources._0;
                case 1:
                    return global::Lab4.Properties.Resources._1;
                case 2:
                    return global::Lab4.Properties.Resources._2;
                case 3:
                    return global::Lab4.Properties.Resources._3;
                case 4:
                    return global::Lab4.Properties.Resources._4;
                default:
                    return global::Lab4.Properties.Resources._5;
            }
        }


        private void label3_Click(object sender, EventArgs e)
        {
            
        }

        private void Form2_Load(object sender, EventArgs e)
        {

        }

        private bool AreAdjacent(PictureBox candy1, PictureBox candy2)
        {
            // Obtener la posición de los osos
            Point pos1 = GetCandyPosition(candy1);
            Point pos2 = GetCandyPosition(candy2);

            // Verificar si son horizontal o verticalmente
            return (Math.Abs(pos1.X - pos2.X) == 1 && pos1.Y == pos2.Y) ||
                   (Math.Abs(pos1.Y - pos2.Y) == 1 && pos1.X == pos2.X);
        }

        private void ReorganizarMat()
        {
            Random rnd = new Random();

            // Desplazar "osos" hacia abajo
            for (int j = 0; j < nuevaMat.cantidadCol; j++)
            {
                for (int i = nuevaMat.cantidadFil - 1; i >= 0; i--)
                {
                    if (nuevaMat.valores[i, j] == -1) // Si es espacio vacío
                    {
                        for (int k = i - 1; k >= 0; k--) // Buscar el primer "oso" por encima
                        {
                            if (nuevaMat.valores[k, j] != -1)
                            {
                                // Intercambiar con el espacio vacío
                                nuevaMat.valores[i, j] = nuevaMat.valores[k, j];
                                nuevaMat.valores[k, j] = -1;

                                // Actualizar las imágenes de los PictureBox
                                matPicBox[i, j].Image = matPicBox[k, j].Image;
                                matPicBox[k, j].Image = null; // o imagen de espacio vacío
                                break;
                            }
                        }
                    }
                }
            }

            // Crea nuevos osos
            for (int j = 0; j < nuevaMat.cantidadCol; j++)
            {
                for (int i = 0; i < nuevaMat.cantidadFil; i++)
                {
                    if (nuevaMat.valores[i, j] == -1)
                    {
                        int candyType = rnd.Next(0, 6); // con esta lo genera aleatorio
                        nuevaMat.valores[i, j] = candyType;
                        matPicBox[i, j].Image = imagenOsos(candyType); // Suponiendo que imagenOsos genera la imagen correcta
                    }
                }
            }
        }


        private Point GetCandyPosition(PictureBox candy)
        {
            // Extraer las coordenadas de la etiqueta del PictureBox
            string[] parts = candy.Name.Split(',');
            int x = int.Parse(parts[0].Substring(2)); // asumiendo que el nombre es "pbX,Y"
            int y = int.Parse(parts[1]);
            return new Point(x, y);
        }


        private void SwapCandies(PictureBox candy1, PictureBox candy2)
        {
            // intercambia imagenes 
            Image tempImage = candy1.Image;
            candy1.Image = candy2.Image;
            candy2.Image = tempImage;

            // intercambiar valores en la matriz
            Point pos1 = GetCandyPosition(candy1);
            Point pos2 = GetCandyPosition(candy2);

            int tempValue = nuevaMat.valores[pos1.X, pos1.Y];
            nuevaMat.valores[pos1.X, pos1.Y] = nuevaMat.valores[pos2.X, pos2.Y];
            nuevaMat.valores[pos2.X, pos2.Y] = tempValue;

        }

        private void CheckForMatches()
        {
            bool[,] isMatched = new bool[nuevaMat.cantidadFil, nuevaMat.cantidadCol];

            // Para ver si hay coincidencias horizontal
            for (int i = 0; i < nuevaMat.cantidadFil; i++)
            {
                for (int j = 0; j < nuevaMat.cantidadCol - 2; j++)
                {
                    int candyType = nuevaMat.valores[i, j];
                    if (candyType != -1 && candyType == nuevaMat.valores[i, j + 1] && candyType == nuevaMat.valores[i, j + 2])
                    {
                        isMatched[i, j] = isMatched[i, j + 1] = isMatched[i, j + 2] = true;
                    }
                }
            }

            //y esta para las coincidencias verticales
            for (int j = 0; j < nuevaMat.cantidadCol; j++)
            {
                for (int i = 0; i < nuevaMat.cantidadFil - 2; i++)
                {
                    int candyType = nuevaMat.valores[i, j];
                    if (candyType != -1 && candyType == nuevaMat.valores[i + 1, j] && candyType == nuevaMat.valores[i + 2, j])
                    {
                        isMatched[i, j] = isMatched[i + 1, j] = isMatched[i + 2, j] = true;
                    }
                }
            }

            // Eliminar las "coincidencias" y que caigan nuevos osos
            for (int i = 0; i < nuevaMat.cantidadFil; i++)
            {
                for (int j = 0; j < nuevaMat.cantidadCol; j++)
                {
                    if (isMatched[i, j])
                    {
                        // Eliminar oso
                        nuevaMat.valores[i, j] = -1; // Suponiendo que -1 representa un espacio vacío
                        matPicBox[i, j].Image = null; // O establecer a una imagen que represente un espacio vacío
                    }
                }
            }

            ReorganizarMat();
        }

    }
}
