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
    /// Lógica de interacción para BuscarCuentaBancariaWindow.xaml
    /// </summary>
    public partial class BuscarCuentaBancariaWindow : Window
    {
        public BuscarCuentaBancariaWindow()
        {
            InitializeComponent();
        }

        private void BtnBuscar_Click(object sender, RoutedEventArgs e)
        {
            string id = TxtIdCuentaBancaria.Text;

            // Validar que el ID es un número entero
            if (!int.TryParse(id, out int numeroInt))
            {
                MessageBox.Show($"El ID {id} es incorrecto", "Advertencia", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            // Llamar a BuscarCuentaBancaria y manejar los resultados
            var resultados_do = CuentaBancaria.BuscarCuentaBancaria(numeroInt);

            // Verificar si existe la clave "resultado1" y si contiene una cuenta válida
            if (!resultados_do.ContainsKey("resultado1") || resultados_do["resultado1"] == null)
            {
                MessageBox.Show("No se encontró la cuenta bancaria.", "Advertencia", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            // Convertir a CuentaBancaria y mostrar en el DataGrid
            var cuenta = resultados_do["resultado1"] as CuentaBancaria;
            if (cuenta != null)
            {
                List<CuentaBancaria> cuentaLista = new List<CuentaBancaria> { cuenta };
                CuentaBancariaDataGrid.ItemsSource = cuentaLista; // Asignar lista con una sola cuenta
            }

            // Manejar mensajes adicionales si existen
            if (resultados_do.ContainsKey("resultado2") && resultados_do["resultado2"] is Mensaje mensaje)
            {
                lblMensajes.Text = mensaje.mensaje;
            }
            else
            {
                lblMensajes.Text = "Operación completada sin mensajes adicionales.";
            }
        }


        /*
        private void BtnBuscar_Click(object sender, RoutedEventArgs e)
        {
            string id = TxtIdCuentaBancaria.Text;

            bool esInt = int.TryParse(id, out int numeroInt);  // Usamos int.TryParse para validar que el ID es un número entero

            if (!esInt)
            {
                MessageBox.Show($"El ID {id} es Incorrecto", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            // Llamamos a BuscarCuentaBancaria que devuelve un solo objeto CuentaBancaria
            Dictionary<string, Object> resultados_do = CuentaBancaria.BuscarCuentaBancaria(numeroInt);

            if (((CuentaBancaria)resultados_do["resultado1"]) == null)
            {
                MessageBox.Show("No se encontró la cuenta", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            // Si se encontró la cuenta, mostramos el resultado en el DataGrid
            List<CuentaBancaria> cuenta_l = new List<CuentaBancaria> { (CuentaBancaria)resultados_do["resultado1"] };  // Convertimos la cuenta en una lista
            CuentaBancariaDataGrid.ItemsSource = cuenta_l;  // Mostramos la lista (con solo una cuenta) en el DataGrid

            lblMensajes.Text = ((Mensaje)resultados_do["resultado2"]).mensaje;
        }
        */

        private void BtnLimpiar_Click(object sender, RoutedEventArgs e)
        {
            TxtIdCuentaBancaria.Clear();
            CuentaBancariaDataGrid.ItemsSource = null;
            lblMensajes.Text = "MENSAJES";
        }
    }
}
