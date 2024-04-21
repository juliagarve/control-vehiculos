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
    /// Lógica de interacción para VentanaAnadirRepostaje.xaml
    /// </summary>
    public partial class VentanaAnadirRepostaje : Window
    {
        ObservableCollection<Vehiculo> listaVehiculos;
        public VentanaAnadirRepostaje(ObservableCollection<Vehiculo> listaVehiculos)
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
            if (String.IsNullOrEmpty(CBvehiculo.Text) || String.IsNullOrEmpty(DPfecha.Text) || String.IsNullOrEmpty(CBhora.Text)
                || String.IsNullOrEmpty(CBminutos.Text) || String.IsNullOrEmpty(TBkilometraje.Text) || String.IsNullOrEmpty(TBlitros.Text) || String.IsNullOrEmpty(TBcoste.Text))
            {
                String msg = "No se han introducido todos los campos.";
                String titulo = "Error";
                MessageBoxButton botones = MessageBoxButton.OK;
                MessageBoxImage icono = MessageBoxImage.Error;
                MessageBox.Show(msg, titulo, botones, icono);

                return;
            }

            Vehiculo vehiculo = (Vehiculo)CBvehiculo.SelectedItem;
            int kilometraje = Int32.Parse(TBkilometraje.Text);
            float litros = float.Parse(TBlitros.Text);
            float coste = float.Parse(TBcoste.Text);

            if (kilometraje == 0 || litros == 0 || coste == 0)
            {
                String msg = "El kilometraje, los litros y costes deben ser todos mayor que cero.";
                String titulo = "Error";
                MessageBoxButton botones = MessageBoxButton.OK;
                MessageBoxImage icono = MessageBoxImage.Error;
                MessageBox.Show(msg, titulo, botones, icono);
                return;
            }

            if (kilometraje <= vehiculo.kilometraje)
            {
                String msg = "El kilometraje debe ser mayor que el del último registro.";
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
            Gastos tipoGasto = Gastos.Repostaje;
            Gasto repostaje = new Gasto(vehiculo, tipoGasto, fechaTotal, kilometraje, litros, coste);
            vehiculo.kilometraje = kilometraje;

            if (vehiculo.ListaGastos.Count + 1 > 1)
            {
                float acumuladorLitros = 0, acumuladorCoste = 0;
                int kilometrajeMax = kilometraje;
                int kilometrajeMin = vehiculo.ListaGastos[0].kilometraje;

                if (vehiculo.ListaGastos.Count > 1)
                {
                    for (int i = 1; i < vehiculo.ListaGastos.Count; i++)
                    {
                        acumuladorCoste = acumuladorCoste + vehiculo.ListaGastos[i].coste;
                        acumuladorLitros = acumuladorLitros + vehiculo.ListaGastos[i].litros;
                    }
                }
                acumuladorCoste = acumuladorCoste + repostaje.coste;
                acumuladorLitros = acumuladorLitros + repostaje.litros;
                vehiculo.mediaConsumo = acumuladorLitros * 100 / (kilometrajeMax - kilometrajeMin);
                vehiculo.mediaCoste = acumuladorCoste * 100 / (kilometrajeMax - kilometrajeMin);
            }

            vehiculo.ListaGastos.Add(repostaje);

            this.Hide();
            CBhora.SelectedIndex = -1;
            CBminutos.SelectedIndex = -1;
            CBvehiculo.SelectedIndex = -1;
            TBlitros.Clear();
            TBcoste.Clear();
            TBkilometraje.Clear();
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            e.Cancel = true;
            CBhora.SelectedIndex = -1;
            CBminutos.SelectedIndex = -1;
            CBvehiculo.SelectedIndex = -1;
            TBlitros.Clear();
            TBcoste.Clear();
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
