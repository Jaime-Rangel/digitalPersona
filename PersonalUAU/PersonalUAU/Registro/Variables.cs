using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using DPUruNet;
using System.Windows.Forms;

namespace PersonalUAU.Registro
{
    class Variables
    {
        public bool reset = false;
        public int count = 0;
        public Reader currentReader;
        public string serializacion;
        public bool terminated = false;
    }
}
