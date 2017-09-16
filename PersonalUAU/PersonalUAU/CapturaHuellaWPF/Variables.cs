using DPUruNet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace PersonalUAU.CapturaHuellaWPF
{
    public class Variables
    {
        public bool Huella_Capturada = false;

        public bool backEnabled = false;

        public bool reset = false;

        public bool threadHandle_lock = false;

        public Reader currentReader;

        public bool streamingOn;

        public System.Drawing.Bitmap saved_picture { get; set; }

        public Image pbFingerprint;
        public string patchCapturaHuella { get; set; }

    }
}
