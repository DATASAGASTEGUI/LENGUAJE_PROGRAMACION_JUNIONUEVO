using System;
using System.Collections.Generic;
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

namespace ProyectoVentanaMysql.ProyectoFerreteria
{
    /// <summary>
    /// Lógica de interacción para BuscarVentaPorFechaHoraWindow.xaml
    /// </summary>
    public partial class BuscarVentaPorFechaHoraWindow : Window
    {
        public static DataGrid dg;
        public BuscarVentaPorFechaHoraWindow()
        {
            InitializeComponent();
            VentaDataGrid.Language = System.Windows.Markup.XmlLanguage.GetLanguage("es-ES");
            dg = VentaDataGrid;
            VentanaOpcionesWindow ventana = new VentanaOpcionesWindow(dg);
            ventana.Show();
        }

        private void BtnProductosVendidos_Click(object sender, RoutedEventArgs e)
        {
            if (VentaDataGrid.SelectedItem is Venta2 ventaSeleccionado)
            {
                int idVenta = ventaSeleccionado.IdVenta;
                List<Venta> productosvendidos = MetodosCrud.ObtenerListaProductosVendidos2(idVenta);
                DetalleVentaDataGrid.ItemsSource = productosvendidos;
            }
        }
        private void BtnBuscarVentaPorFecha_Click(object sender, RoutedEventArgs e)
        {
            if (DatePickerFecha.SelectedDate == null)
            {
                MessageBox.Show("Por favor, seleccione una fecha.", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            DateTime fechaSeleccionada = DatePickerFecha.SelectedDate.Value;  //16/12/2024 0:00:00

            List<Venta2> ventas_lo = MetodosCrud.BuscarVentasPorFecha(fechaSeleccionada);
            VentaDataGrid.ItemsSource = ventas_lo;
        }

        private void BtnBuscarVentaPorFechaHora_Click(object sender, RoutedEventArgs e)
        {
            if (DatePickerFecha.SelectedDate == null)
            {
                MessageBox.Show("Por favor, seleccione una fecha.", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            if (TimePickerHora.Value == null)
            {
                MessageBox.Show("Por favor, seleccione una hora.", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            DateTime fechaSeleccionada = DatePickerFecha.SelectedDate.Value;  //16/12/2024 0:00:00

            TimeSpan horaSeleccionada = TimePickerHora.Value.Value.TimeOfDay; //23:45:30

            DateTime fechaHoraCompleta = fechaSeleccionada.Date + horaSeleccionada; //16/12/2024 23:45:30

            List<Venta2> ventas_lo = MetodosCrud.BuscarVentasPorFechaHora(fechaHoraCompleta);
            VentaDataGrid.ItemsSource = ventas_lo;
        }
    }

    public class Venta2 
    {
       public int IdVenta {  get; set; }
       public DateTime Fecha { get; set; }
       public double Total { get; set; }
    }
}
