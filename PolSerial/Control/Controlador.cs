using System;
using System.Collections.Generic;
using System.IO.Ports;// libreria para el puerto Serial
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PolSerial.Control
{
    class Controlador
    {
       public SerialPort serial = new SerialPort();
       //public string datosRecividos;
        public Controlador()
        {
            serial.DataReceived += Serial_DataReceived;// evento para detectar cuando un dato es recivido
            serial.WriteTimeout = 1;
            serial.ReadTimeout = 1;
        }

        private void Serial_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
           //datosRecividos= await obtenerDatosRecividos();
            //Thread.Sleep(5000);
        }
        
        public async Task<List<string>> GetListaPuertos()
        {
            // Nota se debe encerrar todo en el return await Task.Run(() =>{  /* codigo  */
            return await Task.Run(() =>
            {
                List<string> lista = new List<string>();

                try
                {
                    foreach (string puerto in System.IO.Ports.SerialPort.GetPortNames())
                    {
                        lista.Add(puerto);
                    }
                }
                catch
                {
                   
                }
                return lista;
            });
        }
        public async Task<bool> SetPuertoSeleccionado(string nombrePuerto)
        {
            return await Task.Run(() =>
            {
                bool bandera = true;

                try
                {
                    serial.Close();
                    serial.PortName = nombrePuerto;
                    serial.Open();
                }
                catch
                {
                    bandera = false;
                }
                return bandera;
            });
        }
        public async Task<bool> SetBaudiosSeleccionado(int VelocidadBaudios)
        {
            return await Task.Run(() =>
            {
                bool bandera = true;

                try
                {
                    serial.BaudRate = VelocidadBaudios;
                }
                catch
                {
                    bandera = false;
                }
                return bandera;
            });
        }
        public async Task<bool> enviarDato(String dato)
        {
            // Nota se debe encerrar todo en el return await Task.Run(() =>{  /* codigo  */
            return await Task.Run(() =>
            {
                bool bandera = true;

                try
                {
                    if (serial.IsOpen)
                    {
                        serial.DiscardInBuffer();
                        serial.WriteLine(dato);
                    }
                    else
                    {
                        MessageBox.Show("Debes seleccionar un Puerto");
                    }
                }
                catch (Exception)
                {
                    bandera = false;
                    MessageBox.Show("el mensaje no fue enviado");
                }
                return bandera;
            });
        }
        public async Task<string> obtenerDatosRecividos()
        {
            // Nota se debe encerrar todo en el return await Task.Run(() =>{  /* codigo  */
            return await Task.Run(() =>
            {

                string datosRecividos1="";
                try
                {
                    if (serial.IsOpen)
                    {
                        datosRecividos1 = serial.ReadLine();
                    }
                    
                }
                catch
                {

                }
                return datosRecividos1;
            });
        }
    }
}
