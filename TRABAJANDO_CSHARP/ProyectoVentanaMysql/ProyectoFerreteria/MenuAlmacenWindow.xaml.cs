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
    /// Lógica de interacción para MenuAlmacenWindow.xaml
    /// </summary>
    public partial class MenuAlmacenWindow : Window
    {
        LoginWindow ventanaLoginWindow;
        public MenuAlmacenWindow(LoginWindow loginWindow)
        {
            InitializeComponent();
            this.ventanaLoginWindow = loginWindow;
        }

        private void BtnSalir_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
            this.ventanaLoginWindow.Show();
        }

        private void BtnInventario_Click(object sender, RoutedEventArgs e)
        {
            InventarioWindow ventana = new InventarioWindow();
            ventana.Show();
        }
    }
}
