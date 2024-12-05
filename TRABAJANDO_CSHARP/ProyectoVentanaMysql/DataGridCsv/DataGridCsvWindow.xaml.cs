using System;
using System.Collections.Generic;
using System.IO;
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

namespace ProyectoVentanaMysql.DataGridCsv
{
    /// <summary>
    /// Lógica de interacción para DataGridCsvWindow.xaml
    /// </summary>
    public partial class DataGridCsvWindow : Window
    {
        public DataGridCsvWindow()
        {
            InitializeComponent();
            CargarDataGrid();
        }

        public void CargarDataGrid()
        {
            String rutaAbsoluta = @"C:/LENGUAJE_PROGRAMACION/TRABAJANDO_CSHARP/producto.csv";
            if (File.Exists(rutaAbsoluta))
            {
                List<dynamic> productos_lo = new List<dynamic>();
                using (var reader = new StreamReader(rutaAbsoluta))
                {
                    string cabecera = reader.ReadLine();
                    if (cabecera == null || string.IsNullOrEmpty(cabecera))
                    {
                        MessageBox.Show($"Archivo corrumpido", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                        return;
                    }
                    string? linea = "";
                    while((linea = reader.ReadLine()) != null)
                    { 
                        linea = linea.Trim(); 
                        var campos = linea.Split(';');
                        if (campos.Length != 6)
                        {
                            MessageBox.Show("Filas no cumplen con los 6 campos", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                            return;
                        }
                        else
                        {
                            var objeto = new
                            {
                                id_producto = campos[0],
                                nombre = campos[1],
                                descripcion = campos[2],
                                precio = campos[3],
                                stock = campos[4],
                                categoria = campos[5]
                            };
                            productos_lo.Add(objeto);
                        }
                    }
                    ProductosDataGrid.ItemsSource = productos_lo;
                }
            }
            else
            {
                MessageBox.Show($"No se encontró el archivo {rutaAbsoluta}.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

    }


}
