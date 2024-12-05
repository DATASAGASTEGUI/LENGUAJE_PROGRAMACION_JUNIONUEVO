using ProyectoVentanaMysql.GestionUsuarios;
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

namespace ProyectoVentanaMysql
{
    /// <summary>
    /// Lógica de interacción para MenuWindow.xaml
    /// </summary>
    public partial class MenuWindow : Window
    {
        public MenuWindow()
        {
            InitializeComponent();
        }

        private void BtnBuscar_Click(object sender, RoutedEventArgs e)
        {
            ProyectoVentanaMysql.BuscarProductoPorIdComboBox.MainWindow vbuscar = new ProyectoVentanaMysql.BuscarProductoPorIdComboBox.MainWindow();
            vbuscar.Show();
        }

        private void BtnMostrar_Click(object sender, RoutedEventArgs e)
        {
            ProyectoVentanaMysql.MostrarTodosProductosDataGrid.MainWindow vmostrar = new MostrarTodosProductosDataGrid.MainWindow();
            vmostrar.Show();
        }

        private void BtnSalir_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void BtnBuscarIdTextBox_Click(object sender, RoutedEventArgs e)
        {
            MainWindow vbuscar1 = new MainWindow();
            vbuscar1.Show();
        }

        private void BtnAgregarUsuario_Click(object sender, RoutedEventArgs e)
        {
            WindowGestionUsuarios vusuario = new WindowGestionUsuarios();
            vusuario.Show();
        }
    }
}
