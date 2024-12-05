using System;
using System.Collections.Generic;
using System.Globalization;
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

namespace ProyectoVentanaMysql.CalendarioDinamico
{
    /// <summary>
    /// Lógica de interacción para CalendarioWindow.xaml
    /// </summary>
    public partial class CalendarioWindow : Window
    {
        public CalendarioWindow()
        {
            InitializeComponent();
            CrearInterfazDinamica();
        }

        private void CrearInterfazDinamica()
        {
            // Configuración de la ventana
            this.Title = "Calendario Dinámico";
            this.Width = 600;
            this.Height = 500;
            this.WindowStartupLocation = WindowStartupLocation.CenterScreen;

            // Crear un Grid
            Grid gridMain = new Grid
            {
                Margin = new Thickness(10)
            };

            // Definir filas y columnas del Grid
            for (int i = 0; i < 8; i++) // 8 filas (1 para el año y mes, 1 para los días de la semana y 6 para los días del mes)
            {
                gridMain.RowDefinitions.Add(new RowDefinition());
            }

            for (int i = 0; i < 7; i++) // 7 columnas (7 días de la semana)
            {
                gridMain.ColumnDefinitions.Add(new ColumnDefinition());
            }

            // Mostrar el año y el mes en la primera fila
            string mesNombre = DateTime.Now.ToString("MMMM", CultureInfo.GetCultureInfo("es-ES"));
            string año = DateTime.Now.Year.ToString();
            TextBlock mesAñoTextBlock = new TextBlock
            {
                Text = $"{mesNombre} {año}",
                FontSize = 18,
                FontWeight = FontWeights.Bold,
                HorizontalAlignment = HorizontalAlignment.Center,
                VerticalAlignment = VerticalAlignment.Center
            };
            Grid.SetRow(mesAñoTextBlock, 0); // Primera fila
            Grid.SetColumnSpan(mesAñoTextBlock, 7); // Ocupa las 7 columnas
            gridMain.Children.Add(mesAñoTextBlock);

            // Añadir etiquetas para los días de la semana
            string[] diasSemana = { "Lun", "Mar", "Mié", "Jue", "Vie", "Sáb", "Dom" };
            for (int i = 0; i < diasSemana.Length; i++)
            {
                TextBlock diaSemana = new TextBlock
                {
                    Text = diasSemana[i],
                    FontWeight = FontWeights.Bold,
                    HorizontalAlignment = HorizontalAlignment.Center,
                    VerticalAlignment = VerticalAlignment.Center
                };
                Grid.SetRow(diaSemana, 1); // Segunda fila
                Grid.SetColumn(diaSemana, i);
                gridMain.Children.Add(diaSemana);
            }

            // Crear botones para los días del mes
            DateTime primerDiaMes = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
            int diasEnMes = DateTime.DaysInMonth(primerDiaMes.Year, primerDiaMes.Month);
            int diaDeInicio = (int)primerDiaMes.DayOfWeek; // Obtener día de la semana del primer día (0 = Domingo)
            MessageBox.Show($"Campos con formato incorrecto {diasEnMes}, {diaDeInicio}", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
            if (diaDeInicio == 0) diaDeInicio = 7; // Ajustar para que Lunes sea 1 y Domingo sea 7

            int fila = 2; // Comienza en la tercera fila
            int columna = diaDeInicio - 1; // Ajustar columna de inicio
            for (int dia = 1; dia <= diasEnMes; dia++)
            {
                Button botonDia = new Button
                {
                    Content = dia.ToString(),
                    HorizontalAlignment = HorizontalAlignment.Stretch,
                    VerticalAlignment = VerticalAlignment.Stretch,
                    Background = dia == DateTime.Now.Day ? Brushes.LightBlue : Brushes.White,
                    BorderBrush = Brushes.Gray,
                    Margin = new Thickness(2)
                };

                // Capturar el valor de 'dia' dentro de un objeto anónimo para evitar que se sobrescriba
                int diaCapturado = dia;

                // Usamos el valor de 'diaCapturado' en el evento Click
                botonDia.Click += (s, e) => MessageBox.Show($"Has seleccionado el día {diaCapturado}");

                Grid.SetRow(botonDia, fila);
                Grid.SetColumn(botonDia, columna);
                gridMain.Children.Add(botonDia);

                // Moverse a la siguiente columna y fila
                columna++;
                if (columna > 6)
                {
                    columna = 0;
                    fila++;
                }
            }

            // Asignar el Grid al contenido de la ventana
            this.Content = gridMain;
        }
    }
}
