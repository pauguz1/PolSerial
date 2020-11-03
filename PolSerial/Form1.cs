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
        Control.Controlador Controlador1 = new Control.Controlador();
        public Form1()
        {
            InitializeComponent();
            obtenerPuertos();
            
        }
        /*
         * Este metodo Muestra la lista de Puertos COM disponibles
         * y Muestra la velocidad Serial que se quedo configurada en la ultima Sesion 
         */
        async void obtenerPuertos()
        {
            comboBox1.Items.Clear();
            foreach (string puerto in await Controlador1.GetListaPuertos())
            {
                comboBox1.Items.Add(puerto);
            }
            if (Properties.Settings.Default["velocidad"] != null)
            {
                comboBox2.Text = Properties.Settings.Default["velocidad"].ToString();
                if(!await Controlador1.SetBaudiosSeleccionado(Convert.ToInt32(Properties.Settings.Default["velocidad"].ToString())))
                {
                    MessageBox.Show("Error al seleccionar Velocidad");
                }
            }
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

        private async void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(await Controlador1.SetPuertoSeleccionado(comboBox1.Text))
            {
                MessageBox.Show("Puerto Seleciconado correctamente");
            }
            else
            {
                MessageBox.Show("Error al seleccionar puerto");
            }
        }

        private async void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(await Controlador1.SetBaudiosSeleccionado(Convert.ToInt32(comboBox2.Text)))
            {
                Properties.Settings.Default["velocidad"] = comboBox2.Text;
                Properties.Settings.Default.Save();
            }
            else
            {
                MessageBox.Show("Error al seleccionar Velocidad");
            }
        }

        private async void button1_Click(object sender, EventArgs e)
        {
            if( await Controlador1.enviarDato("ha"))
            {

            }
        }

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
