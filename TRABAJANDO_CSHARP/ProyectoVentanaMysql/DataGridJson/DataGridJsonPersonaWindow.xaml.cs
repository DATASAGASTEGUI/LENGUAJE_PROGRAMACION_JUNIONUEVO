using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.IO;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Newtonsoft.Json.Linq;

namespace ProyectoVentanaMysql.DataGridJson
{
    /// <summary>
    /// Lógica de interacción para DataGridJsonPersonaWindow.xaml
    /// </summary>
    public partial class DataGridJsonPersonaWindow : Window
    {
        public DataGridJsonPersonaWindow()
        {
            InitializeComponent();
            CargarDataGrid();
        }

        public void CargarDataGrid()
        {
            String rutaAbsoluta = @"C:/LENGUAJE_PROGRAMACION/TRABAJANDO_CSHARP/persona.json";
            if (File.Exists(rutaAbsoluta))
            {
                string cadenaJson = File.ReadAllText(rutaAbsoluta);
                JArray objetos = JArray.Parse(cadenaJson);
                List<dynamic> productos_lo = new List<dynamic>();
                foreach (var objeto in objetos)
                {
                    string? dir = objeto["direccion"]["calle"].ToString()
                                  + " " + objeto["direccion"]["numero"]
                                  + " " + objeto["direccion"]["ciudad"];

                    string hob = string.Join(", ", objeto["hobbies"]);
                    var objetoJson = new
                    {
                        idPersona = objeto["idPersona"],
                        nombre = objeto["nombre"],
                        edad = objeto["edad"],
                        estatura = objeto["estatura"],
                        casado = objeto["casado"],
                        sexo = objeto["sexo"],
                        direccion = dir,
                        hobbies = hob
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
