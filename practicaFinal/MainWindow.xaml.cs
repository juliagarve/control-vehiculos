using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Media.Media3D;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace PracticaFinal
{
    /// <summary>
    /// Lógica de interacción para MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        ObservableCollection<Vehiculo> listaVehiculos = new ObservableCollection<Vehiculo>();
        VentanaAnadirVehiculo ventanaAnadirVehiculo;
        VentanaTablas ventanaTablas;
        VentanaAnadirRepostaje ventanaAnadirRepostaje;
        VentanaAnadirGasto ventanaAnadirGasto;
        VentanaAnadirServicio ventanaAnadirServicio;
        int tipoGrafico = 0;

        int cont = 0;
        double centroAnt = -60;
        double costeMax = 0;
        double consumoMax = 0;
        double kmMax = 0;
        double gastosOtrosMax = 0;
        double gastosServiciosMax = 0;
        double costeMaxMarca = 0;
        double consumoMaxMarca = 0;
        double kmMaxMarca = 0;
        double gastosOtrosMaxMarca = 0;
        double gastosServiciosMaxMarca = 0;
        Vehiculo vehiculoSeleccionado;
        public MainWindow()
        {
            InitializeComponent();
            ventanaAnadirVehiculo = new VentanaAnadirVehiculo(listaVehiculos, "", "", "", "", 0, "");
            ventanaTablas = new VentanaTablas(listaVehiculos);
            ventanaTablas.Seleccionando += graficarLineas;
            ventanaTablas.Borrando += graficar;
            ventanaAnadirRepostaje = new VentanaAnadirRepostaje(listaVehiculos);
            ventanaAnadirGasto = new VentanaAnadirGasto(listaVehiculos);
            ventanaAnadirServicio = new VentanaAnadirServicio(listaVehiculos);
            listaVehiculos.CollectionChanged += ListaVehiculos_CollectionChanged;
            botonVolverAtras.Visibility = Visibility.Collapsed;
            botonGraficoOtrosGastos.Visibility = Visibility.Collapsed;
            botonGraficoRepostajes.Visibility = Visibility.Collapsed;
            botonGraficoServicios.Visibility = Visibility.Collapsed;
            botonAnadirRepostaje.Visibility = Visibility.Collapsed;
            botonAnadirServicio.Visibility = Visibility.Collapsed;
            botonAnadirGasto.Visibility = Visibility.Collapsed;

        }

        private void ListaVehiculos_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {

            if (listaVehiculos.Count > 0)
            {
                botonAnadirServicio.Visibility = Visibility.Visible;
                botonAnadirRepostaje.Visibility = Visibility.Visible;
                botonAnadirGasto.Visibility = Visibility.Visible;
            }
            else
            {
                lienzo.Children.Clear();
                lienzo2.Children.Clear();
                botonAnadirServicio.Visibility = Visibility.Collapsed;
                botonAnadirRepostaje.Visibility = Visibility.Collapsed;
                botonAnadirGasto.Visibility = Visibility.Collapsed;
            }

            if (e.OldItems != null)
            {
                costeMax = 0;
                consumoMax = 0;
                kmMax = 0;
            }

            if (e.NewItems != null)
            {
                foreach (Vehiculo v in e.NewItems)
                {
                    v.ListaGastos.CollectionChanged += ListaGastos_CollectionChanged;
                    if (centroAnt + 200 > lienzo.ActualWidth)
                        lienzo.Width = lienzo.ActualWidth + 200;
                    v.centro = centroAnt + 180;
                    centroAnt = v.centro;
                    graficar(this, new BorrandoEventArgs(v));
                }
            }
        }

        private void ListaGastos_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            if (e.NewItems != null)
            {
                foreach (Gasto r in e.NewItems)
                {
                    graficar(this, new BorrandoEventArgs(r.vehiculo));
                }
            }
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            ventanaAnadirVehiculo.Show();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            ventanaAnadirRepostaje.Show();
        }

        private void graficar(object sender, BorrandoEventArgs e)
        {
            Vehiculo v = e.vehiculo;
            if (v.mediaCoste <= costeMax && v.mediaConsumo <= consumoMax && v.kilometraje <= kmMax
                && v.gastosOtrosTotales <= gastosOtrosMax && v.gastosServiciosTotales <= gastosServiciosMax)
            {
                Label Lmatricula = new Label();
                Lmatricula.Content = v.matricula;
                Canvas.SetTop(Lmatricula, lienzo.ActualHeight + 1);
                Canvas.SetLeft(Lmatricula, v.centro - 36);
                lienzo.Children.Add(Lmatricula);

                if (v.mediaCoste > 0)
                    pintarBarra(v.matricula, v.mediaCoste, costeMaxMarca, Brushes.Red, v.centro - 60);
                if (v.mediaConsumo > 0)
                    pintarBarra(v.matricula, v.mediaConsumo, consumoMaxMarca, Brushes.Green, v.centro - 30);
                if (v.kilometraje > 0)
                    pintarBarra(v.matricula, v.kilometraje, kmMaxMarca, Brushes.Blue, v.centro);
                if (v.gastosOtrosTotales > 0)
                    pintarBarra(v.matricula, v.gastosOtrosTotales, gastosOtrosMaxMarca, Brushes.Orange, v.centro + 30);
                if (v.gastosServiciosTotales > 0)
                    pintarBarra(v.matricula, v.gastosServiciosTotales, gastosServiciosMaxMarca, Brushes.Purple, v.centro + 60);

            }
            else
            {
                lienzo.Children.Clear();
                lienzo2.Children.Clear();

                Line leyCoste = new Line();
                leyCoste.X1 = lienzo2.ActualWidth / 7 - 25;
                leyCoste.X2 = lienzo2.ActualWidth / 7 - 10;
                leyCoste.Y1 = lienzo2.ActualHeight - 24;
                leyCoste.Y2 = lienzo2.ActualHeight - 24;
                leyCoste.Stroke = Brushes.Red;
                leyCoste.StrokeThickness = 5;
                lienzo2.Children.Add(leyCoste);
                Label textCoste = new Label();
                textCoste.Content = "Coste (€)";
                textCoste.Foreground = Brushes.Red;
                Canvas.SetBottom(textCoste, 12);
                Canvas.SetLeft(textCoste, lienzo2.ActualWidth / 7);
                lienzo2.Children.Add(textCoste);
                Line leyConsumo = new Line();
                leyConsumo.X1 = lienzo2.ActualWidth / 7 * 2 - 45;
                leyConsumo.X2 = lienzo2.ActualWidth / 7 * 2 - 30;
                leyConsumo.Y1 = lienzo2.ActualHeight - 24;
                leyConsumo.Y2 = lienzo2.ActualHeight - 24;
                leyConsumo.Stroke = Brushes.Green;
                leyConsumo.StrokeThickness = 5;
                lienzo2.Children.Add(leyConsumo);
                Label textConsumo = new Label();
                textConsumo.Content = "Consumo (Litros)";
                textConsumo.Foreground = Brushes.Green;
                Canvas.SetBottom(textConsumo, 12);
                Canvas.SetLeft(textConsumo, lienzo2.ActualWidth / 7 * 2 - 20);
                lienzo2.Children.Add(textConsumo);
                Line leyKm = new Line();
                leyKm.X1 = lienzo2.ActualWidth / 7 * 3 - 20;
                leyKm.X2 = lienzo2.ActualWidth / 7 * 3 - 5;
                leyKm.Y1 = lienzo2.ActualHeight - 24;
                leyKm.Y2 = lienzo2.ActualHeight - 24;
                leyKm.Stroke = Brushes.Blue;
                leyKm.StrokeThickness = 5;
                lienzo2.Children.Add(leyKm);
                Label textKm = new Label();
                textKm.Content = "Km";
                textKm.Foreground = Brushes.Blue;
                Canvas.SetBottom(textKm, 12);
                Canvas.SetLeft(textKm, lienzo2.ActualWidth / 7 * 3 + 5);
                lienzo2.Children.Add(textKm);
                Line leyOtrosGastos = new Line();
                leyOtrosGastos.X1 = lienzo2.ActualWidth / 7 * 4 - 65;
                leyOtrosGastos.X2 = lienzo2.ActualWidth / 7 * 4 - 50;
                leyOtrosGastos.Y1 = lienzo2.ActualHeight - 24;
                leyOtrosGastos.Y2 = lienzo2.ActualHeight - 24;
                leyOtrosGastos.Stroke = Brushes.Orange;
                leyOtrosGastos.StrokeThickness = 5;
                lienzo2.Children.Add(leyOtrosGastos);
                Label textOtrosGastos = new Label();
                textOtrosGastos.Content = "Total otros gastos (€)";
                textOtrosGastos.Foreground = Brushes.Orange;
                Canvas.SetBottom(textOtrosGastos, 12);
                Canvas.SetLeft(textOtrosGastos, lienzo2.ActualWidth / 7 * 4 - 40);
                lienzo2.Children.Add(textOtrosGastos);
                Line leyServicios = new Line();
                leyServicios.X1 = lienzo2.ActualWidth / 7 * 5 - 15;
                leyServicios.X2 = lienzo2.ActualWidth / 7 * 5;
                leyServicios.Y1 = lienzo2.ActualHeight - 24;
                leyServicios.Y2 = lienzo2.ActualHeight - 24;
                leyServicios.Stroke = Brushes.Purple;
                leyServicios.StrokeThickness = 5;
                lienzo2.Children.Add(leyServicios);
                Label textServicios = new Label();
                textServicios.Content = "Total servicios (€)";
                textServicios.Foreground = Brushes.Purple;
                Canvas.SetBottom(textServicios, 12);
                Canvas.SetLeft(textServicios, lienzo2.ActualWidth / 7 * 5 + 10);
                lienzo2.Children.Add(textServicios);

                if (v.mediaCoste > costeMax)
                    costeMax = v.mediaCoste;
                if (v.mediaConsumo > consumoMax)
                    consumoMax = v.mediaConsumo;
                if (v.kilometraje > kmMax)
                    kmMax = v.kilometraje;
                if (v.gastosOtrosTotales > gastosOtrosMax)
                    gastosOtrosMax = v.gastosOtrosTotales;
                if (v.gastosServiciosTotales > gastosServiciosMax)
                    gastosServiciosMax = v.gastosServiciosTotales;

                double dist1 = grid.ColumnDefinitions[0].ActualWidth;
                double dist2 = grid.ColumnDefinitions[9].ActualWidth;

                if (costeMax > 0)
                    costeMaxMarca = determinarMax_Y_pintarEscala(costeMax, 5, Brushes.Red, dist1 - 40);
                if (consumoMax > 0)
                    consumoMaxMarca = determinarMax_Y_pintarEscala(consumoMax, 3, Brushes.Green, dist1);
                if (kmMax > 0)
                    kmMaxMarca = determinarMax_Y_pintarEscala(kmMax, 11, Brushes.Blue, lienzo2.ActualWidth - dist2);
                if (gastosOtrosMax > 0)
                    gastosOtrosMaxMarca = determinarMax_Y_pintarEscala(gastosOtrosMax, 5, Brushes.Orange, lienzo2.ActualWidth - dist2 + 50);
                if (gastosServiciosMax > 0)
                    gastosServiciosMaxMarca = determinarMax_Y_pintarEscala(gastosServiciosMax, 3, Brushes.Purple, lienzo2.ActualWidth - dist2 + 100);

                foreach (Vehiculo v2 in listaVehiculos)
                {
                    Label Lmatricula = new Label();
                    Lmatricula.Content = v2.matricula;
                    Canvas.SetTop(Lmatricula, lienzo.ActualHeight + 1);
                    Canvas.SetLeft(Lmatricula, v2.centro - 36);
                    lienzo.Children.Add(Lmatricula);

                    if (v2.mediaCoste > 0)
                        pintarBarra(v2.matricula, v2.mediaCoste, costeMaxMarca, Brushes.Red, v2.centro - 60);
                    if (v2.mediaConsumo > 0)
                        pintarBarra(v2.matricula, v2.mediaConsumo, consumoMaxMarca, Brushes.Green, v2.centro - 30);
                    if (v2.kilometraje > 0)
                        pintarBarra(v2.matricula, v2.kilometraje, kmMaxMarca, Brushes.Blue, v2.centro);
                    if (v2.gastosOtrosTotales > 0)
                        pintarBarra(v2.matricula, v2.gastosOtrosTotales, gastosOtrosMaxMarca, Brushes.Orange, v2.centro + 30);
                    if (v2.gastosServiciosTotales > 0)
                        pintarBarra(v2.matricula, v2.gastosServiciosTotales, gastosServiciosMaxMarca, Brushes.Purple, v2.centro + 60);
                }
            }
        }

        private double determinarMax_Y_pintarEscala(double valorMax, int numMarcas, SolidColorBrush color, double distIzq)
        {
            double yMaxReal = Math.Round(valorMax * 1.2);
            double incrementoReal = Math.Round(yMaxReal / (numMarcas - 1));
            double numDigitos = Math.Floor(Math.Log10(incrementoReal) + 1);
            int firstDigit = (int)(incrementoReal.ToString()[0]) - 48;
            incrementoReal = (firstDigit + 1) * (Math.Pow(10, numDigitos - 1));
            yMaxReal = incrementoReal * (numMarcas - 1);

            double incrementoPant = incrementoReal * lienzo.ActualHeight / yMaxReal;
            double alturaAntPant = -incrementoPant;
            double alturaAntReal = yMaxReal + incrementoReal;

            for (int i = 0; i < numMarcas; i++)
            {
                Line marca = new Line();
                marca.Y1 = alturaAntPant + incrementoPant + lienzo.Margin.Top;
                marca.Y2 = alturaAntPant + incrementoPant + lienzo.Margin.Top;
                marca.X1 = distIzq;
                if (color == Brushes.Blue || color == Brushes.Orange || color == Brushes.Purple)
                    marca.X2 = distIzq + 10;
                else
                    marca.X2 = distIzq - 10;
                marca.Stroke = color;
                marca.StrokeThickness = 3;
                lienzo2.Children.Add(marca);
                Label num = new Label();
                num.Content = alturaAntReal - incrementoReal;
                num.Foreground = color;
                if (color == Brushes.Blue || color == Brushes.Orange || color == Brushes.Purple)
                    Canvas.SetLeft(num, distIzq + 10);
                else
                    Canvas.SetRight(num, lienzo2.ActualWidth - distIzq + 10);
                Canvas.SetTop(num, marca.Y1 - 13);
                lienzo2.Children.Add(num);
                alturaAntPant = alturaAntPant + incrementoPant;
                alturaAntReal = alturaAntReal - incrementoReal;
            }

            return yMaxReal;
        }

        private void pintarBarra(String matricula, double valorMax, double valorMaxMarca, SolidColorBrush color, double centro)
        {
            Line barra = new Line();
            barra.X1 = centro;
            barra.X2 = centro;
            if (valorMax > 0)
                barra.Y1 = lienzo.ActualHeight - (valorMax * lienzo.ActualHeight / valorMaxMarca);
            else
                barra.Y1 = lienzo.ActualHeight;
            barra.Y2 = lienzo.ActualHeight;
            barra.Stroke = color;
            barra.StrokeThickness = 20;
            barra.ToolTip = valorMax;
            lienzo.Children.Add(barra);
        }

        private void graficarLineas(object sender, SeleccionandoEventArgs e)
        {
            tipoGrafico = 0;
            vehiculoSeleccionado = e.vehiculo;
            Vehiculo v = e.vehiculo;
            botonAnadirServicio.Visibility = Visibility.Collapsed;
            botonAnadirRepostaje.Visibility = Visibility.Collapsed;
            botonAnadirGasto.Visibility = Visibility.Collapsed;
            botonAnadirVehiculo.Visibility = Visibility.Collapsed;
            botonVolverAtras.Visibility = Visibility.Visible;
            botonGraficoOtrosGastos.Visibility = Visibility.Visible;
            botonGraficoRepostajes.Visibility = Visibility.Visible;
            botonGraficoServicios.Visibility = Visibility.Visible;
            lienzo.Children.Clear();
            lienzo2.Children.Clear();
            costeMax = 0;
            consumoMax = 0;
            kmMax = 0;

            Line leyCoste = new Line();
            leyCoste.X1 = lienzo2.ActualWidth / 5 - 25;
            leyCoste.X2 = lienzo2.ActualWidth / 5 - 10;
            leyCoste.Y1 = lienzo2.ActualHeight - 32;
            leyCoste.Y2 = lienzo2.ActualHeight - 32;
            leyCoste.Stroke = Brushes.Red;
            leyCoste.StrokeThickness = 5;
            lienzo2.Children.Add(leyCoste);
            Label textCoste = new Label();
            textCoste.Content = "Coste (€)";
            textCoste.Foreground = Brushes.Red;
            Canvas.SetBottom(textCoste, 20);
            Canvas.SetLeft(textCoste, lienzo2.ActualWidth / 5);
            lienzo2.Children.Add(textCoste);
            Line leyConsumo = new Line();
            leyConsumo.X1 = lienzo2.ActualWidth / 5 * 2 - 45;
            leyConsumo.X2 = lienzo2.ActualWidth / 5 * 2 - 30;
            leyConsumo.Y1 = lienzo2.ActualHeight - 32;
            leyConsumo.Y2 = lienzo2.ActualHeight - 32;
            leyConsumo.Stroke = Brushes.Green;
            leyConsumo.StrokeThickness = 5;
            lienzo2.Children.Add(leyConsumo);
            Label textConsumo = new Label();
            textConsumo.Content = "Consumo (Litros)";
            textConsumo.Foreground = Brushes.Green;
            Canvas.SetBottom(textConsumo, 20);
            Canvas.SetLeft(textConsumo, lienzo2.ActualWidth / 5 * 2 - 20);
            lienzo2.Children.Add(textConsumo);
            Line leyKm = new Line();
            leyKm.X1 = lienzo2.ActualWidth / 5 * 3 - 35;
            leyKm.X2 = lienzo2.ActualWidth / 5 * 3 - 20;
            leyKm.Y1 = lienzo2.ActualHeight - 32;
            leyKm.Y2 = lienzo2.ActualHeight - 32;
            leyKm.Stroke = Brushes.Blue;
            leyKm.StrokeThickness = 5;
            lienzo2.Children.Add(leyKm);
            Label textKm = new Label();
            textKm.Content = "Km";
            textKm.Foreground = Brushes.Blue;
            Canvas.SetBottom(textKm, 20);
            Canvas.SetLeft(textKm, lienzo2.ActualWidth / 5 * 3 - 10);
            lienzo2.Children.Add(textKm);

            foreach (Gasto r in v.ListaGastos)
            {
                if (r.tipoGasto == Gastos.Repostaje)
                {
                    if (r.coste > costeMax)
                        costeMax = r.coste;
                    if (r.litros > consumoMax)
                        consumoMax = r.litros;

                    //Determinar si es el primer repostaje o no
                    int i = 0;
                    while (v.ListaGastos[i] != r && v.ListaGastos[i].tipoGasto != Gastos.Repostaje)
                    {
                        i++;
                    }

                    if (v.ListaGastos[i] != r)
                    {
                        //No es el primer repostaje, encontrar ahora el repostaje anterior
                        i = v.ListaGastos.IndexOf(r) - 1;
                        while (v.ListaGastos[i].tipoGasto != Gastos.Repostaje)
                        {
                            i--;
                        }
                        if (r.kilometraje - v.ListaGastos[i].kilometraje > kmMax)
                            kmMax = r.kilometraje - v.ListaGastos[i].kilometraje;
                    }
                }
            }

            double dist1 = grid.ColumnDefinitions[0].ActualWidth;
            double dist2 = grid.ColumnDefinitions[9].ActualWidth;

            if (costeMax > 0)
                costeMaxMarca = determinarMax_Y_pintarEscala(costeMax, 5, Brushes.Red, dist1 - 40);
            if (consumoMax > 0)
                consumoMaxMarca = determinarMax_Y_pintarEscala(consumoMax, 3, Brushes.Green, dist1);
            if (kmMax > 0)
                kmMaxMarca = determinarMax_Y_pintarEscala(kmMax, 11, Brushes.Blue, lienzo2.ActualWidth - dist2);

            Polyline lineaCoste = new Polyline();
            lineaCoste.Stroke = Brushes.Red;
            var puntosCoste = new PointCollection();
            Polyline lineaConsumo = new Polyline();
            lineaConsumo.Stroke = Brushes.Green;
            var puntosConsumo = new PointCollection();
            Polyline lineaKm = new Polyline();
            lineaKm.Stroke = Brushes.Blue;
            var puntoskm = new PointCollection();

            double centro = -60;
            foreach (Gasto r in v.ListaGastos)
            {
                if (r.tipoGasto == Gastos.Repostaje)
                {
                    centro = centro + 120;
                    Label fecha = new Label();
                    fecha.Content = r.fecha.ToString("dd-MM-yyyy");
                    Canvas.SetTop(fecha, lienzo.ActualHeight + 5);
                    Canvas.SetLeft(fecha, centro - 36);
                    lienzo.Children.Add(fecha);

                    if (costeMax > 0)
                        puntosCoste.Add(new Point(centro, lienzo.ActualHeight - (r.coste * lienzo.ActualHeight / costeMaxMarca)));
                    if (consumoMax > 0)
                        puntosConsumo.Add(new Point(centro, lienzo.ActualHeight - (r.litros * lienzo.ActualHeight / consumoMaxMarca)));
                    //Determinar si es el primer repostaje o no
                    int i = 0;
                    while (v.ListaGastos[i] != r && v.ListaGastos[i].tipoGasto != Gastos.Repostaje)
                    {
                        i++;
                    }

                    if (v.ListaGastos[i] != r)
                    {
                        //No es el primer repostaje, encontrar ahora el repostaje anterior
                        i = v.ListaGastos.IndexOf(r) - 1;
                        while (v.ListaGastos[i].tipoGasto != Gastos.Repostaje)
                        {
                            i--;
                        }
                        int kmRecorridos = r.kilometraje - v.ListaGastos[i].kilometraje;
                        puntoskm.Add(new Point(centro, lienzo.ActualHeight - (kmRecorridos * lienzo.ActualHeight / kmMaxMarca)));
                    }
                }
            }
            lineaCoste.Points = puntosCoste;
            lineaConsumo.Points = puntosConsumo;
            lineaKm.Points = puntoskm;
            lienzo.Children.Add(lineaCoste);
            lienzo.Children.Add(lineaConsumo);
            lienzo.Children.Add(lineaKm);
        }
        private void Window_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            if (cont != 0)
            {
                double ancho = this.ActualWidth - grid.ColumnDefinitions[0].ActualWidth - grid.ColumnDefinitions[9].ActualWidth;
                if(ancho > lienzo.ActualWidth)
                    lienzo.Width = ancho;
                costeMax = 0;
                consumoMax = 0;
                kmMax = 0;
                if (botonVolverAtras.Visibility == Visibility.Visible)
                {
                    if (tipoGrafico==1)
                    {
                        dibujarSectoresGastos(vehiculoSeleccionado);
                    }
                    if(tipoGrafico==2)
                    {
                        dibujarSectoresServicios(vehiculoSeleccionado);
                    }
                    else
                    {
                        graficarLineas(this, new SeleccionandoEventArgs(vehiculoSeleccionado));
                    }
                }
                else
                {
                    foreach (Vehiculo v in listaVehiculos)
                        graficar(this, new BorrandoEventArgs(v));
                }
            }
            else cont = 1;
        }

        private void botonVerTablas_Click(object sender, RoutedEventArgs e)
        {
            ventanaTablas.Show();
        }

        private void botonVerGrafico_Click(object sender, RoutedEventArgs e)
        {
            botonVolverAtras.Visibility = Visibility.Collapsed;
            botonGraficoOtrosGastos.Visibility=Visibility.Collapsed;
            botonGraficoRepostajes.Visibility = Visibility.Collapsed;
            botonGraficoServicios.Visibility = Visibility.Collapsed;
            botonAnadirVehiculo.Visibility = Visibility.Visible;
            botonAnadirRepostaje.Visibility = Visibility.Visible;
            botonAnadirServicio.Visibility = Visibility.Visible;
            botonAnadirGasto.Visibility = Visibility.Visible;
            costeMax = 0;
            consumoMax = 0;
            kmMax = 0;
            foreach (Vehiculo v in listaVehiculos)
                graficar(this, new BorrandoEventArgs(v));
        }

        private void botonAnadirGasto_Click(object sender, RoutedEventArgs e)
        {
            ventanaAnadirGasto.Show();
        }

        private void botonAnadirServicio_Click(object sender, RoutedEventArgs e)
        {
            ventanaAnadirServicio.Show();
        }

        private void botonGraficoOtrosGastos_Click(object sender, RoutedEventArgs e)
        {
            dibujarSectoresGastos(vehiculoSeleccionado);
        }

        private void dibujarSectoresGastos(Vehiculo v)
        {
            tipoGrafico = 1;
            lienzo.Children.Clear();
            lienzo2.Children.Clear();

            Line leyEstacionamiento = new Line();
            leyEstacionamiento.X1 = lienzo2.ActualWidth / 5 - 25;
            leyEstacionamiento.X2 = lienzo2.ActualWidth / 5 - 10;
            leyEstacionamiento.Y1 = lienzo2.ActualHeight - 32;
            leyEstacionamiento.Y2 = lienzo2.ActualHeight - 32;
            leyEstacionamiento.Stroke = Brushes.DarkCyan;
            leyEstacionamiento.StrokeThickness = 5;
            lienzo2.Children.Add(leyEstacionamiento);
            Label textEstacionamiento = new Label();
            textEstacionamiento.Content = "Estacionamiento";
            textEstacionamiento.Foreground = Brushes.DarkCyan;
            Canvas.SetBottom(textEstacionamiento, 20);
            Canvas.SetLeft(textEstacionamiento, lienzo2.ActualWidth / 5);
            lienzo2.Children.Add(textEstacionamiento);
            Line leyMulta = new Line();
            leyMulta.X1 = lienzo2.ActualWidth / 5 * 2 - 45;
            leyMulta.X2 = lienzo2.ActualWidth / 5 * 2 - 30;
            leyMulta.Y1 = lienzo2.ActualHeight - 32;
            leyMulta.Y2 = lienzo2.ActualHeight - 32;
            leyMulta.Stroke = Brushes.DarkTurquoise;
            leyMulta.StrokeThickness = 5;
            lienzo2.Children.Add(leyMulta);
            Label textMulta = new Label();
            textMulta.Content = "Multa (€)";
            textMulta.Foreground = Brushes.DarkTurquoise;
            Canvas.SetBottom(textMulta, 20);
            Canvas.SetLeft(textMulta, lienzo2.ActualWidth / 5 * 2 - 20);
            lienzo2.Children.Add(textMulta);
            Line leyPeaje = new Line();
            leyPeaje.X1 = lienzo2.ActualWidth / 5 * 3 - 85;
            leyPeaje.X2 = lienzo2.ActualWidth / 5 * 3 - 70;
            leyPeaje.Y1 = lienzo2.ActualHeight - 32;
            leyPeaje.Y2 = lienzo2.ActualHeight - 32;
            leyPeaje.Stroke = Brushes.Aquamarine;
            leyPeaje.StrokeThickness = 5;
            lienzo2.Children.Add(leyPeaje);
            Label textPeaje = new Label();
            textPeaje.Content = "Peaje (€)";
            textPeaje.Foreground = Brushes.Aquamarine;
            Canvas.SetBottom(textPeaje, 20);
            Canvas.SetLeft(textPeaje, lienzo2.ActualWidth / 5 * 3 - 60);
            lienzo2.Children.Add(textPeaje);
            Line leyImpuestos = new Line();
            leyImpuestos.X1 = lienzo2.ActualWidth / 5 * 4 - 115;
            leyImpuestos.X2 = lienzo2.ActualWidth / 5 * 4 - 100;
            leyImpuestos.Y1 = lienzo2.ActualHeight - 32;
            leyImpuestos.Y2 = lienzo2.ActualHeight - 32;
            leyImpuestos.Stroke = Brushes.MediumAquamarine;
            leyImpuestos.StrokeThickness = 5;
            lienzo2.Children.Add(leyImpuestos);
            Label textImpuestos = new Label();
            textImpuestos.Content = "Impuestos (€)";
            textImpuestos.Foreground = Brushes.MediumAquamarine;
            Canvas.SetBottom(textImpuestos, 20);
            Canvas.SetLeft(textImpuestos, lienzo2.ActualWidth / 5 * 4 - 80);
            lienzo2.Children.Add(textImpuestos);

            double totalEstacionamiento = 0, totalMulta = 0, totalPeajes = 0, totalImpuestos = 0;
            foreach (Gasto r in v.ListaGastos)
            {
                //Calcular el total de cada tipo de Otro gasto

                if(r.tipoGasto == Gastos.OtroGasto)
                    if (r.detalles == "Estacionamiento")
                        totalEstacionamiento = totalEstacionamiento + r.coste;
                if (r.tipoGasto == Gastos.OtroGasto)
                    if (r.detalles == "Multa")
                        totalMulta = totalMulta + r.coste;
                if (r.tipoGasto == Gastos.OtroGasto)
                    if (r.detalles == "Peajes")
                        totalPeajes = totalPeajes + r.coste;
                if (r.tipoGasto == Gastos.OtroGasto)
                    if (r.detalles == "Impuestos")
                        totalImpuestos = totalImpuestos + r.coste;
            }

            double total = totalEstacionamiento + totalImpuestos + totalMulta + totalPeajes;

            if (total > 0)
            {
                double[] totales = { totalEstacionamiento, totalMulta, totalPeajes, totalImpuestos };

                double centrox = 275;
                double centroy = lienzo.ActualHeight / 2;
                double radio = lienzo.ActualHeight / 2 - 40;

                double[] sigCoordenadas = { centrox, centroy - radio };
                SolidColorBrush[] colores = { Brushes.DarkCyan, Brushes.DarkTurquoise, Brushes.Aquamarine, Brushes.MediumAquamarine };
                double angulo = 360;
                Ellipse circulo = new Ellipse();
                circulo.Stroke = Brushes.Black;
                circulo.Width = radio * 2;
                circulo.Height = radio * 2;
                int i = 0;
                while (totales[i] == 0)
                    i++;
                int sig = 0;
                Boolean flag = false;
                int j;
                for(j = i+1; j < 4 && !flag; j++)
                {
                    if(totales[j] != 0)
                    {
                        sig = j;
                        flag = true;
                    }
                }
                if (flag)
                {
                    circulo.Fill = colores[sig];
                    circulo.ToolTip = totales[sig] * 100 / total;
                }
                else
                {
                    circulo.Fill = colores[i];
                    circulo.ToolTip = totales[i] * 100 / total;
                }
                Canvas.SetLeft(circulo, centrox - radio);
                Canvas.SetBottom(circulo, centroy - radio);
                lienzo.Children.Add(circulo);

                i = i + 1;

                while (i < 4)
                {
                    if (totales[i] != 0)
                    {
                        angulo = angulo - 360 * totales[i] / total;
                        PathFigure figuraPath = new PathFigure();
                        figuraPath.StartPoint = new Point(centrox, centroy);

                        LineSegment linea1 = new LineSegment();
                        linea1.Point = new Point(centrox, centroy - radio);

                        //punto final arco angulo
                        double coordX = centrox + radio * Math.Sin(angulo * Math.PI / 180);
                        double coordY = centroy + radio * -Math.Cos(angulo * Math.PI / 180);
                        sigCoordenadas[0] = coordX;
                        sigCoordenadas[1] = coordY;
                        Point puntoFin = new Point(coordX, coordY);

                        ArcSegment arco = new ArcSegment(puntoFin, new Size(radio, radio), 0, angulo >= 180, SweepDirection.Clockwise, true);

                        LineSegment linea2 = new LineSegment();
                        linea2.Point = new Point(centrox, centroy);

                        PathSegmentCollection collecionPath = new PathSegmentCollection();
                        collecionPath.Add(linea1);
                        collecionPath.Add(arco);
                        collecionPath.Add(linea2);

                        figuraPath.Segments = collecionPath;

                        PathFigureCollection collecionFigurasPath = new PathFigureCollection();
                        collecionFigurasPath.Add(figuraPath);

                        PathGeometry GeometriaPath = new PathGeometry();
                        GeometriaPath.Figures = collecionFigurasPath;

                        Path path = new Path();
                        path.Stroke = Brushes.Black;
                        path.StrokeThickness = 1;
                        path.Data = GeometriaPath;

                        flag = false;
                        for (j = i+1; j < 4 && !flag; j++)
                        {
                            if (totales[j] != 0)
                            {
                                sig = j;
                                flag = true;
                            }
                        }
                        if (flag)
                        {
                            path.Fill = colores[sig];
                            path.ToolTip = totales[sig] * 100 / total;
                        }
                        else
                        {
                            for (j = 0; j < i && !flag; j++)
                            {
                                if (totales[j] != 0)
                                {
                                    sig = j;
                                    flag = true;
                                }
                            }
                            path.Fill = colores[sig];
                            path.ToolTip = totales[sig] * 100 / total;
                        }

                        lienzo.Children.Add(path);
                    }
                    i++;
                }
            }
        }

        private void dibujarSectoresServicios(Vehiculo v)
        {
            tipoGrafico = 2;
            lienzo.Children.Clear();
            lienzo2.Children.Clear();

            double incremento = 0;

            double[] altura1 = { lienzo2.ActualHeight - 57, lienzo2.ActualHeight - 42, lienzo2.ActualHeight - 28 };
            double[] altura2 = { 45, 30, 15 };
            SolidColorBrush[] color = {Brushes.Blue, Brushes.BlueViolet, Brushes.Brown, Brushes.BurlyWood, Brushes.CadetBlue, Brushes.Gray, Brushes.Chartreuse,
                                    Brushes.DarkOliveGreen, Brushes.Coral, Brushes.CornflowerBlue, Brushes.Fuchsia, Brushes.Crimson, Brushes.Cyan, Brushes.DarkBlue,
                                    Brushes.Gold, Brushes.Green, Brushes.MediumVioletRed};
            int z = 0;
            int i;
            for (i = 0; i < 6; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    if (z != 17)
                    {
                        Line ley = new Line();
                        ley.X1 = lienzo2.ActualWidth / 8 - 85 + incremento;
                        ley.X2 = lienzo2.ActualWidth / 8 - 70 + incremento;
                        ley.Y1 = altura1[j];
                        ley.Y2 = altura1[j];
                        ley.Stroke = color[z];
                        ley.StrokeThickness = 5;
                        lienzo2.Children.Add(ley);
                        Label texto = new Label();
                        texto.Content = Enum.GetName(typeof(Servicios), z);
                        texto.Foreground = color[z];
                        Canvas.SetBottom(texto, altura2[j]);
                        Canvas.SetLeft(texto, lienzo2.ActualWidth / 8 - 60 + incremento);
                        lienzo2.Children.Add(texto);
                        z++;
                    }
                }
                incremento = incremento + 130;
            }

            double[] totales = new double[17];
            Boolean encontrado;
            foreach (Gasto r in v.ListaGastos)
            {
                //Calcular el total de cada tipo de Otro gasto
                i = 0;
                encontrado = false;
                if (r.tipoGasto == Gastos.Servicio)
                {
                    while (encontrado==false)
                    {
                        if (r.detalles == Enum.GetName(typeof(Servicios), i))
                        {
                            totales[i] = totales[i] + r.coste;
                            encontrado = true;
                        }
                        i++;
                    }
                }
            }

            double total=0;
            for(i=0; i < totales.Length; i++)
            {
                total = total + totales[i];
            }

            if (total > 0)
            {
                double centrox = 275;
                double centroy = lienzo.ActualHeight / 2;
                double radio = lienzo.ActualHeight / 2 - 40;

                double[] sigCoordenadas = { centrox, centroy - radio };
                SolidColorBrush[] colores = {Brushes.Blue, Brushes.BlueViolet, Brushes.Brown, Brushes.BurlyWood, Brushes.CadetBlue, Brushes.Gray, Brushes.Chartreuse,
                                    Brushes.DarkOliveGreen, Brushes.Coral, Brushes.CornflowerBlue, Brushes.Fuchsia, Brushes.Crimson, Brushes.Cyan, Brushes.DarkBlue,
                                    Brushes.Gold, Brushes.Green, Brushes.MediumVioletRed};
                double angulo = 360;
                Ellipse circulo = new Ellipse();
                circulo.Stroke = Brushes.Black;
                circulo.Width = radio * 2;
                circulo.Height = radio * 2;
                i = 0;
                while (totales[i] == 0)
                    i++;
                int sig = 0;
                Boolean flag = false;
                int j;
                for (j = i + 1; j < 17 && !flag; j++)
                {
                    if (totales[j] != 0)
                    {
                        sig = j;
                        flag = true;
                    }
                }
                if (flag)
                {
                    circulo.Fill = colores[sig];
                    circulo.ToolTip = totales[sig] * 100 / total;
                }
                else
                {
                    circulo.Fill = colores[i];
                    circulo.ToolTip = totales[i] * 100 / total;
                }
                Canvas.SetLeft(circulo, centrox - radio);
                Canvas.SetBottom(circulo, centroy - radio);
                lienzo.Children.Add(circulo);

                i = i + 1;

                while (i < 17)
                {
                    if (totales[i] != 0)
                    {
                        angulo = angulo - 360 * totales[i] / total;
                        PathFigure figuraPath = new PathFigure();
                        figuraPath.StartPoint = new Point(centrox, centroy);

                        LineSegment linea1 = new LineSegment();
                        linea1.Point = new Point(centrox, centroy - radio);

                        //punto final arco angulo
                        double coordX = centrox + radio * Math.Sin(angulo * Math.PI / 180);
                        double coordY = centroy + radio * -Math.Cos(angulo * Math.PI / 180);
                        sigCoordenadas[0] = coordX;
                        sigCoordenadas[1] = coordY;
                        Point puntoFin = new Point(coordX, coordY);

                        ArcSegment arco = new ArcSegment(puntoFin, new Size(radio, radio), 0, angulo >= 180, SweepDirection.Clockwise, true);

                        LineSegment linea2 = new LineSegment();
                        linea2.Point = new Point(centrox, centroy);

                        PathSegmentCollection collecionPath = new PathSegmentCollection();
                        collecionPath.Add(linea1);
                        collecionPath.Add(arco);
                        collecionPath.Add(linea2);

                        figuraPath.Segments = collecionPath;

                        PathFigureCollection collecionFigurasPath = new PathFigureCollection();
                        collecionFigurasPath.Add(figuraPath);

                        PathGeometry GeometriaPath = new PathGeometry();
                        GeometriaPath.Figures = collecionFigurasPath;

                        Path path = new Path();
                        path.Stroke = Brushes.Black;
                        path.StrokeThickness = 1;
                        path.Data = GeometriaPath;

                        flag = false;
                        for (j = i + 1; j < 17 && !flag; j++)
                        {
                            if (totales[j] != 0)
                            {
                                sig = j;
                                flag = true;
                            }
                        }
                        if (flag)
                        {
                            path.Fill = colores[sig];
                            path.ToolTip = totales[sig] * 100 / total;
                        }
                        else
                        {
                            for (j = 0; j < i && !flag; j++)
                            {
                                if (totales[j] != 0)
                                {
                                    sig = j;
                                    flag = true;
                                }
                            }
                            path.Fill = colores[sig];
                            path.ToolTip = totales[sig] * 100 / total;
                        }

                        lienzo.Children.Add(path);
                    }
                    i++;
                }
            }
        }

        private void botonGraficoRepostajes_Click(object sender, RoutedEventArgs e)
        {
            graficarLineas(this, new SeleccionandoEventArgs(vehiculoSeleccionado));
        }

        private void botonGraficoServicios_Click(object sender, RoutedEventArgs e)
        {
            dibujarSectoresServicios(vehiculoSeleccionado);
        }
    }
}
