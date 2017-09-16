using DPUruNet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Forms;

namespace PersonalUAU.CapturaHuella
{
    public class Variables
    {
        public bool huellaCapturada = false;
        public bool backEnabled = false;
        public bool reset = false;
        public bool threadHandle_lock = false;
        public Reader currentReader;
        public bool streamingOn;
        public PictureBox picFingerPrint;
        public Bitmap imagenHuella { get; set; }
        public string patchCapturaHuella { get; set; }
    }
}
