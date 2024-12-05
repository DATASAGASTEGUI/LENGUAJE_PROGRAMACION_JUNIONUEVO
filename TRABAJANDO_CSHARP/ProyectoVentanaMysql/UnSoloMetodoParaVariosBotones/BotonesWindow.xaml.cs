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

namespace ProyectoVentanaMysql.UnSoloMetodoParaVariosBotones
{
    /// <summary>
    /// Lógica de interacción para BotonesWindow.xaml
    /// </summary>
    public partial class BotonesWindow : Window
    {
        public BotonesWindow()
        {
            InitializeComponent();
        }

        private void Boton_Click(object sender, RoutedEventArgs e)
        {
            Button boton = (Button)sender;
            string? textoTag = boton.Tag as string;
            string? contenido = "";
            if (textoTag != null)
            {
                //MessageBox.Show($"Presiono boton {textoTag}", "Information", MessageBoxButton.OK, MessageBoxImage.Information);
                contenido = TxtContenido.Text + textoTag;//Anterior + Nuevo
                TxtContenido.Text = contenido;



            }
        }
    }
}
