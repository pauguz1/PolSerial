using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
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
            if (Properties.Settings.Default["modoOscuro"].ToString() == "true")
            {
                modoOscuro(true);
                zeroitSwitchThematic1.Checked = true;
            }
            else
            {
                modoOscuro(false);
                zeroitSwitchThematic1.Checked = false;
            }
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
        void modoOscuro(bool modo)
        {
            if (modo)
            {
                ventanaInicio.BackColor = Color.FromArgb(27, 33, 39);
                VentanaConfiguracion.BackColor= Color.FromArgb(27, 33, 39);
                label1.ForeColor = Color.White;
                label2.ForeColor = Color.White;
                label3.ForeColor = Color.White;
                label4.ForeColor = Color.White;
                label5.ForeColor = Color.White;
                label6.ForeColor = Color.White;
                label7.ForeColor = Color.White;
                label8.ForeColor = Color.White;
                label9.ForeColor = Color.White;
                label10.ForeColor = Color.White;
                label11.ForeColor = Color.White;
            }
            else
            {
                ventanaInicio.BackColor = Color.Transparent;
                VentanaConfiguracion.BackColor = Color.Transparent;
                label1.ForeColor = Color.Black;
                label2.ForeColor = Color.Black;
                label3.ForeColor = Color.Black;
                label4.ForeColor = Color.Black;
                label5.ForeColor = Color.Black;
                label6.ForeColor = Color.Black;
                label7.ForeColor = Color.Black;
                label8.ForeColor = Color.Black;
                label9.ForeColor = Color.Black;
                label10.ForeColor = Color.Black;
                label11.ForeColor = Color.Black;
            }
        }
        /*
         * este metodo es para salir de la aplicacion
         */
        private void pictureBox1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
        /*
         * este metodo es para minimizar la ventana
         */
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
        /*
         * este metodo es para cuando seleccionamos un puerto serial
         */
        private async void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(await Controlador1.SetPuertoSeleccionado(comboBox1.Text))
            {
                tabControl1.SelectedTab = ventanaInicio;
                MessageBox.Show("Puerto Seleciconado correctamente");
            }
            else
            {
                MessageBox.Show("Error al seleccionar puerto");
            }
        }
        /*
         * este metodo es para cuando seleccionamos la velocidad de comunicacion del puerto serial
         */
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
        /*
         * Este metodo envia el dato de que el boton 1 fue presionado
         */
        private async void button1_Click(object sender, EventArgs e)
        {
            if (await Controlador1.enviarDato("btn1click"))
            {

            }
           /* if (!checkBox1.Enabled)
            {
                checkBox1.Enabled = true;
                //checkBox1.CheckState = CheckState.Checked;// asi es como ponemos un checkbox en checdo
            }
            else
            {
                checkBox1.Enabled = false;
            }*/
        }

        private async void button2_Click(object sender, EventArgs e)
        {
            if (await Controlador1.enviarDato("btn2click"))
            {

            }
        }

        private async void button3_Click(object sender, EventArgs e)
        {
            if (await Controlador1.enviarDato("btn3click"))
            {

            }
        }
        /*
         * 
         * Este metodo envia un dato por el puerto serial que indica que el boton fue presionado
         */
        private async void button4_Click(object sender, EventArgs e)
        {
            if (await Controlador1.enviarDato("btn4click"))
            {

            }
        }

        private async  void timer1_Tick(object sender, EventArgs e)
        {
            if (Controlador1.serial.IsOpen)
            {
                //string datos= Controlador1.datosRecividos;
                string datos = await Controlador1.obtenerDatosRecividos();
                //button1.Text = datos;
                if (datos == null)
                {

                }
                else if (datos.Contains("led1On"))
                {
                    pictureBox5.Image = PolSerial.Properties.Resources.Icon_CircleGreen35px;
                }
                else if (datos.Contains("led2On"))
                {
                    pictureBox6.Image = PolSerial.Properties.Resources.Icon_CircleGreen35px;
                }
                else if (datos.Contains("led3On"))
                {
                    pictureBox7.Image = PolSerial.Properties.Resources.Icon_CircleGreen35px;
                }
                else if (datos.Contains("led4On"))
                {
                    pictureBox8.Image = PolSerial.Properties.Resources.Icon_CircleGreen35px;
                }//----------------------- Apagado de LED
                else if (datos.Contains("led1Off"))
                {
                    pictureBox5.Image = PolSerial.Properties.Resources.Icon_CircleWhite35px;
                }
                else if (datos.Contains("led2Off"))
                {
                    pictureBox6.Image = PolSerial.Properties.Resources.Icon_CircleWhite35px;
                }
                else if (datos.Contains("led3Off"))
                {
                    pictureBox7.Image = PolSerial.Properties.Resources.Icon_CircleWhite35px;
                }
                else if (datos.Contains("led4Off"))
                {
                    pictureBox8.Image = PolSerial.Properties.Resources.Icon_CircleWhite35px;
                }
                else if (datos != "")
                {
                    // MessageBox.Show(datos);
                    string[] datosSeparados;
                    try
                    {
                        if (Controlador1.serial.IsOpen)
                        {
                            datosSeparados = datos.Split(',');
                            label1.Text = datosSeparados[0];
                            label2.Text = datosSeparados[1];
                            label3.Text = datosSeparados[2];
                            label4.Text = datosSeparados[3];
                        }
                    }
                    catch
                    {

                    }

                }
            }
        }
        /*
         * switch para manejar el modo oscuro
         */
        private void zeroitSwitchThematic1_Click(object sender, EventArgs e)
        {
            if (zeroitSwitchThematic1.Checked)// si esta activada 
            {
                modoOscuro(true);
                Properties.Settings.Default["modoOscuro"] = "true";
                Properties.Settings.Default.Save();
            }
            else
            {
                modoOscuro(false);
                Properties.Settings.Default["modoOscuro"] = "false";
                Properties.Settings.Default.Save();
            }
        }
        /*
         * actualizar los puertos en el combobox
         */
        private void pictureBox4_Click(object sender, EventArgs e)
        {
            obtenerPuertos();
        }

        /*
         * este metodo es para cuando el usuario quiere mover la ventana de posicion 
         */
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
