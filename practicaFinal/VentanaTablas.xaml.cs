using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
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

namespace PracticaFinal
{
    /// <summary>
    /// Lógica de interacción para ventanaTablas.xaml
    /// </summary>

    public partial class VentanaTablas : Window
    {
        ObservableCollection<Vehiculo> listaVehiculos;
        ObservableCollection<Gasto> listaGastos;
        public event EventHandler<SeleccionandoEventArgs> Seleccionando;
        public event EventHandler<BorrandoEventArgs> Borrando;
        VentanaAnadirVehiculo ventanaAnadirVehiculo;
        int indiceAModificar;
        private void OnSeleccionando(SeleccionandoEventArgs e)
        {
            Seleccionando?.Invoke(this, e);
        }
        private void OnBorrando(BorrandoEventArgs e)
        {
            Borrando?.Invoke(this, e);
        }
        public VentanaTablas(ObservableCollection<Vehiculo> listaVehiculos)
        {
            InitializeComponent();
            this.listaVehiculos = listaVehiculos;
            tablaVehiculos.ItemsSource = listaVehiculos;
        }

        private void tablaVehiculos_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
         
            if (e.AddedItems.Count > 0 && tablaVehiculos.SelectedItem != e.AddedItems[0])
                tablaVehiculos.SelectedItem = e.AddedItems[0];

            if (tablaVehiculos.SelectedItem != null)
            {
                this.listaGastos = ((Vehiculo)tablaVehiculos.SelectedItem).ListaGastos;
                tablaGastos.ItemsSource = ((Vehiculo)tablaVehiculos.SelectedItem).ListaGastos;
                OnSeleccionando(new SeleccionandoEventArgs((Vehiculo)tablaVehiculos.SelectedItem));
            }
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            e.Cancel = true;
            Hide();
        }

        private void botonBorrar_Click(object sender, RoutedEventArgs e)
        {
            if (tablaVehiculos.SelectedItem != null)
            {
                listaVehiculos.RemoveAt(tablaVehiculos.SelectedIndex);
                double centroAnt = -60;
                foreach(Vehiculo v in listaVehiculos)
                {
                    v.centro = centroAnt + 180;
                    centroAnt = v.centro;
                }
                foreach (Vehiculo v in listaVehiculos)
                    OnBorrando(new BorrandoEventArgs(v));
            }
        }

        private void modificar(object sender, ModificandoEventArgs e)
        {
            Vehiculo vehiculo = e.vehiculo;
            vehiculo.ListaGastos = listaVehiculos[indiceAModificar].ListaGastos;
            listaVehiculos.RemoveAt(indiceAModificar);
            listaVehiculos.Insert(indiceAModificar, vehiculo);
            foreach (Vehiculo v in listaVehiculos)
                OnBorrando(new BorrandoEventArgs(v));
        }

        private void botonModificar_Click(object sender, RoutedEventArgs e)
        {
            if (tablaVehiculos.SelectedItem != null)
            {
                indiceAModificar = tablaVehiculos.SelectedIndex;
                Vehiculo v = (Vehiculo)tablaVehiculos.SelectedItem;
                ventanaAnadirVehiculo = new VentanaAnadirVehiculo(listaVehiculos, v.tipoVehiculo.ToString(), v.marca, v.modelo, v.matricula, v.kilometraje, v.tipoCombustible.ToString());
                ventanaAnadirVehiculo.Modificando += modificar;
                ventanaAnadirVehiculo.Show();
            }
        }
    }
}
