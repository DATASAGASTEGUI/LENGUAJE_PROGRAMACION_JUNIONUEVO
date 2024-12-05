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
    /// Lógica de interacción para GestionProductosWindow.xaml
    /// </summary>
    public partial class GestionProductosWindow : Window
    {
        int idProducto = 0;
        public GestionProductosWindow()
        {
            InitializeComponent();
            CargarDatosDataGrid();
            CargarDatosComboBox();
        }

        private void CargarDatosDataGrid()
        {
            List<Producto> productos_l = MetodosCrud.ObtenerListaProductos();
            ProductosDataGrid.ItemsSource = productos_l;
        }

        private void CargarDatosComboBox()
        {
            CmbCategoria.ItemsSource = MetodosCrud.ObtenerListaCategorias();
        }

        private void BtnAgregar_Click(object sender, RoutedEventArgs e)
        {
            string? nombre;
            string? descripcion;
            double precio;
            int stock;
            string? categoria;

            //VALIDAR CAMPOS VACIOS
            if(string.IsNullOrWhiteSpace(TxtNombre.Text.Trim()) ||
               string.IsNullOrWhiteSpace(TxtDescripcion.Text.Trim()) ||
               string.IsNullOrWhiteSpace(TxtPrecio.Text.Trim()) ||
               string.IsNullOrWhiteSpace(TxtStock.Text.Trim()) ||
               string.IsNullOrWhiteSpace(TxtCategoria.Text.Trim())
               )
            {
                MessageBox.Show("Por favor, llena todos los campos obligatorios.", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            //VALIDAR FORMATO DE LOS CAMPOS
            try
            {
                nombre = TxtNombre.Text;
                descripcion = TxtDescripcion.Text;
                precio = Convert.ToDouble(TxtPrecio.Text);
                stock = Convert.ToInt32(TxtStock.Text);
                categoria = TxtCategoria.Text;
            }
            catch(FormatException ex)
            {
                MessageBox.Show($"Campos con formato incorrecto {ex.Message}", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            Producto producto = new Producto(nombre,descripcion,precio,
                                             stock,categoria);
            MetodosCrud.AgregarProducto(producto);
            BtnNuevo_Click(null, null);
            CargarDatosDataGrid();
            CargarDatosComboBox();
        }

        private void BtnEditar_Click(object sender, RoutedEventArgs e)
        {
            if (!(TxtNombre.Text.Length > 0 &&
               TxtDescripcion.Text.Length > 0 &&
               TxtPrecio.Text.Length > 0 &&
               TxtStock.Text.Length > 0 &&
               TxtCategoria.Text.Length > 0))
            {
                MessageBox.Show("Por favor, llene todos los campos", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            if(!(MetodosCrud.EsDouble(TxtPrecio.Text.Trim()) &&
                MetodosCrud.ValidarNumeroConComa(TxtPrecio.Text.Trim())
                ))
            {
                MessageBox.Show("Por favor, utilice la coma para el precio", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            try
            {
                string nombre = TxtNombre.Text.Trim();
                string descripcion = TxtDescripcion.Text.Trim();
                double precio = Convert.ToDouble(TxtPrecio.Text.Trim());
                int stock = Convert.ToInt32((TxtStock.Text.Trim()));
                string categoria = TxtCategoria.Text.Trim();
                Producto producto = new Producto(idProducto,
                                                 nombre,
                                                 descripcion,
                                                 precio,
                                                 stock,
                                                 categoria);
                MetodosCrud.ActualizarProducto(producto);
                CargarDatosDataGrid();
                CargarDatosComboBox();
                BtnNuevo_Click(null, null);
            }
            catch (FormatException ex)
            {
                MessageBox.Show("Campos no cumplen con el formato", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }


        }

        private void BtnEliminar_Click(object sender, RoutedEventArgs e)
        {
            if (ProductosDataGrid.SelectedItem is Producto productoSeleccionado)
            {
                MetodosCrud.EliminarProducto(productoSeleccionado.IdProducto);
            }
            else
            {
                MessageBox.Show("Debe seleccionar primero un producto para eliminar", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
            BtnNuevo_Click(null, null);
            CargarDatosDataGrid();
        }

        private void BtnNuevo_Click(object sender, RoutedEventArgs e)
        {
            TxtNombre.Clear();
            TxtDescripcion.Clear();
            TxtPrecio.Clear();
            TxtStock.Clear();
            TxtCategoria.Clear();
            CmbCategoria.SelectedIndex = 0;

            CargarDatosDataGrid();
            CargarDatosComboBox();
        }

        private void CmbCategoria_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            TxtCategoria.Text = CmbCategoria.SelectedItem.ToString();
        }

        private void ProductosDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (ProductosDataGrid.SelectedItem is Producto productoSeleccionado)
            {
                idProducto = productoSeleccionado.IdProducto;
                TxtNombre.Text = productoSeleccionado.Nombre;
                TxtDescripcion.Text = productoSeleccionado.Descripcion;
                TxtPrecio.Text = productoSeleccionado.Precio.ToString();
                TxtStock.Text = productoSeleccionado.Stock.ToString();
                TxtCategoria.Text = productoSeleccionado.Categoria;
            }

        }
    }
}
