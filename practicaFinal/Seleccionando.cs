using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PracticaFinal
{
    public class SeleccionandoEventArgs : EventArgs
    {
        public Vehiculo vehiculo { get; set; }
        public SeleccionandoEventArgs(Vehiculo vehiculo)
        {
            this.vehiculo = vehiculo;
        }
    }

    public class BorrandoEventArgs : EventArgs
    {
        public Vehiculo vehiculo { get; set; }
        public BorrandoEventArgs(Vehiculo vehiculo)
        {
            this.vehiculo = vehiculo;
        }
    }

    public class ModificandoEventArgs : EventArgs
    {
        public Vehiculo vehiculo { get; set; }
        public ModificandoEventArgs(Vehiculo vehiculo)
        {
            this.vehiculo = vehiculo;
        }
    }

}
