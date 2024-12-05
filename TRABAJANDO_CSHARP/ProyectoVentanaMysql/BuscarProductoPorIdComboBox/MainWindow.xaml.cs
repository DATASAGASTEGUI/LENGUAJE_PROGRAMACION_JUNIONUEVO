using MySql.Data.MySqlClient;
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
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace ProyectoVentanaMysql.BuscarProductoPorIdComboBox
{
    /// <summary>
    /// Lógica de interacción para MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            CargarIdsComboBox();
            cmbIdProducto.SelectedIndex = 0;
        }

        public static MySqlConnection ObtenerConexion()
        {
            string conexionUrl = "Server=localhost;Database=ferreteria;Uid=root;Pwd=12345678;Port=3307";
            MySqlConnection conexion = new MySqlConnection(conexionUrl);
            try
            {
                conexion.Open();
                return conexion;
            }
            catch (MySqlException ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
        }

        public void CargarIdsComboBox()
        {
            using (MySqlConnection conexion = ObtenerConexion())
            {
                if (conexion != null)
                {
                    try
                    {
                        string query = "SELECT id_producto FROM Producto";
                        MySqlCommand cmd = new MySqlCommand(query, conexion);
                        MySqlDataReader reader = cmd.ExecuteReader();
                        cmbIdProducto.Items.Add("Seleccione Aquí");
                        while (reader.Read())
                        {
                            string? id_producto = reader["id_producto"].ToString();
                            cmbIdProducto.Items.Add(id_producto);
                        }
                    }
                    catch (MySqlException ex)
                    {
                        MessageBox.Show($"Query Select {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
                else
                {
                    MessageBox.Show("Conexion", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }




        private void CmbIdProducto_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            string idProducto = cmbIdProducto.SelectedIndex.ToString();
            txtNombreProducto.Clear();
            txtDescripcion.Clear();
            txtPrecio.Clear();
            txtStock.Clear();
            txtCategoria.Clear();
            if (idProducto != "Seleccione Aquí")
            {
                MostrarInformacionProducto(int.Parse(idProducto));
            }
        }

        public void MostrarInformacionProducto(int id_producto_buscar)
        {
            using (MySqlConnection conexion = ObtenerConexion())
            {
                if (conexion != null)
                {
                    try
                    {
                        string query = "SELECT * FROM Producto WHERE id_producto = @id_producto_p";
                        MySqlCommand cmd = new MySqlCommand(query, conexion);
                        cmd.Parameters.AddWithValue("@id_producto_p", id_producto_buscar);
                        MySqlDataReader reader = cmd.ExecuteReader();
                        if (reader.Read())
                        {
                            string? nombre = reader["nombre"].ToString();
                            string? descripcion = reader["descripcion"].ToString();
                            string? precio = reader["precio"].ToString();
                            string? stock = reader["stock"].ToString();
                            string? categoria = reader["categoria"].ToString();

                            txtNombreProducto.Text = nombre;
                            txtDescripcion.Text = descripcion;
                            txtPrecio.Text = precio;
                            txtStock.Text = stock;
                            txtCategoria.Text = categoria;
                        }
                        else
                        {
                            Console.WriteLine($"ID {id_producto_buscar} NO EXISTE");
                        }
                        reader.Close();
                    }
                    catch (MySqlException ex)
                    {
                        Console.WriteLine($"ERROR: QUERY {ex.Message}");
                    }
                }
                else
                {
                    Console.WriteLine("ERROR: CONEXION");
                }
            }
        }

        private void BtnLimpiar_Click(object sender, RoutedEventArgs e)
        {
            cmbIdProducto.SelectedIndex = 0;
            txtNombreProducto.Clear();
            txtDescripcion.Clear();
            txtPrecio.Clear();
            txtStock.Clear();
            txtCategoria.Clear();
        }

        private void BtnSalir_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
