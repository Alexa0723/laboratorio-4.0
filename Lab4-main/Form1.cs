using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Lab4
{
    /// <summary>
    /// Clase que contiene los métodos del Form1.
    /// </summary>
    public partial class Form1 : Form
    {
        /// <summary>
        /// Método generado automaticamente
        /// </summary>
        public Form1()
        {
            InitializeComponent();

        }

        /// <summary>
        /// Método que se ejecuta al dar click en el botón Jugar.
        /// Guarda la información del jugador y lo envía a la pantalla de juego.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnInciar_Click(object sender, EventArgs e)
        {

            Form2 formJuego = new Form2();
            formJuego.ShowDialog();
            button1.Enabled = true;
        }

        /// <summary>
        /// Muestra los mejores 5 puntajes
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button1_Click(object sender, EventArgs e)
        {
            Form2 form2 = new Form2();
            label2.Text = form2.getObjetoJugador(); //?
        }

      
    }
}


