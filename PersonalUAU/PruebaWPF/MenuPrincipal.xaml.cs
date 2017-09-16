using DPUruNet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace PruebaWPF
{
    /// <summary>
    /// Lógica de interacción para MenuPrincipal.xaml
    /// </summary>
    public partial class MenuPrincipal : Window
    {
        PersonalUAU.DigitalPersona objMethods;
        Reader objReader;
        string xml;
        string imgroute;

        public MenuPrincipal()
        {
            InitializeComponent();
            objMethods = new PersonalUAU.DigitalPersona();

            //For one device detected by the computer
            objReader = objMethods.GetDevice();
        }

        private void IniciarCapturas_Click(object sender, RoutedEventArgs e)
        {
            objMethods.CaptureFingerprintWPF(objReader,picHuella);
        }

        private void DetenerCapturas_Click(object sender, RoutedEventArgs e)
        {
            objMethods.StopCaptureFingerprintWPF();
            objMethods.ClearFingerprint_XML();
            ReloadImageFingerPrint();
        }
        
        private void ReloadImageFingerPrint()
        {
            picHuella.Source = null;
        }

        private void RegistrarHuella_Click(object sender, RoutedEventArgs e)
        {
            objMethods.ShowWindowEnrollment(objReader);
            xml = objMethods.GetFingerprint_XML();
        }

        private void AlmacenarHuella_Click(object sender, RoutedEventArgs e)
        {
           imgroute = objMethods.SavePictureFingerprintWPF("1","OP-1");
        }

        private void EliminarHuella_Click(object sender, RoutedEventArgs e)
        {
            objMethods.DeletePictureFingerprintWPF(objMethods.GetPathFingerPrintImageWPF() + imgroute);
        }
    }
}
