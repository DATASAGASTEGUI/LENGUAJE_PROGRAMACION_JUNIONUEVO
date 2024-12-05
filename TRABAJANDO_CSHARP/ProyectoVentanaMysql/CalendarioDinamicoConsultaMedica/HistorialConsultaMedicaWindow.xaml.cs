using ProyectoVentanaMysql.ProyectoFerreteria;
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
using System.Windows.Threading;
using static System.Net.Mime.MediaTypeNames;

namespace ProyectoVentanaMysql.CalendarioDinamicoConsultaMedica
{
    /// <summary>
    /// Lógica de interacción para HistorialConsultaMedicaWindow.xaml
    /// </summary>
    public partial class HistorialConsultaMedicaWindow : Window
    {
        private Grid gridMain;
        private ComboBox comboBoxAnio;
        private ComboBox comboBoxMes;
        private Button botonActualizar;
        private DispatcherTimer reloj;
        TextBlock textReloj;
        TextBlock textConsultas;

        public HistorialConsultaMedicaWindow()
        {
            InitializeComponent();
            personalizarVentana();
            CrearInterfazDinamica();

            reloj = new DispatcherTimer();
            reloj.Interval = TimeSpan.FromSeconds(1);//Actualizar cada segundo
            reloj.Tick += ActualizarReloj;
            reloj.Start(); //Iniciarl el reloj

            textReloj = new TextBlock
            {
                Text = "00:00:00",
                HorizontalAlignment = HorizontalAlignment.Center,
                VerticalAlignment = VerticalAlignment.Center,
                TextWrapping = TextWrapping.Wrap,
                FontSize = 12
            };
           
            Grid.SetRow(textReloj, 0);
            Grid.SetColumn(textReloj, 6);
            gridMain.Children.Add(textReloj);

            textConsultas = new TextBlock
            {
                HorizontalAlignment = HorizontalAlignment.Center,
                VerticalAlignment = VerticalAlignment.Center,
                TextWrapping = TextWrapping.Wrap,
                Text = "CONSULTAS"
            };
            Grid.SetRow(textConsultas, 0);
            Grid.SetColumn(textConsultas, 5);
            gridMain.Children.Add(textConsultas);
    }

        public void ActualizarReloj(object sender, EventArgs e)
        {
            //ClockLabel.Content = DateTime.Now.ToString("HH:mm:ss");
            textReloj.Text = DateTime.Now.ToString("hh:mm:ss tt", System.Globalization.CultureInfo.InvariantCulture);
        }
        private void personalizarVentana()
        {
            this.Title = "Calendario Dinámico";
            this.Width = 700;
            this.Height = 600;
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
        private void CrearInterfazDinamica()
        {
            // Crear un Grid
            gridMain = new Grid
            {
                Margin = new Thickness(10)
            };

            // Definir filas y columnas del Grid
            for (int i = 0; i < 9; i++) // 8 filas (1 para el año y mes, 1 para los días de la semana y 6 para los días del mes)
            {
                gridMain.RowDefinitions.Add(new RowDefinition());
            }

            for (int i = 0; i < 7; i++) // 7 columnas (7 días de la semana)
            {
                gridMain.ColumnDefinitions.Add(new ColumnDefinition());
            }

            // *****************************************************************
            // Crear ComboBox para seleccionar el año
            comboBoxAnio = new ComboBox
            {
                Width = 65,
                Height = 23,
                FontSize = 10,
                HorizontalContentAlignment = HorizontalAlignment.Center,
                VerticalContentAlignment = VerticalAlignment.Center,
                //HorizontalAlignment = HorizontalAlignment.Center,
                //VerticalAlignment = VerticalAlignment.Center
            };
            // LLENAR EL COMBOBOX ANIO
            List<int> anios_li = MetodoCrud.ObtenerListaAnios();
            for (int i = 0; i < anios_li.Count; i++)
            {
                comboBoxAnio.Items.Add(anios_li[i].ToString());
            }
            comboBoxAnio.SelectedItem = anios_li[0].ToString();
            // *****************************************************************

            // *****************************************************************
            // Crear ComboBox para seleccionar el mes
            comboBoxMes = new ComboBox
            {
                Width = 65,
                Height = 23,
                FontSize = 10,
                HorizontalContentAlignment = HorizontalAlignment.Center,
                VerticalContentAlignment = VerticalAlignment.Center,
                //HorizontalAlignment = HorizontalAlignment.Center,
                //VerticalAlignment = VerticalAlignment.Center
            };
            for (int i = 1; i <= 12; i++)
            {
                comboBoxMes.Items.Add(i.ToString());
            }
            comboBoxMes.SelectedItem = DateTime.Now.Month.ToString();
            // *****************************************************************

            // *****************************************************************
            // Crear botón de actualización
            botonActualizar = new Button
            {
                Content = "Actualizar",
                Height = 23,
                Width = 300,
                FontSize = 10,
                HorizontalContentAlignment = HorizontalAlignment.Center,
                VerticalContentAlignment = VerticalAlignment.Center,
                //HorizontalAlignment = HorizontalAlignment.Center,
                //VerticalAlignment = VerticalAlignment.Center
            };
            botonActualizar.Click += BotonActualizar_Click;
            // *****************************************************************

            // *****************************************************************
            // Colocar los ComboBox y el botón en la primera fila
            Grid.SetRow(comboBoxAnio, 0);
            Grid.SetColumn(comboBoxAnio, 0);
            gridMain.Children.Add(comboBoxAnio);

            Grid.SetRow(comboBoxMes, 0);
            Grid.SetColumn(comboBoxMes, 1);
            gridMain.Children.Add(comboBoxMes);

            Grid.SetRow(botonActualizar, 0);
            Grid.SetColumn(botonActualizar, 2);
            Grid.SetColumnSpan(botonActualizar, 3);
            gridMain.Children.Add(botonActualizar);
            // *****************************************************************

            // *****************************************************************
            // Añadir etiquetas para los días de la semana
            string[] diasSemana = { "Lum", "Mar", "Mié", "Jue", "Vie", "Sáb", "Dom" };
            for (int i = 0; i < diasSemana.Length; i++)
            {
                TextBlock diaSemana = new TextBlock
                {
                    Text = diasSemana[i],
                    FontWeight = FontWeights.Bold,
                    HorizontalAlignment = HorizontalAlignment.Center,
                    VerticalAlignment = VerticalAlignment.Center
                };
                Grid.SetRow(diaSemana, 2); // Segunda fila
                Grid.SetColumn(diaSemana, i);
                gridMain.Children.Add(diaSemana);
            }

            // Asignar el Grid al contenido de la ventana
            this.Content = gridMain;
            // *****************************************************************
        }

        private void BotonActualizar_Click(object sender, RoutedEventArgs e)
        {
            int añoSeleccionado = int.Parse(comboBoxAnio.SelectedItem.ToString());
            int mesSeleccionado = int.Parse(comboBoxMes.SelectedItem.ToString());
            textConsultas.Text = "Consultas Mes\n" + MetodoCrud.ObtenerNumeroConsultasPorMes(añoSeleccionado, mesSeleccionado).ToString();
            CrearCalendario(añoSeleccionado, mesSeleccionado);
        }

        private void CrearCalendario(int anio, int mes)
        {
            // *****************************************************************
            // LIMPIAR EL GRID A PARTIR DE LA FILA 3
            gridMain.Children.Cast<UIElement>()
                .Where(e => Grid.GetRow(e) >= 3)
                .ToList()
                .ForEach(e => gridMain.Children.Remove(e));
            // *****************************************************************

            // ***************************************************************** 
            // CREAR O ACTUALIZAR EL AÑO Y MES EN LA PRIMERA FILA
            // Obtener el nombre del mes en español
            string nombreMesEspaniol = new DateTime(anio, mes, 1).ToString("MMMM", CultureInfo.GetCultureInfo("es-ES")).ToUpper();
            // Buscar o crear el TextBlock en la primera fila
            TextBlock mesAñoTextBlock = gridMain.Children.OfType<TextBlock>()
                .FirstOrDefault(tb => Grid.GetRow(tb) == 1) ?? new TextBlock
                {
                    FontSize = 18,
                    FontWeight = FontWeights.Bold,
                    HorizontalAlignment = HorizontalAlignment.Center,
                    VerticalAlignment = VerticalAlignment.Center
                };
            // Actualizar texto
            mesAñoTextBlock.Text = $"{nombreMesEspaniol} {anio}";
            // Si el TextBlock es nuevo(no esta contenido como hijo de gridMain), configúralo y añádelo al Grid
            if (!gridMain.Children.Contains(mesAñoTextBlock))
            {
                // Primera fila
                Grid.SetRow(mesAñoTextBlock, 1);
                // Ocupa las 7 columnas
                Grid.SetColumnSpan(mesAñoTextBlock, 7);
                // Agrega el TextBlock al Grid
                gridMain.Children.Add(mesAñoTextBlock);
            }
            // ***************************************************************** 

            // Crear botones para los días del mes
            DateTime primerDiaMes = new DateTime(anio, mes, 1);
            int diasEnMes = DateTime.DaysInMonth(anio, mes);
            int diaDeInicio = (int)primerDiaMes.DayOfWeek; // Obtener día de la semana del primer día (0 = Domingo)
            MessageBox.Show($"{diasEnMes}  {diaDeInicio}");
            if (diaDeInicio == 0) diaDeInicio = 7; // Ajustar para que Lunes sea 1 y Domingo sea 7

            int fila = 3; // Comienza en la tercera fila
            int columna = diaDeInicio - 1; // Ajustar columna de inicio
            for (int dia = 1; dia <= diasEnMes; dia++)
            {
                string resultado = MetodoCrud.ObtenerCantidadPorTipoParto(anio, mes, dia);

                // Crear un TextBlock para el contenido del botón
                TextBlock textBlock = new TextBlock
                {
                    HorizontalAlignment = HorizontalAlignment.Center,
                    VerticalAlignment = VerticalAlignment.Center,
                    TextWrapping = TextWrapping.Wrap
                };

                // Añadir los textos con colores distintos
                textBlock.Inlines.Add(new Run(dia.ToString()) { Foreground = Brushes.Red }); // Rojo
                //textBlock.Inlines.Add(new Run(" ")); // Espacio entre textos
                textBlock.Inlines.Add(new Run(resultado) { Foreground = Brushes.Blue }); // Azul

                Button botonDia = new Button
                {
                    FontSize = 9,
                    Content = textBlock, // Asignar el TextBlock al contenido
                    HorizontalAlignment = HorizontalAlignment.Stretch,
                    VerticalAlignment = VerticalAlignment.Stretch,
                    //Background = dia == DateTime.Now.Day ? Brushes.LightBlue : Brushes.White,
                    BorderBrush = Brushes.Gray,
                    Background = MetodoCrud.ObtenerNumeroConsultas(anio,mes,dia) == 0 ? Brushes.Yellow : Brushes.White,
                    Margin = new Thickness(2)
                };

                // Capturar el valor de 'dia' dentro de un objeto anónimo para evitar que se sobrescriba
                int diaCapturado = dia;

                // Usamos el valor de 'diaCapturado' en el evento Click
                botonDia.Click += (s, e) =>
                //MessageBox.Show($"Has seleccionado el día {diaCapturado}");
                {
                    string fecha = diaCapturado + "/" + mes + "/" + anio;
                    List<Consulta> consultas_lo = MetodoCrud.ObtenerListaConsulta(anio, mes, diaCapturado);
                    
                    DataGridConsultasWindow ventana = new DataGridConsultasWindow(fecha, consultas_lo);
                    ventana.Show(); 
                };

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
        }
    }
}