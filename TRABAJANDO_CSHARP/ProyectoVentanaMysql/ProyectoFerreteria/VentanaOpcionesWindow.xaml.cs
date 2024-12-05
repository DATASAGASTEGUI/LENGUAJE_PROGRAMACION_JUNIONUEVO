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
    /// Lógica de interacción para VentanaOpcionesWindow.xaml
    /// </summary>
    public partial class VentanaOpcionesWindow : Window
    {
        public DataGrid VentaDataGrid;
        public VentanaOpcionesWindow(DataGrid dg)
        {
            InitializeComponent();
            VentaDataGrid = dg;
        }

        private void YearComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (YearComboBox.SelectedItem is ComboBoxItem selectedYear)
            {
                int y = Convert.ToInt32(YearComboBox.SelectedItem.ToString());
                int year = Convert.ToInt32(selectedYear.Content.ToString());
                VentaDataGrid.ItemsSource = MetodosCrud.BuscarVentasPorYear(year);
            }

        }
    }
}
