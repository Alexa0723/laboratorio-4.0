using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Lab4
{
    public partial class Juego : Form
    {
        Matriz nuevaMat;
        PictureBox[,] matPicBox;
        public Juego()
        {
            InitializeComponent();

            nuevaMat = new Matriz(8, 8, 6);
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
                    pictureAux.Location = new Point(x,y);
                    pictureAux.Name = ($"pb{i},{j}");
                    pictureAux.Image = imagenOsos(nuevaMat.valores[i, j]);
                    pictureAux.Click += pictureAux_Click;
                    pictureAux.SizeMode = PictureBoxSizeMode.StretchImage;
                    matPicBox[i, j] = pictureAux;

                    Controls.Add(pictureAux);
                    x += 50;
                }

                y+= 50;

            }

        }


        private void pictureAux_Click(object sender, EventArgs e)
        {

            throw new NotImplementedException();
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



    }

}
