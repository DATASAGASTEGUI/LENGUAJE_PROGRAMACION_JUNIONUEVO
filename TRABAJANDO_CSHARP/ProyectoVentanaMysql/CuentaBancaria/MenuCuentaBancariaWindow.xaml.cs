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

namespace ProyectoVentanaMysql.CuentaBancaria
{
    /// <summary>
    /// Lógica de interacción para MenuCuentaBancariaWindow.xaml
    /// </summary>
    public partial class MenuCuentaBancariaWindow : Window
    {
        public MenuCuentaBancariaWindow()
        {
            InitializeComponent();
        }

        private void BtnCrearCuentaBancaria_Click(object sender, RoutedEventArgs e)
        {
            CrearCuentaBancariaWindow ventana = new CrearCuentaBancariaWindow();
            ventana.Show();
        }

        private void BtnBuscarCuentaBancaria_Click(object sender, RoutedEventArgs e)
        {
            BuscarCuentaBancariaWindow ventana = new BuscarCuentaBancariaWindow();
            ventana.Show();
        }

        private void BtnIngresarDineroCuentaBancaria_Click(object sender, RoutedEventArgs e)
        {
            IngresarDineroCuentaBancariaWindow ventana = new IngresarDineroCuentaBancariaWindow();
            ventana.Show();
        }

        private void BtnRetirarDineroCuentaBancaria_Click(object sender, RoutedEventArgs e)
        {
            RetirarDineroCuentaBancariaWindow ventana = new RetirarDineroCuentaBancariaWindow();
            ventana.Show();
        }

        private void BtnSalir_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
