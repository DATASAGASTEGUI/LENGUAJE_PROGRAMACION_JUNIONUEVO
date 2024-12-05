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
using System.Windows.Threading;

namespace ProyectoVentanaMysql.Reloj
{
    /// <summary>
    /// Lógica de interacción para RelojWindow.xaml
    /// </summary>
    public partial class RelojWindow : Window
    {
        public DispatcherTimer reloj;
        public RelojWindow()
        {
            InitializeComponent();
            reloj = new DispatcherTimer();
            reloj.Interval = TimeSpan.FromSeconds(1);//Actualizar cada segundo
            reloj.Tick += ActualizarReloj;
            reloj.Start(); //Iniciarl el reloj
        }

        public void ActualizarReloj(object sender, EventArgs e)
        {
            //ClockLabel.Content = DateTime.Now.ToString("HH:mm:ss");
            ClockLabel.Content = DateTime.Now.ToString("hh:mm:ss tt", System.Globalization.CultureInfo.InvariantCulture);
        }


        private void SumButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void NewButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void ExitButton_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
