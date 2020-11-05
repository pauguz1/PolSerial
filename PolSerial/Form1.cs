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
                label12.ForeColor = Color.White;
                linkLabel1.LinkColor = Color.White;
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
                label12.ForeColor = Color.Black;
                linkLabel1.LinkColor = Color.Black;
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
                validarConexionSerial();
                await Controlador1.enviarDato("mostrarNombreBoton");
                MessageBox.Show("Puerto Seleciconado correctamente");
            }
            else
            {
                MessageBox.Show("Error al seleccionar puerto");
            }
        }
        void validarConexionSerial()
        {
            if (Controlador1.serial.IsOpen)
            {
                zeroitSwitchThematic2.Checked = true;//Mostramos el switch com Activado 
                zeroitSwitchThematic2.Enabled = true;
            }
            else
            {
                zeroitSwitchThematic2.Checked = false;//Mostramos el switch com Activado 
                zeroitSwitchThematic2.Enabled = false;
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
            if (Controlador1.serial.IsOpen)
            {
                if (await Controlador1.enviarDato("btn1click"))
                {

                }
                else
                {
                    MessageBox.Show("el mensaje no fue enviado");
                }
            }
            else
            {
                MessageBox.Show("Debes seleccionar un puerto");
                validarConexionSerial();
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
            if (Controlador1.serial.IsOpen)
            {
                if (await Controlador1.enviarDato("btn2click"))
                {

                }
                else
                {
                    MessageBox.Show("el mensaje no fue enviado");
                }
            }
            else
            {
                validarConexionSerial();
                MessageBox.Show("Debes seleccionar un puerto");
            }
            
        }

        private async void button3_Click(object sender, EventArgs e)
        {
            if (Controlador1.serial.IsOpen)
            {
                if (await Controlador1.enviarDato("btn3click"))
                {

                }
                else
                {
                    MessageBox.Show("el mensaje no fue enviado");
                }
            }
            else
            {
                validarConexionSerial();
                MessageBox.Show("Debes seleccionar un puerto");
            }
            
        }
        /*
         * 
         * Este metodo envia un dato por el puerto serial que indica que el boton fue presionado
         */
        private async void button4_Click(object sender, EventArgs e)
        {
            if (Controlador1.serial.IsOpen)
            {
                if (await Controlador1.enviarDato("btn4click"))
                {

                }
                else
                {
                    MessageBox.Show("el mensaje no fue enviado");
                }
            }
            else
            {
                validarConexionSerial();
                MessageBox.Show("Debes seleccionar un puerto");
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
                else if (datos.Contains("led1On"))//------------------------------------ Encendido del led
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
                }//---------------------------------------------------------------------------   Apagado de LED
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
                }else if (datos.Contains("btn1txt"))//------------------------------------------ Cambio del texto en los botones
                {
                    string b = datos.Replace("btn1txt", "");
                    button1.Text = b;
                }
                else if (datos.Contains("btn2txt"))
                {
                    string b = datos.Replace("btn2txt", "");
                    button2.Text = b;
                }
                else if (datos.Contains("btn3txt"))
                {
                    string b = datos.Replace("btn3txt", "");
                    button3.Text = b;
                }
                else if (datos.Contains("btn4txt"))
                {
                    string b = datos.Replace("btn4txt", "");
                    button4.Text = b;
                }
                else if (datos.Contains("etiqueta1"))
                {
                    string b = datos.Replace("etiqueta1", "");
                    label1.Text = b;
                }
                else if (datos.Contains("etiqueta2"))
                {
                    string b = datos.Replace("etiqueta2", "");
                    label2.Text = b;
                }
                else if (datos.Contains("etiqueta3"))
                {
                    string b = datos.Replace("etiqueta3", "");
                    label3.Text = b;
                }
                else if (datos.Contains("etiqueta4"))
                {
                    string b = datos.Replace("etiqueta4", "");
                    label4.Text = b;
                }
                else if (datos.Contains("mostrarNombreBoton"))
                {
                }
                else if (datos != "")
                {
                    // MessageBox.Show(datos);
                    /*string[] datosSeparados;
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

                    }*/

                }
            }
            else
            {
                zeroitSwitchThematic2.Checked = false;
                zeroitSwitchThematic2.Enabled = false;
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
         *Este metodo es para cambiar el estado de la conexion
         */
        private void zeroitSwitchThematic2_Click(object sender, EventArgs e)
        {
            if (Controlador1.serial.IsOpen)
            {
                Controlador1.serial.Close();
                zeroitSwitchThematic2.Enabled = false;
                //MessageBox.Show("Puerto Desconectado");
            }
            {
                zeroitSwitchThematic2.Checked = false;
                zeroitSwitchThematic2.Enabled = false;
            }
        }
        /*
         * Este es el link para contactar al creador 
         */
        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start("https://www.instagram.com/paul_s4ntana/");
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
