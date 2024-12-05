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
    /// Lógica de interacción para BuscarVentaPorIdWindow.xaml
    /// </summary>
    public partial class BuscarVentaPorIdWindow : Window
    {
        public BuscarVentaPorIdWindow()
        {
            InitializeComponent();
            CargarComboBox();
        }

        private void CargarComboBox()
        {
            CmbIdsVenta.ItemsSource = MetodosCrud.ObtenerListaIdsVenta();
        }

        private void BtnBuscarVenta_Click(object sender, RoutedEventArgs e)
        {
            if(CmbIdsVenta.SelectedItem == null)
            {
                MessageBox.Show("Por favor, seleccione un id venta a buscar", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            int idVenta = Convert.ToInt32(CmbIdsVenta.SelectedItem);

            dynamic objeto = MetodosCrud.ObtenerVentaPorId(idVenta);
            
            TxtFechaHoraVenta.Text = objeto.FechaHoraVenta+"";
            TxtTotalVenta.Text = objeto.Total+"";

            // Actualiza la vista del DataGrid
            CarritoDataGrid.ItemsSource = null;
            CarritoDataGrid.ItemsSource = MetodosCrud.ObtenerListaProductosVendidos2(idVenta);
            //MessageBox.Show($"{idVenta}", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
        }

        private void BtnLimpiar_Click(object sender, RoutedEventArgs e)
        {
            CarritoDataGrid.ItemsSource = null;
            TxtFechaHoraVenta.Clear();
            TxtTotalVenta.Clear();
            CmbIdsVenta.SelectedIndex = -1;
            CargarComboBox();
            
        }
    }
}
