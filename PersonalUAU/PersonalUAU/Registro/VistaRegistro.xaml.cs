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
using DPUruNet;

namespace PersonalUAU.Registro
{
    /// <summary>
    /// Lógica de interacción para Vista_Registro.xaml
    /// </summary>
    public partial class VistaRegistro : Window
    {
        Metodos objMethods;
        Reader objReader;
        public string xml;
        public bool operacion=false;

        public VistaRegistro(Reader objReader)
        {
            InitializeComponent();
            this.objReader = objReader;
            objMethods = new Metodos();
            UserEnrollment();
        }

        public void UserEnrollment()
        {
            objMethods.StartEnrollment(objReader, this);
        }

        public void EndEnrollment(string serializacion,bool operacion)
        {
            xml = serializacion;
            this.operacion = operacion;
            Finished();
        }

        public void Finished()
        {
            this.Dispatcher.BeginInvoke(new Action(delegate()
            {
                DialogResult = true;
            }));
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (operacion == false)
            {
                objMethods.StopEnrollment();
                DialogResult = false;
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            objMethods.StopEnrollment();
            DialogResult = false;
        }

        private void txtSalida_TextChanged(object sender, TextChangedEventArgs e)
        {
            txtSalida.ScrollToEnd();
        }
    }
}
