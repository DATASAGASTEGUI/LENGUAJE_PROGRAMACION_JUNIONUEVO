using ProyectoVentanaMysql.ProyectoFerreteria;
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
    /// Lógica de interacción para IngresarDineroCuentaBancariaWindow.xaml
    /// </summary>
    public partial class IngresarDineroCuentaBancariaWindow : Window
    {
        public IngresarDineroCuentaBancariaWindow()
        {
            InitializeComponent();
        }

        private void BtnIngresar_Click(object sender, RoutedEventArgs e)
        {
            string id = TxtIdCuentaBancaria.Text;

            bool esInt = int.TryParse(id, out int numeroInt);  // Usamos int.TryParse para validar que el ID es un número entero

            if (!esInt)
            {
                MessageBox.Show($"El ID {id} es Incorrecto", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            // Llamamos a BuscarCuentaBancaria que devuelve un solo objeto CuentaBancaria
            var resultados_do = CuentaBancaria.BuscarCuentaBancaria(numeroInt);

            // Verificar si existe la clave "resultado1" y si contiene una cuenta válida
            if (!resultados_do.ContainsKey("resultado1") || resultados_do["resultado1"] == null)
            {
                MessageBox.Show("No se encontró la cuenta bancaria.", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            // Si se encontró la cuenta, mostramos el resultado en el DataGrid
            //double dinero = Double.Parse(TxtDinero.Text);

            bool esDouble = double.TryParse(TxtDinero.Text, out double dinero);

            if(!esDouble) 
            {
                MessageBox.Show($"El Dinero {TxtDinero.Text} es Incorrecto", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            CuentaBancaria cuenta = resultados_do["resultado1"] as CuentaBancaria;

            // Llamamos al método para actualizar el saldo (en este caso, añadir dinero con el tipo '+')
            cuenta.ActualizarSaldo(numeroInt, dinero, '+');

            // Después de hacer el ingreso, volvemos a buscar la cuenta actualizada (para reflejar el cambio en el saldo)
            resultados_do = CuentaBancaria.BuscarCuentaBancaria(numeroInt);
            cuenta = resultados_do["resultado1"] as CuentaBancaria;
            // Convertimos la cuenta actualizada en una lista (porque el DataGrid espera una lista de objetos)
            List<CuentaBancaria> cuenta_l = new List<CuentaBancaria> { cuenta };

            // Actualizamos el DataGrid con la cuenta recién modificada
            CuentaBancariaDataGrid.ItemsSource = cuenta_l;

            // Mostramos un mensaje indicando que el ingreso fue exitoso
            //MessageBox.Show("Ingreso realizado correctamente", "Información", MessageBoxButton.OK, MessageBoxImage.Information);
            lblMensajes.Text = "Infomation: Ingreso realizado correctamente";
        }
    }
}
