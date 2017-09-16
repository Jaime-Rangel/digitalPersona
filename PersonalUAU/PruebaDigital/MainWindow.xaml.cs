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
using System.Windows.Navigation;
using System.Windows.Shapes;
using DPUruNet;

namespace PruebaDigital
{
    /// <summary>
    /// Lógica de interacción para MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            Mostrar_VentanaPrincipal();
        }

        public void Mostrar_VentanaPrincipal()
        {
            Reader Dispositivo;

            PersonalUAU.PersonalDigital Operaciones = new PersonalUAU.PersonalDigital();
            //Muestra Ventana Para seleccion de lector
            Dispositivo = Operaciones.ShowWindowSelectDevice();
            //Una vez seleccionado se empieza a hacer el escaneo de huella
            Operaciones.Capturefingerprint(Dispositivo);
        }
    }
}
