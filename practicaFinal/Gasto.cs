using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PracticaFinal
{
    public enum OtrosGastos { Estacionamiento, Multa, Peajes, Impuestos };
    public enum Servicios
    {
        Aire_Acondicionado, Autolavado, Batería, Cambio_de_aceite, Cinturones, Filtro_de_aceite,
        Filtro_de_combustible, Inspección, Luces, Líquido_de_frenos, Mano_de_obra, Neumáticos_Alineación,
        Neumáticos_Presión, Nuevos_Neumáticos, Pastillas_de_freno, Rotar_neumáticos, Sistema_de_suspensión
    };
    public enum Gastos { Repostaje, OtroGasto, Servicio }
    public class Gasto
    {
        public DateTime fecha { get; set; }
        public int kilometraje { get; set; }
        public float litros { get; set; }
        public float coste { get; set; }
        public Vehiculo vehiculo { get; set; }
        public Gastos tipoGasto { get; set; }
        public String detalles { get; set; }

        public Gasto()
        {

        }
        public Gasto(Vehiculo vehiculo, Gastos tipoGasto, DateTime fecha, int kilometraje, float litros, float coste)
        {
            this.vehiculo = vehiculo;
            this.tipoGasto = tipoGasto;
            this.fecha = fecha;
            this.kilometraje = kilometraje;
            this.litros = litros;
            this.coste = coste;
        }
        public Gasto(Vehiculo vehiculo, Gastos tipoGasto, DateTime fecha, String detalles, float coste)
        {
            this.vehiculo = vehiculo;
            this.fecha = fecha;
            this.tipoGasto = tipoGasto;
            this.detalles = detalles;
            this.coste = coste;
        }
        public Array GetTiposOtrosGastos { get { return Enum.GetValues(typeof(OtrosGastos)); } }
        public Array GetTiposServicios { get { return Enum.GetValues(typeof(Servicios)); } }

    }
}
