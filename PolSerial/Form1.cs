using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PolSerial
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            WindowState = FormWindowState.Minimized;
        }
       
        /*
         * boton para cambiar entre las ventana de inicio y configuracion
         */
        private void pictureBox3_Click(object sender, EventArgs e)
        {
            if (tabControl1.SelectedTab == ventanaInicio)
            {
                tabControl1.SelectedTab = VentanaConfiguracion;
                toolTip1.SetToolTip(this.pictureBox3, "Mostrar inicio");
            }
            else
            {
                tabControl1.SelectedTab = ventanaInicio;
                toolTip1.SetToolTip(this.pictureBox3, "Mostrar configuracion");
            }
        }
        int mousey, mousex;
        private void panel1_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button != MouseButtons.Left)
            {
                mousex = e.X;
                mousey = e.Y;
            }
            else
            {
                Left = Left + (e.X - mousex);
                Top = Top + (e.Y - mousey);
            }
        }
    }
}
