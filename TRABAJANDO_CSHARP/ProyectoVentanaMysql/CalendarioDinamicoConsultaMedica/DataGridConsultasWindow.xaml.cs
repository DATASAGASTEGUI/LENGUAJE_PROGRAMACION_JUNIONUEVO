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

namespace ProyectoVentanaMysql.CalendarioDinamicoConsultaMedica
{
    /// <summary>
    /// Lógica de interacción para DataGridConsultasWindow.xaml
    /// </summary>
    public partial class DataGridConsultasWindow : Window
    {
        public DataGridConsultasWindow(string fecha, List<Consulta> consultas_lo)
        {
            InitializeComponent();

            personarlizarVentana(fecha);
            crearDataGridConsultas(consultas_lo);
        }

        public void personarlizarVentana(string fecha)
        {
            Title = $"Consultas para el {fecha}";
            Width = 600;
            Height = 400;
            this.FontFamily = new FontFamily("Courier New");
            this.FontSize = 10;

            string rutaRelativa = "imagen/cross1.png";
            string rutaAbsoluta = System.IO.Path.GetFullPath(rutaRelativa);
            this.Icon = new BitmapImage(new Uri(rutaAbsoluta, UriKind.Relative));

            this.ResizeMode = ResizeMode.NoResize;
            this.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            this.WindowStyle = WindowStyle.SingleBorderWindow;
            this.Background = Brushes.LightGray;
        }

        public void crearDataGridConsultas(List<Consulta> consultas_lo) 
        {
            DataGrid dataGrid = new DataGrid
            {
               Margin = new Thickness(10),
               AutoGenerateColumns = false,
               IsReadOnly = true,
               ItemsSource = consultas_lo,
               HorizontalAlignment = HorizontalAlignment.Stretch,
               VerticalAlignment = VerticalAlignment.Stretch
            };

            // Configurar las columnas para que tengan igual tamaño
            // y se adapten al ancho de la ventana
            dataGrid.Columns.Clear();
            var columnWidth = new DataGridLength(1, DataGridLengthUnitType.Star); // Estilo de ancho proporcional

            dataGrid.Columns.Add(new DataGridTextColumn
            {
                Header = "Número Consulta",
                Binding = new System.Windows.Data.Binding("NumeroConsulta"),
                Width = columnWidth
            });

            dataGrid.Columns.Add(new DataGridTextColumn
            {
                Header = "Fecha",
                Binding = new System.Windows.Data.Binding("Fecha"),
                Width = columnWidth
            });

            dataGrid.Columns.Add(new DataGridTextColumn
            {
                Header = "Nombre Médico",
                Binding = new System.Windows.Data.Binding("NombreMedico"),
                Width = columnWidth
            });

            dataGrid.Columns.Add(new DataGridTextColumn
            {
                Header = "Tipo Parto",
                Binding = new System.Windows.Data.Binding("Deinpr"),
                Width = columnWidth
            });

            dataGrid.Columns.Add(new DataGridTextColumn
            {
                Header = "Procedencia",
                Binding = new System.Windows.Data.Binding("Procedencia"),
                Width = columnWidth
            });
            dataGrid.CanUserResizeColumns = true; // Evitar redimensionar columnas manualmente
            this.Content = dataGrid; //Agregar el dataGrid a nuestra ventana

        }


    }
}
