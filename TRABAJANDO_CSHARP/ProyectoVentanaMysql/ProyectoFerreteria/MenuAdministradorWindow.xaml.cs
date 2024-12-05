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
    /// Lógica de interacción para MenuAdministradorWindow.xaml
    /// </summary>
    public partial class MenuAdministradorWindow : Window
    {
        LoginWindow ventanaLoginWindow;
        public MenuAdministradorWindow(LoginWindow loginWindow)
        {
            InitializeComponent();
            ventanaLoginWindow = loginWindow;
        }

        private void BtnGestionUsuaios_Click(object sender, RoutedEventArgs e)
        {
            GestionUsuariosWindow ventana = new GestionUsuariosWindow();
            ventana.Show();
        }

        private void BtnGestionProductos_Click(object sender, RoutedEventArgs e)
        {
            GestionProductosWindow ventana = new GestionProductosWindow();
            ventana.Show();
        }

        private void BtnInventario_Click(object sender, RoutedEventArgs e)
        {
            InventarioWindow ventana = new InventarioWindow();
            ventana.Show();
        }

        private void BtnVenta_Click(object sender, RoutedEventArgs e)
        {
            VentaWindow ventana = new VentaWindow();
            ventana.Show();
        }

        private void BtnSalir_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
            this.ventanaLoginWindow.Show();
        }
    }
}
