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

namespace PersonalUAU.Identificacion
{
    /// <summary>
    /// Lógica de interacción para VistaIdentificacion.xaml
    /// </summary>
    public partial class VistaIdentificacion : Window
    {
        Reader objReader;
        Metodos objReaderMethods;
        List<Clases.Persona> listPersons;
        public PersonalUAU.Clases.Persona objPersona;
        public bool exitIdentify;

        public VistaIdentificacion(Reader objReader, List<Clases.Persona> listPersons)
        {
            InitializeComponent();
            this.objReader = objReader;
            this.listPersons = listPersons;
            initializeObjects();
            StartIdenfityThread();
        }

        public void initializeObjects()
        {
            objReaderMethods = new Metodos(objReader,listPersons,this);
        }

        public void StartIdenfityThread()
        {
            objReaderMethods.StartIdentify();
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            objReaderMethods.StopIdentify();
        }
    }
}
