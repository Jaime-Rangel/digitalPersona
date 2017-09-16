using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DPUruNet;
using System.Threading;

namespace Prueba
{
    public partial class MenuPrincipal : Form
    {
        PersonalUAU.DigitalPersona objMethods;
        List<PersonalUAU.Clases.Persona> listPerson;
        Reader objReader;

        string xml;
        string fileDelete;
        
        public MenuPrincipal()
        {
            InitializeComponent();
            objMethods = new PersonalUAU.DigitalPersona();
            InitializeObjects();
        }

        private void InitializeObjects()
        {
            listPerson = new List<PersonalUAU.Clases.Persona>();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            //For one device detected by the computer
            objReader = objMethods.GetDevice();
        }

        private void Actualizar_Lista_Dispositivos()
        {
            objMethods.GetAllDevices(this.cboDispositivo);
        }

        private void cboDispositivo_SelectedIndexChanged(object sender, EventArgs e)
        {
            objReader = objMethods.SelectDevice(this.cboDispositivo);
        }

        private void btnCapturar_Click(object sender, EventArgs e)
        {
            objMethods.CaptureFingerprint(objReader,this.picHuella);
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            objMethods.StopCaptureFingerprint();
            objMethods.ClearFingerprint_XML();
            Reanudar_Vista();
        }

        private void Reanudar_Vista()
        {
            picHuella.Image = null;
            picHuella.Refresh();
        }


        private void btnVentana_Click(object sender, EventArgs e)
        {
            objMethods.ShowWindowEnrollment(objReader);
            xml = objMethods.GetFingerprint_XML();

            PersonalUAU.Clases.Persona objPersona = new PersonalUAU.Clases.Persona();
            objPersona.huella = xml;
            listPerson.Add(objPersona);

        }

        private void btnAlmacenar_Click(object sender, EventArgs e)
        {
            fileDelete = objMethods.SavePictureFingerprint("1","OP-1");
            MessageBox.Show(fileDelete);
        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            try
            {
                objMethods.StopCaptureFingerprint();
            }
            catch(Exception Ex)
            {
                Console.WriteLine(Ex);
            }
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            objMethods.DeletePictureFingerprint(fileDelete);
        }

        private void btnIdentify_Click(object sender, EventArgs e)
        {
            objMethods.StartIdentify(objReader, listPerson);
        }

    }
}
