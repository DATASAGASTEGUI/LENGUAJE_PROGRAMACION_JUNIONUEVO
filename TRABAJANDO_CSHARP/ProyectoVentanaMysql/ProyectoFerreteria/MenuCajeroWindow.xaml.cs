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
    /// Lógica de interacción para MenuCajeroWindow.xaml
    /// </summary>
    public partial class MenuCajeroWindow : Window
    {
        LoginWindow ventanaLoginWindow;
        public MenuCajeroWindow(LoginWindow loginWindow)
        {
            InitializeComponent();
            ventanaLoginWindow = loginWindow;
        }

        private void BtnVenta_Click(object sender, RoutedEventArgs e)
        {

        }

        private void BtnSalir_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
            this.ventanaLoginWindow.Show();
        }
    }
}
