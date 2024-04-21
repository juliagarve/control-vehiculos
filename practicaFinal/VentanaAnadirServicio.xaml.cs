using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
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
    /// Lógica de interacción para ventanaAnadirServicio.xaml
    /// </summary>
    public partial class VentanaAnadirServicio : Window
    {
        ObservableCollection<Vehiculo> listaVehiculos;
        public VentanaAnadirServicio(ObservableCollection<Vehiculo> listaVehiculos)
        {
            InitializeComponent();
            this.listaVehiculos = listaVehiculos;
            CBvehiculo.ItemsSource = listaVehiculos;
            for (int i = 0; i < 25; i++)
            {
                if (i < 10)
                    CBhora.Items.Add("0" + i.ToString());
                else
                    CBhora.Items.Add(i.ToString());
            }
            for (int i = 0; i < 60; i++)
            {
                if (i < 10)
                    CBminutos.Items.Add("0" + i.ToString());
                else
                    CBminutos.Items.Add(i.ToString());
            }
        }

        private void botonAnadir_Click(object sender, RoutedEventArgs e)
        {
            if (String.IsNullOrEmpty(CBvehiculo.Text) || String.IsNullOrEmpty(DPfecha.Text) || String.IsNullOrEmpty(CBhora.Text) || String.IsNullOrEmpty(CBtipoServicio.Text)
                || String.IsNullOrEmpty(CBminutos.Text) || String.IsNullOrEmpty(TBcoste.Text))
            {
                String msg = "No se han introducido todos los campos.";
                String titulo = "Error";
                MessageBoxButton botones = MessageBoxButton.OK;
                MessageBoxImage icono = MessageBoxImage.Error;
                MessageBox.Show(msg, titulo, botones, icono);

                return;
            }

            Vehiculo vehiculo = (Vehiculo)CBvehiculo.SelectedItem;
            float coste = float.Parse(TBcoste.Text);

            if (coste == 0)
            {
                String msg = "El coste debe ser mayor que cero.";
                String titulo = "Error";
                MessageBoxButton botones = MessageBoxButton.OK;
                MessageBoxImage icono = MessageBoxImage.Error;
                MessageBox.Show(msg, titulo, botones, icono);
                return;
            }

            DateTime diaMesAnno = (DateTime)DPfecha.SelectedDate;
            int horas = Int32.Parse(CBhora.Text);
            int minutos = Int32.Parse(CBminutos.Text);
            DateTime fechaTotal = new DateTime(diaMesAnno.Year, diaMesAnno.Month, diaMesAnno.Day, horas, minutos, 0);
            String detalles = CBtipoServicio.Text;
            Gastos tipoGasto = Gastos.Servicio;
            Gasto gasto = new Gasto(vehiculo, tipoGasto, fechaTotal, detalles, coste);
            vehiculo.gastosServiciosTotales = vehiculo.gastosServiciosTotales + coste;
            vehiculo.ListaGastos.Add(gasto);

            this.Hide();
            CBhora.SelectedIndex = -1;
            CBminutos.SelectedIndex = -1;
            CBvehiculo.SelectedIndex = -1;
            CBtipoServicio.SelectedIndex = -1;
            TBcoste.Clear();
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            e.Cancel = true;
            CBhora.SelectedIndex = -1;
            CBminutos.SelectedIndex = -1;
            CBvehiculo.SelectedIndex = -1;
            CBtipoServicio.SelectedIndex = -1;
            TBcoste.Clear();
            Hide();
        }

        private void TBcoste_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            NumberFormatInfo numberFormatInfo = System.Globalization.CultureInfo.CurrentCulture.NumberFormat;
            string decimalSeparator = numberFormatInfo.NumberDecimalSeparator;
            if (Char.IsDigit(e.Text[0]))
            {
                // No hacemos nada porque aceptamos los dígitos
            }
            else if (e.Text.Equals(decimalSeparator))
            {
                // No hacemos nada porque aceptamos el separador decimal
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
