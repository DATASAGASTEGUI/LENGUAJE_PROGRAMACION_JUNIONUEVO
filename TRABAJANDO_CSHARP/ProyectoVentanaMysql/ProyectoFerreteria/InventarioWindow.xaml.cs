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
    /// Lógica de interacción para InventarioWindow.xaml
    /// </summary>
    public partial class InventarioWindow : Window
    {
        int idProducto = 0;
        public InventarioWindow()
        {
            InitializeComponent();
            CargarDatosDataGrid();
        }

        private void CargarDatosDataGrid()
        {
            List<Producto> productos_l = MetodosCrud.ObtenerListaProductos();
            ProductosDataGrid.ItemsSource = productos_l;
        }

        private void BtnDisminuirStock_Click(object sender, RoutedEventArgs e)
        {
            int cantidad = Convert.ToInt32(SpnCantidad.Value);
            int stock = MetodosCrud.ObtenerStock(idProducto);
            if (cantidad <= stock)
            {
                MetodosCrud.ActualizarStock(idProducto, cantidad, '-');
                CargarDatosDataGrid();
            }
            else
            {
                MessageBox.Show($"Cantidad {cantidad} debe ser menor o igual al stock", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private void BtnAumentarStock_Click(object sender, RoutedEventArgs e)
        {
            int cantidad = Convert.ToInt32(SpnCantidad.Value);
            MetodosCrud.ActualizarStock(idProducto, cantidad, '+');
            CargarDatosDataGrid();
        }

        private void BtnActualizar_Click(object sender, RoutedEventArgs e)
        {
            CargarDatosDataGrid();
        }

        private void ProductosDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // Verifica si el elemento seleccionado es del tipo 'Productos'
            if (ProductosDataGrid.SelectedItem is Producto productoSeleccionado)
            {
                // Asigna el ID del producto al TextBox
                TxtIdProducto.Text = productoSeleccionado.IdProducto.ToString();
                idProducto = productoSeleccionado.IdProducto;
            }

        }
    }
}
