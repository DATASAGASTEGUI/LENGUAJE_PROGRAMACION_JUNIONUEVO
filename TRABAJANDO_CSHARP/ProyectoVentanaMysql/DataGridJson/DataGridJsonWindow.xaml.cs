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
using System.IO;
using System.Text.Json.Nodes;
using Newtonsoft.Json.Linq; //JArray

namespace ProyectoVentanaMysql.DataGridJson
{
    /// <summary>
    /// Lógica de interacción para DataGridJsonWindow.xaml
    /// </summary>
    public partial class DataGridJsonWindow : Window
    {
        public DataGridJsonWindow()
        {
            InitializeComponent();
            CargarDataGrid();
        }

        public void CargarDataGrid()
        {
            String rutaAbsoluta = @"C:/LENGUAJE_PROGRAMACION/TRABAJANDO_CSHARP/producto.json";
            if (File.Exists(rutaAbsoluta))
            {
                string cadenaJson = File.ReadAllText(rutaAbsoluta);
                JArray objetos = JArray.Parse(cadenaJson);
                List<dynamic> productos_lo = new List<dynamic>();
                foreach (var objeto in objetos)
                {
                    var objetoJson = new
                    {
                        id_producto = objeto["id_producto"],
                        nombre = objeto["nombre"],
                        descripcion = objeto["descripcion"],
                        precio = objeto["precio"],
                        stock = objeto["stock"],
                        categoria = objeto["categoria"]
                    };
                    productos_lo.Add(objetoJson);
                }
                ProductosDataGrid.ItemsSource = productos_lo;
            }
            else
            {
                MessageBox.Show($"No se encontró el archivo {rutaAbsoluta}.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
