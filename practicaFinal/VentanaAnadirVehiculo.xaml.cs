using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
    /// Lógica de interacción para VentanaAnadirVehiculo.xaml
    /// </summary>
    public partial class VentanaAnadirVehiculo : Window
    {
        ObservableCollection<Vehiculo> listaVehiculos;
        public event EventHandler<ModificandoEventArgs> Modificando;
        int modificando = 0;
        double centroAnt = -60;
        private void OnModificando(ModificandoEventArgs e)
        {
            Modificando?.Invoke(this, e);
        }
        public VentanaAnadirVehiculo(ObservableCollection<Vehiculo> listaVehiculos, String tipoVehiculo, String marca, String modelo, String matricula, int kilometraje, String tipoCombustible)
        {
            InitializeComponent();
            this.listaVehiculos = listaVehiculos;
            if (!String.IsNullOrEmpty(tipoVehiculo))
            {
                CBtipoVehiculo.Text = tipoVehiculo;
                CBmarca.Text = marca;
                TBmodelo.Text = modelo;
                TBmatricula.Text = matricula;
                TBkilometraje.Text = kilometraje.ToString();
                CBtipoCombustible.Text = tipoCombustible;
                modificando = 1;
                TBkilometraje.IsReadOnly = true;

            }
        }      
        private void botonAnadir_Click(object sender, RoutedEventArgs e)
        {
            if (String.IsNullOrEmpty(CBtipoVehiculo.Text) || String.IsNullOrEmpty(CBmarca.Text) || String.IsNullOrEmpty(TBmodelo.Text)
                || String.IsNullOrEmpty(TBmatricula.Text) || String.IsNullOrEmpty(TBkilometraje.Text) || String.IsNullOrEmpty(CBtipoCombustible.Text))
            {
                String msg = "No se han introducido todos los campos.";
                String titulo = "Error";
                MessageBoxButton botones = MessageBoxButton.OK;
                MessageBoxImage icono = MessageBoxImage.Error;
                MessageBox.Show(msg, titulo, botones, icono);
                return;
            }

            int kilometraje = Int32.Parse(TBkilometraje.Text);
            if (kilometraje < 0)
            {
                String msg = "El kilometraje debe ser mayor que cero.";
                String titulo = "Error";
                MessageBoxButton botones = MessageBoxButton.OK;
                MessageBoxImage icono = MessageBoxImage.Error;
                MessageBox.Show(msg, titulo, botones, icono);
                return;
            }

            Vehiculos tipoVehiculo = (Vehiculos)Enum.Parse(typeof(Vehiculos), CBtipoVehiculo.Text);
            String marca = CBmarca.Text;
            String modelo = TBmodelo.Text;
            String matricula = TBmatricula.Text;
            Combustibles tipoCombustible = (Combustibles)Enum.Parse(typeof(Combustibles), CBtipoCombustible.Text);
            Vehiculo vehiculo = new Vehiculo(tipoVehiculo, marca, modelo, matricula, tipoCombustible, kilometraje);
     
            if (modificando == 0)
                   listaVehiculos.Add(vehiculo);
            else
                OnModificando(new ModificandoEventArgs(vehiculo));

            this.Hide();
            CBtipoVehiculo.SelectedIndex = -1;
            CBmarca.SelectedIndex = -1;
            CBtipoCombustible.SelectedIndex = -1;
            TBmodelo.Clear();
            TBmatricula.Clear();
            TBkilometraje.Clear();
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            e.Cancel = true;
            CBtipoVehiculo.SelectedIndex = -1;
            CBmarca.SelectedIndex = -1;
            CBtipoCombustible.SelectedIndex = -1;
            TBmodelo.Clear();
            TBmatricula.Clear();
            TBkilometraje.Clear();
            Hide();
        }

        private void TBkilometraje_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (Char.IsDigit(e.Text[0]))
            {
                // No hacemos nada porque aceptamos los dígitos
            }
            else if (e.Text == "\b")
            {
                // No hacemos nada porque aceptamos el Backspace
            }
            else
            {
                // Nos saltamos el carácter deteniendo el enrutamiento
                e.Handled = true;
            }
        }
    }
}


