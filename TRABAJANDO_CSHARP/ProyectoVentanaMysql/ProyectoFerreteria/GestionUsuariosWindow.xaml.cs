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
    /// Lógica de interacción para GestionUsuariosWindow.xaml
    /// </summary>
    public partial class GestionUsuariosWindow : Window
    {
        string idUsuario;
        string contrasenaOriginal;
        public GestionUsuariosWindow()
        {
            InitializeComponent();
            CargarDatosDataGrid();
        }

        private void CargarDatosDataGrid()
        {
            List<Usuario> usuarios_l = MetodosCrud.ObtenerListaUsuarios();
            UsuariosDataGrid.ItemsSource = usuarios_l;
        }

        private void BtnAgregar_Click(object sender, RoutedEventArgs e)
        {
            string nombreUsuario = TxtNombreUsuario.Text;
            string contrasena = TxtContrasena.Text;
            string rol = TxtRol.Text;

            if (nombreUsuario.Length > 0 && contrasena.Length > 0 && rol.Length > 0)
            {
                if (rol == "Administrador" || rol == "Cajero" || rol == "Almacén")
                {
                    Usuario usuario = new Usuario("0", TxtNombreUsuario.Text,
                                                  BCrypt.Net.BCrypt.HashPassword(TxtContrasena.Text), TxtRol.Text);

                    MetodosCrud.AgregarUsario(usuario);
                    CargarDatosDataGrid();
                }
                else
                {
                    MessageBox.Show("Rol Incorrecto", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
            }else
            {
                MessageBox.Show("Tiene campos vacios", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private void BtnEditar_Click(object sender, RoutedEventArgs e)
        {
           string nombreUsuario = TxtNombreUsuario.Text.Trim();
           string contrasenaNuevo = TxtContrasena.Text.Trim();
           string rol = TxtRol.Text.Trim();

           Usuario usuario = new Usuario(idUsuario,nombreUsuario, contrasenaNuevo, rol);


            if (contrasenaNuevo == contrasenaOriginal)
            {
                MetodosCrud.ActualizarUsuario(usuario,0);//No encriptar
            }
            else
            {
                MetodosCrud.ActualizarUsuario(usuario, 1);//Si encriptar
            }
            BtnNuevo_Click(null, null);
            CargarDatosDataGrid();
        }

        private void BtnEliminar_Click(object sender, RoutedEventArgs e)
        {
            if (UsuariosDataGrid.SelectedItem is Usuario usuarioSeleccionado)
            {
                MetodosCrud.EliminarUsuario(int.Parse(usuarioSeleccionado.IdUsuario));
            }
            else
            {
                MessageBox.Show("Debe seleccionar primero un usuario para eliminar", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
            BtnNuevo_Click(null, null);
            CargarDatosDataGrid();
        }

        private void BtnNuevo_Click(object sender, RoutedEventArgs e)
        {
            TxtNombreUsuario.Clear();
            TxtContrasena.Clear();
            TxtRol.Clear();
            CargarDatosDataGrid();
        }

        private void UsuariosDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (UsuariosDataGrid.SelectedItem is Usuario usuarioSeleccionado)
            {
                idUsuario = usuarioSeleccionado.IdUsuario;
                TxtNombreUsuario.Text = usuarioSeleccionado.NombreUsuario;
                TxtContrasena.Text = usuarioSeleccionado.Contrasena;
                TxtRol.Text = usuarioSeleccionado.Rol;
                contrasenaOriginal = usuarioSeleccionado.Contrasena;
            }
        }
    }
}
