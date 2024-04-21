using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Documents;
using System.ComponentModel;

namespace PracticaFinal
{
    public enum Vehiculos { coche, motocicleta, autobús, camión };
    public enum Combustibles { diesel, gasolina };
    public class Vehiculo : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        float mediaConsumo_, mediaCoste_, gastosOtrosTotales_, gastosServiciosTotales_;
        int kilometraje_;
        public Vehiculo(Vehiculos tipoVehiculo, String marca, String modelo, String matricula, Combustibles tipoCombustible, int kilometraje)
        {
            this.tipoVehiculo = tipoVehiculo;
            this.marca = marca;
            this.modelo = modelo;
            this.matricula = matricula;
            this.tipoCombustible = tipoCombustible;
            this.kilometraje = kilometraje;
            this.kilometrajeIni = kilometraje;
            this.mediaConsumo_ = 0;
            this.mediaCoste_ = 0;
            this.gastosOtrosTotales_ = 0;
            this.gastosServiciosTotales_ = 0;
            this.centro = 0;
            ListaGastos = new ObservableCollection<Gasto>();
        }
        public Vehiculos tipoVehiculo { get; set; }
        public String marca { get; set; }
        public String modelo { get; set; }
        public String matricula { get; set; }
        public Combustibles tipoCombustible { get; set; }
        public int kilometrajeIni { get; set; }
        public int kilometraje
        {
            get { return kilometraje_; }
            set { kilometraje_ = value; OnKilometrajeChanged("kilometraje"); }
        }
        public ObservableCollection<Gasto> ListaGastos { get; set; }
        public Vehiculo()
        {

        }

        public double centro { get; set; }
        public float mediaConsumo
        {
            get { return mediaConsumo_; }
            set { mediaConsumo_ = value; OnMediaConsumoChanged("mediaConsumo"); }
        }

        public float mediaCoste
        {
            get { return mediaCoste_; }
            set { mediaCoste_ = value; OnMediaCosteChanged("mediaCoste"); }
        }

        public float gastosOtrosTotales
        {
            get { return gastosOtrosTotales_; }
            set { gastosOtrosTotales_ = value; OnGastosOtrosTotalesChanged("gastosOtrosTotales"); }
        }

        public float gastosServiciosTotales
        {
            get { return gastosServiciosTotales_; }
            set { gastosServiciosTotales_ = value; OnGastosServiciosTotalesChanged("gastosServiciosTotales"); }
        }

        public Array GetTiposVehiculos { get { return Enum.GetValues(typeof(Vehiculos)); } }
        public Array GetTiposCombustibles { get { return Enum.GetValues(typeof(Combustibles)); } }
        public Collection<String> GetMarcasVehiculos
        {
            get
            {
                Collection<String> lineas = new Collection<String>();
                foreach (string linea in System.IO.File.ReadAllLines("marcas.txt"))
                {
                    lineas.Add(linea);
                }
                return lineas;
            }
        }

        protected void OnMediaConsumoChanged(string mediaConsumo)
        {
            if (PropertyChanged != null)
                PropertyChanged(this,
               new PropertyChangedEventArgs(mediaConsumo));
        }

        protected void OnMediaCosteChanged(string mediaCoste)
        {
            if (PropertyChanged != null)
                PropertyChanged(this,
               new PropertyChangedEventArgs(mediaCoste));
        }

        protected void OnKilometrajeChanged(string kilometraje)
        {
            if (PropertyChanged != null)
                PropertyChanged(this,
               new PropertyChangedEventArgs(kilometraje));
        }

        protected void OnGastosOtrosTotalesChanged(string gastosOtrosTotales)
        {
            if (PropertyChanged != null)
                PropertyChanged(this,
               new PropertyChangedEventArgs(gastosOtrosTotales));
        }

        protected void OnGastosServiciosTotalesChanged(string gastosServiciosTotales)
        {
            if (PropertyChanged != null)
                PropertyChanged(this,
               new PropertyChangedEventArgs(gastosServiciosTotales));
        }

    }
}
