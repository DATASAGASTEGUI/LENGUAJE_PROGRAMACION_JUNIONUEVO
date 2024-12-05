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
using MySql.Data.MySqlClient;

namespace ProyectoVentanaMysql.MostrarTodosUsuariosDataGrid
{
    /// <summary>
    /// Lógica de interacción para MostrarTodosUsuariosWindow.xaml
    /// </summary>
    public partial class MostrarTodosUsuariosWindow : Window
    {
        public MostrarTodosUsuariosWindow()
        {
            InitializeComponent();
        }
        private void BtnRecargar_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                CargarUsuariosDataGrid();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Query Select {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void BtnLimpiarTabla_Click(object sender, RoutedEventArgs e)
        {
            dgUsuarios.ItemsSource = null;
        }

        private void BtnSalir_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
        public static MySqlConnection ObtenerConexion()
        {
            string conexionUrl = "Server=localhost;Database=ferreteria;Uid=root;Pwd=12345678;Port=3306";
            MySqlConnection conexion = new MySqlConnection(conexionUrl);
            try
            {
                conexion.Open();
                return conexion;
            }
            catch (MySqlException ex)
            {
                return null;
            }
        }
        public void CargarUsuariosDataGrid()
        {
            using (MySqlConnection conexion = ObtenerConexion())
            {
                if (conexion != null)
                {
                    try
                    {
                        string query = "SELECT * FROM Usuario";
                        MySqlCommand cmd = new MySqlCommand(query, conexion);
                        MySqlDataReader reader = cmd.ExecuteReader();

                        List<Usuario> usuarios_l = new List<Usuario>();

                        while (reader.Read())
                        {
                            usuarios_l.Add(new Usuario
                            {
                                id_usuario = Convert.ToString(reader["id_usuario"]),
                                nombre = Convert.ToString(reader["nombre_usuario"]),
                                clave = Convert.ToString(reader["contrasena"]),
                                rol = Convert.ToString(reader["rol"]) 

                                /*
                                id_usuario = reader["id_usuario"] != DBNull.Value ? Convert.ToString(reader["id_usuario"]) : string.Empty,
                                nombre = reader["nombre_usuario"] != DBNull.Value ? Convert.ToString(reader["nombre_usuario"]) : string.Empty,
                                clave = reader["contrasena"] != DBNull.Value ? Convert.ToString(reader["contrasena"]) : string.Empty,
                                rol = reader["rol"] != DBNull.Value ? Convert.ToString(reader["rol"]) : string.Empty
                                */
                            });

                        }
                        dgUsuarios.ItemsSource = usuarios_l;

                    }
                    catch (MySqlException ex)
                    {
                        MessageBox.Show($"Query Select {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
                else
                {
                    MessageBox.Show("CONEXIÓN", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }
    }

    public class Usuario
    {

        public string id_usuario { get; set; }
        public string nombre { get; set; }
        public string clave { get; set; }
        public string rol { get; set; }

    }
}