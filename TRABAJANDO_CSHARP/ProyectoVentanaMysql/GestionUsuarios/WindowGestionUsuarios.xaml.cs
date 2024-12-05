using MySql.Data.MySqlClient;
using Org.BouncyCastle.Crypto.Generators;
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
using BCrypt.Net;

namespace ProyectoVentanaMysql.GestionUsuarios
{
    /// <summary>
    /// Lógica de interacción para WindowGestionUsuarios.xaml
    /// </summary>
    public partial class WindowGestionUsuarios : Window
    {
        public WindowGestionUsuarios()
        {
            InitializeComponent();
            cmbRol.Items.Add("");
            cmbRol.Items.Add("Administrador");
            cmbRol.Items.Add("Almacén");
            cmbRol.Items.Add("Cajero");
            cmbRol.SelectedIndex = 0;
        }

        private void BtnAgregar_Click(object sender, RoutedEventArgs e)
        {
            Usuario usuario = new Usuario
            {
                Nombre = txtNombreUsuario.Text,
                Contrasena = BCrypt.Net.BCrypt.HashPassword(txtClave.Text),
                Rol = cmbRol.SelectedIndex.ToString()
            };
            Insert(usuario);
        }

        private void BtnNuevo_Click(object sender, RoutedEventArgs e)
        {
            txtNombreUsuario.Clear();
            txtClave.Clear();
            cmbRol.SelectedIndex = 0;
        }

        private void BtnSalir_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void CmbRol_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

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

        public static void Insert(Usuario usuario)
        {
            using (MySqlConnection conexion = ObtenerConexion())
            {
                if (conexion != null)
                {
                    try
                    {
                        string query = @"INSERT INTO Usuario(nombre_usuario, contrasena, rol)
                                         VALUES(@nombre_p, @contrasena_p, @rol_p)";
                        MySqlCommand cmd = new MySqlCommand(query, conexion);
                        cmd.Parameters.AddWithValue("@nombre_p", usuario.Nombre);
                        cmd.Parameters.AddWithValue("@contrasena_p", usuario.Contrasena);
                        cmd.Parameters.AddWithValue("@rol_p", usuario.Rol);
                        cmd.ExecuteNonQuery();
                        MessageBox.Show("Ok Query Insert", "Información", MessageBoxButton.OK, MessageBoxImage.Information);
                    }
                    catch (MySqlException ex)
                    {
                        MessageBox.Show($"Query {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
                else
                {
                    MessageBox.Show("Conexion", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }
    }

    public class Usuario
    {
       public string? Nombre { get; set; }
       public string? Contrasena { get; set; }
       public string? Rol { get; set; }
    }
}
