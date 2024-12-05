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
    /// Lógica de interacción para CrearCuentaBancariaWindow.xaml
    /// </summary>
    public partial class CrearCuentaBancariaWindow : Window
    {
        public CrearCuentaBancariaWindow()
        {
            InitializeComponent();
        }

        private void BtnCrear_Click(object sender, RoutedEventArgs e)
        {
            string titular = TxtTitular.Text;

            bool esDouble = double.TryParse(TxtSaldo.Text, out double saldo);  // Usamos TryParse directamente con TxtSaldo.Text
            bool estaLleno = titular.Length > 0;
            bool esPositivo = saldo >= 0;

            if (!(estaLleno && esDouble && esPositivo))  // Validamos que el saldo sea un número y positivo
            {
                MessageBox.Show($"Dantos de entrada incorrectos", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            // Creamos la cuenta bancaria
            CuentaBancaria cuenta = new CuentaBancaria(titular, saldo);
            string mensaje = cuenta.CrearCuentaBancaria();
            lblMensajes.Text = mensaje;
        }

        private void BtnNuevo_Click(object sender, RoutedEventArgs e)
        {
            TxtSaldo.Clear();
            TxtTitular.Clear();
            lblMensajes.Text = "MENSAJES";
        }
    }
}
