using DPUruNet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PersonalUAU
{
    public class DigitalPersona
    {
        //space for attributes
        SelectorHuella.Metodos objSelector;
        CapturaHuella.Metodos objCapturaHuella;
        CapturaHuellaWPF.Metodos objCapturaHuellaWPF;

        string xml = null;

        public DigitalPersona()
        {
            objSelector = new SelectorHuella.Metodos();
        }

        /// <summary>
        /// Get all devices from a control type parameter Combobox. </summary>
        /// <remarks>
        /// Method should be called after unplug and plug some devices
        ///</remarks>
        public void GetAllDevices(ComboBox cboLista)
        {
            objSelector.IndexDeviceList(cboLista);
        }

        /// <summary>
        /// Get the first device detected by the computer. </summary>
        /// <returns>
        /// Returns a Reader Type object </returns>
        public Reader GetDevice()
        {
            return objSelector.IndexDevice();
        }

        /// <summary>
        /// Returns the data of the selected reader in a combobox list and initialize the object. </summary>
        /// <returns>
        /// Returns a Reader Type object </returns>
        public Reader SelectDevice(ComboBox cboLista)
        {
            Reader objReader;
            objReader = objSelector.AssignDevice(cboLista);

            return objReader;
        }

        /// <summary>
        /// Set the image of a fingerprint captured by the device reader and is showed by a PictureBox. </summary>
        /// <returns>
        /// Parameter type picturebox </returns>
        public void CaptureFingerprint(Reader objReader,PictureBox imgFingerPrint)
        {
            objCapturaHuella = new CapturaHuella.Metodos(objReader,imgFingerPrint);
            objCapturaHuella.StartCaptures();
        }

        /// <summary>
        /// Set the image of a fingerprint captured by the device reader and is showed by a Image WPF. </summary>
        /// <returns>
        /// Parameter type picturebox </returns>
        public void CaptureFingerprintWPF(Reader objReader,System.Windows.Controls.Image imgFingerPrint)
        {
            objCapturaHuellaWPF = new CapturaHuellaWPF.Metodos(objReader,imgFingerPrint);
            objCapturaHuellaWPF.StartCaptures();
        }

        /// <summary>
        /// Stop the capture of a image of a fingerprint by the device reader and is showed by a PictureBox. </summary>
        /// <returns>
        /// Parameter type picturebox </returns>
        public void StopCaptureFingerprint()
        {
            if (objCapturaHuella != null)
            {
                objCapturaHuella.StopCaptures();
                objCapturaHuella = null;
            }
        }

        /// <summary>
        /// Stop the capture of a image of a fingerprint by the device reader and is showed by a Image WPF. </summary>
        /// <returns>
        /// Parameter type picturebox </returns>
        public void StopCaptureFingerprintWPF()
        {
            if (objCapturaHuellaWPF != null)
            {
                objCapturaHuellaWPF.StopCaptures();
                objCapturaHuellaWPF = null;
            }
        }

        /// <summary>
        /// Starts a thread and show a emergent window of enrollment users and returns a xml generate by the library . </summary>
        /// <returns>
        /// xml string </returns>
        public void ShowWindowEnrollment(Reader objReader)
        {
            Registro.VistaRegistro Registrar = new Registro.VistaRegistro(objReader);

            bool? resultado = Registrar.ShowDialog();

            switch (resultado)
            {
                case true:
                    this.xml = Registrar.xml;
                    break;

                case false:
                    MessageBox.Show("No se hizo ningún registro de huella.",
                    "Aviso",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Exclamation,
                    MessageBoxDefaultButton.Button1);
                    break;

                default:
                    break;
            }
        }
        /// <summary>
        /// Store picture fingerprint in system</summary>
        public string SavePictureFingerprint(string type,string reference)
        {
            return objCapturaHuella.SaveFingerprintImage(type,reference);
        }

        /// <summary>
        /// Store picture fingerprint in system WPF Method</summary>
        public string SavePictureFingerprintWPF(string type, string reference)
        {
            return objCapturaHuellaWPF.SaveFingerprintImage(type, reference);
        }

        /// <summary>
        /// Erase ImageFile of fingerprint from System </summary>
        public void DeletePictureFingerprint(string filepath)
        {
            objCapturaHuella.DeleteImageDirectory(filepath);          
        }

        /// <summary>
        /// Erase ImageFile of fingerprint from System WPF method</summary>
        public void DeletePictureFingerprintWPF(string filepath)
        {
            objCapturaHuellaWPF.DeleteImageDirectory(filepath);
        }

        /// <summary>
        /// return the path of the fingerprint</summary>
        public string GetPathFingerPrintImage()
        {
            return objCapturaHuella.ReturnFingerprintImageRoute();
        }

        /// <summary>
        /// return the path of the fingerprint WPF method</summary>
        public string GetPathFingerPrintImageWPF()
        {
            return objCapturaHuellaWPF.ReturnFingerprintImageRoute();
        }

        /// <summary>
        /// Return Only the xml previusly captured by Enrollment Method. </summary>
        /// <returns>
        /// xml string </returns>
        public string GetFingerprint_XML()
        {
            return xml;
        }

        /// <summary>
        /// Erase all string of xml Enrollment captured previusly by Enrollment Method </summary>
        public void ClearFingerprint_XML()
        {
            xml = null;
        }

        /// <summary>
        /// StartIdentifyThread. </summary>
        /// <remarks>
        /// return a class of person
        ///</remarks>
        ///
        public void StartIdentify(Reader objReader, List<Clases.Persona> listPersons)
        {
            Identificacion.VistaIdentificacion Identificar = new Identificacion.VistaIdentificacion(objReader, listPersons);
            Identificar.Show();
        }

    }
}
