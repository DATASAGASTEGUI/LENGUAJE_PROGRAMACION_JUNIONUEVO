using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

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
    /// Lógica de interacción para CalculadoraWindow.xaml
    /// </summary>
    public partial class CalculadoraWindow : Window
    {
        public CalculadoraWindow()
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
                if (textoTag == "C")  // Si el botón presionado es "C", limpia la caja de texto
                {
                    TxtContenido.Clear();
                }
                else if (textoTag != "=")  // Si el botón no es "=", se agrega el texto del botón al contenido
                {
                    contenido = TxtContenido.Text + textoTag; // Anterior + Nuevo
                    TxtContenido.Text = contenido;
                }
                else  // Si el botón es "=", realiza la operación
                {
                    contenido = TxtContenido.Text;
                    var parte = Regex.Split(contenido, @"(\+|\-|\*|\/)");

                    if (parte.Length == 3)  // Verificamos si la expresión tiene 3 partes
                    {
                        int n1, n2;
                        if (int.TryParse(parte[0], out n1) && int.TryParse(parte[2], out n2))
                        {
                            string op = parte[1];

                            if (op == "+")
                            {
                                int z = n1 + n2;
                                TxtContenido.Text = contenido + "=" + z;
                            }
                            else if (op == "-")
                            {
                                int z = n1 - n2;
                                TxtContenido.Text = contenido + "=" + z;
                            }
                            else if (op == "*")
                            {
                                int z = n1 * n2;
                                TxtContenido.Text = contenido + "=" + z;
                            }
                            else if (op == "/")
                            {
                                if (n2 == 0)
                                {
                                    MessageBox.Show("Error: No se puede dividir entre cero.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                                }
                                else
                                {
                                    int z = n1 / n2;
                                    TxtContenido.Text = contenido + "=" + z;
                                }
                            }
                        }
                        else
                        {
                            MessageBox.Show("Entrada inválida", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                        }
                    }
                    else
                    {
                        MessageBox.Show("Formato de entrada inválido", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
            }
        }
    }
}
