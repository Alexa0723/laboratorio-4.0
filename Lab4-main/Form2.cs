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
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Lab4
{

    /// <summary>
    /// Clase que contiene los métodos escenciales del juego.
    /// </summary>
    public partial class Form2 : Form
    {
        private Matriz nuevaMat;
        private PictureBox[,] matPicBox;

        private PictureBox selectedCandy1 = null;
        private PictureBox selectedCandy2 = null;
        int puntos = 0;

        /// <summary>
        /// Método que se ejecuta al iniciar el formulario
        /// </summary>
        public Form2()
        {
            InitializeComponent();
            nuevaMat = new Matriz(8, 8, 6); // Asumiendo que Matriz tiene un constructor apropiado
            matrizPictureBox();

        }

        /// <summary>
        /// Método que crea la matriz de Imagenes
        /// </summary>
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
                    PictureBox oso = new PictureBox();

                    oso.Size = new Size(50, 50);
                    oso.Location = new Point(x, y);
                    oso.Name = ($"{i},{j}");
                    oso.Image = imagenOsos(nuevaMat.valores[i, j]);
                    oso.Click += pictureAux_Click;
                    oso.SizeMode = PictureBoxSizeMode.StretchImage;
                    oso.MouseDown += pictureAux_MouseDown;
                    oso.MouseUp += pictureAux_MouseUp;

                    matPicBox[i, j] = oso;

                    Controls.Add(oso);
                    x += 50;
                }

                y += 50;

            }
        }

        /// <summary>
        /// Asigna las imagenes según el valor de cada posición de la matriz.
        /// </summary>
        /// <param name="num">Valor del elemento en la matriz</param>
        /// <returns></returns>
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

        /// <summary>
        /// Resalta la imagen cuando el usuario la selecciona.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void pictureAux_MouseDown(object sender, MouseEventArgs e)
        {
            var imagen = sender as PictureBox;
            imagen.BorderStyle = BorderStyle.Fixed3D;
        }

        /// <summary>
        /// Devuelve la imagen a la normalidad despues del click.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void pictureAux_MouseUp(object sender, MouseEventArgs e)
        {
            var imagen = sender as PictureBox;
            imagen.BorderStyle = BorderStyle.None;
        }


        int lastSelectedF = -1;
        int lastSelectedC = -1;

        int lastSelectedF2 = -1;
        int lastSelectedC2 = -1;

        /// <summary>
        /// Envía los elementos seleccionados por el jugador a los demás métodos de la clase.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void pictureAux_Click(object sender, EventArgs e)
        {

            if (selectedCandy1 == null)
            {
                // Primer caramelo seleccionado
                selectedCandy1 = sender as PictureBox;

                if (lastSelectedF == -1)
                {
                    // guardar la posicion del seleccionado si es el primero
                    for (int i = 0; i < nuevaMat.cantidadFil; i++)
                    {
                        for (int j = 0; j < nuevaMat.cantidadCol; j++)
                        {
                            if (matPicBox[i, j] == selectedCandy1)
                            {
                                lastSelectedF = i;
                                lastSelectedC = j;
                                break;
                            }
                        }

                        if (lastSelectedF != -1)
                        {
                            break;
                        }
                    }
                }
                else
                {
                    int selectedF = -1;
                    int selectedC = -1;
                    //obtiene la posicion del seleccionado
                    for (int i = 0; i < nuevaMat.cantidadFil; i++)
                    {
                        for (int j = 0; j < nuevaMat.cantidadCol; j++)
                        {
                            if (matPicBox[i, j] == selectedCandy1)
                            {
                                selectedF = i;
                                selectedC = j;

                                break;
                            }
                        }

                        if (lastSelectedF != -1)
                        {
                            break;
                        }
                    }

                    // la compara con la anterior, y si es la misma posicion que antes sale
                    if (lastSelectedF == selectedF && lastSelectedC == selectedC || lastSelectedF2 == selectedF &&
                        lastSelectedC2 == selectedC)
                    {
                        //parar el evento??
                        return;
                    }
                    else //sino guarda esta para la proxima
                    {
                        lastSelectedF = selectedF;
                        lastSelectedC = selectedC;
                    }
                }
            }
            else
            {
                // Segundo caramelo seleccionado
                selectedCandy2 = sender as PictureBox;

                if (lastSelectedF2 == -1)
                {
                    // guardar la posicion del seleccionado si es el primero
                    for (int i = 0; i < nuevaMat.cantidadFil; i++)
                    {
                        for (int j = 0; j < nuevaMat.cantidadCol; j++)
                        {
                            if (matPicBox[i, j] == selectedCandy2)
                            {
                                lastSelectedF2 = i;
                                lastSelectedC2 = j;
                                break;
                            }
                        }

                        if (lastSelectedF2 != -1)
                        {
                            break;
                        }
                    }
                }
                else
                {
                    int selectedF2 = -1;
                    int selectedC2 = -1;
                    //obtiene la posicion del seleccionado 2
                    for (int i = 0; i < nuevaMat.cantidadFil; i++)
                    {
                        for (int j = 0; j < nuevaMat.cantidadCol; j++)
                        {
                            if (matPicBox[i, j] == selectedCandy2)
                            {
                                selectedF2 = i;
                                selectedC2 = j;

                                break;
                            }
                        }

                        if (lastSelectedF != -1)
                        {
                            break;
                        }
                    }

                    // la compara con la anterior, y si es la misma posicion que antes sale
                    if (lastSelectedF == selectedF2 && lastSelectedC == selectedC2 || lastSelectedF2 == selectedF2 &&
                        lastSelectedC2 == selectedC2)
                    {
                        //parar el evento???
                        return;
                    }
                    else //sino guarda esta para la proxima
                    {
                        lastSelectedF = selectedF2;
                        lastSelectedC = selectedC2;
                    }
                }


                // Verificar si son adyacentes
                if (areAdjacent(selectedCandy1, selectedCandy2))
                {

                    // Realizar intercambio si es posible
                    swapCandies(selectedCandy1, selectedCandy2);


                    // Verificar coincidencias y actualizar el juego
                    checkForMatches();

                    // Reiniciar selecciones
                    selectedCandy1 = null;
                    selectedCandy2 = null;
                }
                else
                {
                    // No son adyacentes, resetear selección
                    selectedCandy1 = null;
                    selectedCandy2 = null;

                    pointsCount(0);

                }
            }
        }


        /// <summary>
        /// Verifica si los elementos seleccionados son adyacentes.
        /// </summary>
        /// <param name="candy1">Elemento seleccionado 1</param>
        /// <param name="candy2">lemento seleccionado 2</param>
        /// <returns>Bool si son adyacentes vertical u horizontalmente</returns>
        private bool areAdjacent(PictureBox candy1, PictureBox candy2)
        {
            // Obtener la posición de los osos
            Point pos1 = getCandyPosition(candy1);
            Point pos2 = getCandyPosition(candy2);

            // Verificar si son horizontal o verticalmente
            return (Math.Abs(pos1.X - pos2.X) == 1 && pos1.Y == pos2.Y) || (Math.Abs(pos1.Y - pos2.Y) == 1 && pos1.X == pos2.X);

        }

        /// <summary>
        /// Intercambia los elementos seleccionados
        /// </summary>
        /// <param name="candy1">Elemento seleccionado 1</param>
        /// <param name="candy2">Elemento seleccionado 2</param>
        private void swapCandies(PictureBox candy1, PictureBox candy2)
        {
            // intercambia imagenes 
            Image tempImage = candy1.Image;
            candy1.Image = candy2.Image;
            candy2.Image = tempImage;

            // intercambiar valores en la matriz
            Point pos1 = getCandyPosition(candy1);
            Point pos2 = getCandyPosition(candy2);

            int tempValue = nuevaMat.valores[pos1.X, pos1.Y];
            nuevaMat.valores[pos1.X, pos1.Y] = nuevaMat.valores[pos2.X, pos2.Y];
            nuevaMat.valores[pos2.X, pos2.Y] = tempValue;

        }

        /// <summary>
        /// Obtiene la posición del elemento seleccionado
        /// </summary>
        /// <param name="candy">Elemento seleccionado</param>
        /// <returns>Posicion del elemento</returns>
        private Point getCandyPosition(PictureBox candy)
        {
            // Extraer las coordenadas de la etiqueta del PictureBox
            string[] parts = candy.Name.Split(',');
            int x = int.Parse(parts[0]);
            int y = int.Parse(parts[1]);
            return new Point(x, y);
        }

        /// <summary>
        /// Asigna la cantidad de puntos de acuerdo con la longuitud de la cadena.
        /// </summary>
        /// <param name="num">Numero de elementos de la cadena</param>
        public void pointsCount(int num)
        {
            switch (num)
            {
                case 0:
                    puntos = puntos - 5;
                    lblPuntos.Text = puntos.ToString();
                    break;
                case 2:
                    puntos = puntos + 6;
                    lblPuntos.Text = puntos.ToString();
                    break;
                case 3:
                    puntos = puntos + 10;
                    lblPuntos.Text = puntos.ToString();
                    break;
                case 4:
                    puntos = puntos + 15;
                    lblPuntos.Text = puntos.ToString();
                    break;
                default:
                    break;
            }
        }


        /// <summary>
        /// Revisa si hay filas o columnas de elementos iguales en la matriz.
        /// En caso afirmativo las elimina.
        /// </summary>
        private void checkForMatches()
        {
            bool[,] isMatched = new bool[nuevaMat.cantidadFil, nuevaMat.cantidadCol];

            // Para ver si hay coincidencias horizontal
            for (int i = 0; i < nuevaMat.cantidadFil; i++)
            {
                for (int j = 0; j < nuevaMat.cantidadCol - 2; j++)
                {
                    int candyType = nuevaMat.valores[i, j];

                    /* if (candyType != -1 && candyType == nuevaMat.valores[i, j + 1] && candyType == nuevaMat.valores[i, j + 2])
                     {
                         isMatched[i, j] = isMatched[i, j + 1] = isMatched[i, j + 2] = true;
                     }*/

                    if (candyType != -1)
                    {
                        int matches = 0;

                        //para revisar cuantos son iguales - min 3 max 5
                        for (int k = 1; j + k < nuevaMat.cantidadCol && k < 5; k++)
                        {

                            if (candyType == nuevaMat.valores[i, j + k])
                            {
                                matches++; ; //cada igual le suma uno
                            }
                            else
                            {

                                break; //si no es igual el que sigue termina
                            }

                        }

                        // segun cuantos sean iguales y le suma o resta puntos
                        switch (matches)
                        {
                            case 2:
                                isMatched[i, j] = isMatched[i, j + 1] = isMatched[i, j + 2] = true;
                                break;

                            case 3:
                                isMatched[i, j] = isMatched[i, j + 1] = isMatched[i, j + 2] = isMatched[i, j + 3] = true;
                                break;

                            case 4:
                                isMatched[i, j] = isMatched[i, j + 1] = isMatched[i, j + 2] = isMatched[i, j + 3] = isMatched[i, j + 4] = true;
                                break;

                            default:
                                isMatched[i, j] = false;
                                break;

                        }

                        pointsCount(matches);
                        //revisa toda la matriz entonces se cuentan muchos!

                    }


                }
            }


            //y esta para las coincidencias verticales
            for (int j = 0; j < nuevaMat.cantidadCol; j++)
            {
                for (int i = 0; i < nuevaMat.cantidadFil - 2; i++)
                {
                    int candyType = nuevaMat.valores[i, j];

                    /*if (candyType != -1 && candyType == nuevaMat.valores[i + 1, j] && candyType == nuevaMat.valores[i + 2, j])
                    {
                             isMatched[i, j] = isMatched[i + 1, j] = isMatched[i + 2, j] = true;
                    }*/

                    if (candyType != -1)
                    {
                        int matches = 0;

                        //para revisar cuantos son iguales - min 3 max 5
                        for (int k = 1; i + k < nuevaMat.cantidadFil && k < 5; k++)
                        {

                            if (candyType == nuevaMat.valores[i + k, j])
                            {
                                matches = matches + 1; //cada igual le suma uno
                            }
                            else
                            {
                                break; //si no es igual el que sigue termina
                            }


                        }

                        // segun cuantos sean iguales y le suma o resta puntos
                        switch (matches)
                        {
                            case 2:
                                isMatched[i, j] = isMatched[i + 1, j] = isMatched[i + 2, j] = true;
                                puntos = puntos + 6;
                                lblPuntos.Text = puntos.ToString();
                                break;

                            case 3:
                                isMatched[i, j] = isMatched[i + 1, j] = isMatched[i + 2, j] = isMatched[i + 3, j] = true;
                                puntos = puntos + 10;
                                lblPuntos.Text = puntos.ToString();
                                break;

                            case 4:
                                isMatched[i, j] = isMatched[i + 1, j] = isMatched[i + 2, j] = isMatched[i + 3, j] = isMatched[i + 4, j] = true;
                                puntos = puntos + 15;
                                lblPuntos.Text = puntos.ToString();
                                break;

                            default:
                                isMatched[i, j] = false;
                                puntos = puntos - 5;
                                lblPuntos.Text = puntos.ToString();
                                break;
                        }

                    }

                }


                // Eliminar las "coincidencias" y que caigan nuevos osos
                for (int i = 0; i < nuevaMat.cantidadFil; i++)
                {
                    for (int m = 0; m < nuevaMat.cantidadCol; m++)
                    {
                        if (isMatched[i, m])
                        {
                            // Eliminar oso
                            nuevaMat.valores[i, m] = -1; // Suponiendo que -1 representa un espacio vacío
                            matPicBox[i, m].Image = null; // O establecer a una imagen que represente un espacio vacío

                        }
                    }
                }

                reorganizarMat();
            }

        }

        /// <summary>
        /// Reorganiza y rellena la matriz luego de que se elimien elementos.
        /// </summary>
        private void reorganizarMat()
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

            //revise si hay posibilidad de matches en la mat

        }

        public List<Jugadores> jugadores = new List<Jugadores>();
        private List<Jugadores> mejoresPuntajes = new List<Jugadores>();

        /// <summary>
        /// Guarda la información del jugador
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button1_Click_1(object sender, EventArgs e)
        {
            Jugadores jugador = new Jugadores();
            jugador.puntaje = Convert.ToInt32(lblPuntos.Text);
            jugador.fecha = DateTime.Now.ToString();
            jugador.nombre = textBox1.Text;

            jugadores.Add(jugador);
            mejoresPuntajes = jugadores.OrderByDescending(objeto => objeto.puntaje).Take(5).ToList();

            MessageBox.Show(mejoresPuntajes[0].nombre); //si llega y se guarda la info

            this.Close();
        }
   


        /// <summary>
        /// Devuelve los mejores puntajes, si los hay
        /// </summary>
        /// <returns>Mejores puntajes o mensaje de error</returns>
        public string getObjetoJugador()
        {

            if (mejoresPuntajes.Count > 0)
            {
                string fechaj = mejoresPuntajes[0].fecha;
                string nombrej = mejoresPuntajes[0].nombre;
                string puntajej = mejoresPuntajes[0].puntaje.ToString();

                StringBuilder infoo = new StringBuilder();
                infoo.Append(nombrej + Environment.NewLine);
                infoo.Append(puntajej + Environment.NewLine);
                infoo.Append(fechaj);
                return infoo.ToString();
            }
            else
            {
                return "No hay historico de puntajes altos";
            }


        }

      
    }
}
