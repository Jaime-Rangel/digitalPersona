using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace PersonalUAU.Clases
{
    public class BuscarPersona
    {
        public bool endThread { get; set; }
        public Persona persona { get; set; }

        private static volatile BuscarPersona buscarPersona = null;

        public static BuscarPersona getInstance
        {
            [MethodImpl(MethodImplOptions.Synchronized)]
            get
            {
                if (buscarPersona == null)
                {
                    buscarPersona = new BuscarPersona();
                }

                return buscarPersona;
            }
        }

    }
}
