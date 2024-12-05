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
    /// Lógica de interacción para VentaWindow.xaml
    /// </summary>
    public partial class VentaWindow : Window
    {
        Dictionary<string, Producto> productosdisponibles_ld;
        List<Venta> carrito_lo = new List<Venta>();
        public VentaWindow()
        {
            InitializeComponent();
            productosdisponibles_ld = MetodosCrud.ObtenerDiccionarioProductosDisponibles();
            CargarDatosComboBox();
        }

        private void CargarDatosComboBox()
        {
            CmbProductos.ItemsSource = productosdisponibles_ld.Keys;
        }

        private void BtnAgregarCarrito_Click(object sender, RoutedEventArgs e)
        {
            if (CmbProductos.SelectedItem == null)
            {
                MessageBox.Show("Por favor, Debe seleccionar un producto", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            int cantidad = Convert.ToInt32(SpnCantidad.Value.ToString());
            if (cantidad == 0)
            {
                MessageBox.Show("Por favor, Debe seleccionar una cantidad mayor a cero", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }


            string clave = CmbProductos.SelectedItem.ToString();
            //MessageBox.Show($"{clave}", "Information", MessageBoxButton.OK, MessageBoxImage.Information);

            Producto producto = productosdisponibles_ld[clave];

            if(cantidad > producto.Stock)
            {
                MessageBox.Show("Cantidad excede al stock disponible", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            int idProducto = producto.IdProducto;
            string? nombreProducto = producto.Nombre;
            double precio = producto.Precio;

            Venta venta = new Venta()
            {
                IdProducto = idProducto,
                NombreProducto = nombreProducto,
                Cantidad = cantidad,
                PrecioUnitario = precio
            };

            carrito_lo.Add(venta);

            double total = 0;
            foreach (Venta v in carrito_lo)
            {
                total = total + v.Total; 
            }
            lblTotal.Content = $"Total €  {total:N2}";
            //ACTUALIZAR EL DATAGRID CON EL EL CARRITO_LO
            CarritoDataGrid.ItemsSource = null; // Limpiar 
            CarritoDataGrid.ItemsSource = carrito_lo; // LLenar

            CmbProductos.SelectedIndex = -1;
            SpnCantidad.Text = "0";
        }

        private void BtnLimpiarCarrito_Click(object sender, RoutedEventArgs e)
        {
            CmbProductos.SelectedIndex = -1;
            carrito_lo.Clear();
            CarritoDataGrid.ItemsSource = null;
            SpnCantidad.Text = "0";
            lblTotal.Content = "Total: \u20AC  0.00";
        }

        private void BtnRealizarVenta_Click(object sender, RoutedEventArgs e)
        {
            MetodosCrud.RealizarVenta(carrito_lo);
            BtnLimpiarCarrito_Click(null, null);
            productosdisponibles_ld = MetodosCrud.ObtenerDiccionarioProductosDisponibles();
            CargarDatosComboBox();
        }

        private void BtnEliminarSeleccion_Click(object sender, RoutedEventArgs e)
        {
            // Verifica si hay una fila seleccionada
            if (CarritoDataGrid.SelectedItem is Venta ventaSeleccionada)
            {
                MessageBoxResult resultado = MessageBox.Show(
                "Confirme, si desea eliminar el producto del carrito.",
                "Opciones",
                MessageBoxButton.YesNo,
                MessageBoxImage.Question);

                switch (resultado)
                {
                    case MessageBoxResult.Yes:
                        // Elimina el elemento seleccionado del carrito
                        carrito_lo.Remove(ventaSeleccionada);

                        // Actualiza la vista del DataGrid
                        CarritoDataGrid.ItemsSource = null;
                        CarritoDataGrid.ItemsSource = carrito_lo;

                        double total = 0;
                        foreach (Venta venta in carrito_lo)
                        {
                            total += venta.Total;
                        }
                        //lblTotal.Text = $"Total: {total:C}";
                        lblTotal.Content = $"Total: € {total:N2}";
                        
                        MessageBox.Show("Producto eliminado del carrito.", "Información", MessageBoxButton.OK, MessageBoxImage.Information);
                        
                        break;
                    case MessageBoxResult.No:
                         break;
                }
             }
            else
            {
                MessageBox.Show("Seleccione un producto para eliminar.", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
            }


        }
    }
}
