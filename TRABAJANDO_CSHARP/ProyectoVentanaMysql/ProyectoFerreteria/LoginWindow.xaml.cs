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
    /// Lógica de interacción para LoginWindow.xaml
    /// </summary>
    public partial class LoginWindow : Window
    {
        public LoginWindow()
        {
            InitializeComponent();
        }

        private void BtnLoguearse_Click(object sender, RoutedEventArgs e)
        {
            string nombreUsuario = TxtNombreUsuario.Text.Trim();
            string contrasenaIngresada = TxtContrasena.Password.Trim();

            Usuario usuario = MetodosCrud.BuscarUsuario(nombreUsuario);

            if(BCrypt.Net.BCrypt.Verify(contrasenaIngresada, usuario.Contrasena))
            {
                if(usuario.Rol == "Administrador")
                {
                    //LEVANTAR LA VENTANA MENUADMINISTRADOR
                    MenuAdministradorWindow ventana = new MenuAdministradorWindow(this);
                    ventana.Show();
                    LimpiarCajitas();
                    this.Hide();
                    MessageBox.Show($"Bienvenido {nombreUsuario} rol Administrador", "Information", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                if (usuario.Rol == "Almacén")
                {
                    //LEVANTAR LA VENTANA MENUALMACEN
                    MenuAlmacenWindow ventana = new MenuAlmacenWindow(this);
                    ventana.Show(); 
                    LimpiarCajitas();
                    MessageBox.Show($"Bienvenido {nombreUsuario} rol Almacén", "Information", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                if (usuario.Rol == "Cajero")
                {
                    //LEVANTAR LA VENTANA MENUCAJERO
                    MenuCajeroWindow ventana = new MenuCajeroWindow(this);
                    ventana.Show();
                    LimpiarCajitas();
                    MessageBox.Show($"Bienvenido {nombreUsuario} rol Cajero", "Information", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
            else
            {
                MessageBox.Show("Contraseña no valida", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        public void LimpiarCajitas()
        {
            TxtNombreUsuario.Clear();
            TxtContrasena.Clear();
        }
    }
}
