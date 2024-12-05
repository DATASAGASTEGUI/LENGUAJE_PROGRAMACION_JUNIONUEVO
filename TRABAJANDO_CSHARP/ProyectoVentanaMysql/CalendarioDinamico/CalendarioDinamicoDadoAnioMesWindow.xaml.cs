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
    /// Lógica de interacción para CalendarioDinamicoDadoAnioMesWindow.xaml
    /// </summary>
    public partial class CalendarioDinamicoDadoAnioMesWindow : Window
    {
        private Grid gridMain;
        private ComboBox comboBoxAnio;
        private ComboBox comboBoxMes;
        private Button botonActualizar;
        public CalendarioDinamicoDadoAnioMesWindow()
        {
            InitializeComponent();
            personalizarVentana();
            CrearInterfazDinamica();
        }

        private void personalizarVentana()
        {
            this.Title = "Calendario Dinámico";
            this.Width = 600;
            this.Height = 500;
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
            for (int i = DateTime.Now.Year - 150; i <= DateTime.Now.Year + 100; i++)
            {
                comboBoxAnio.Items.Add(i.ToString());
            }
            comboBoxAnio.SelectedItem = DateTime.Now.Year.ToString();
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
                Width = 400,
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
            Grid.SetColumnSpan(botonActualizar, 5);
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
            CrearCalendario(añoSeleccionado, mesSeleccionado);
        }

        private void CrearCalendario(int año, int mes)
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
            string nombreMesEspaniol = new DateTime(año, mes, 1).ToString("MMMM", CultureInfo.GetCultureInfo("es-ES")).ToUpper();
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
            mesAñoTextBlock.Text = $"{nombreMesEspaniol} {año}";
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
            DateTime primerDiaMes = new DateTime(año, mes, 1);
            int diasEnMes = DateTime.DaysInMonth(año, mes);
            int diaDeInicio = (int)primerDiaMes.DayOfWeek; // Obtener día de la semana del primer día (0 = Domingo)
            MessageBox.Show($"{diasEnMes}  {diaDeInicio}");
            if (diaDeInicio == 0) diaDeInicio = 7; // Ajustar para que Lunes sea 1 y Domingo sea 7

            int fila = 3; // Comienza en la tercera fila
            int columna = diaDeInicio - 1; // Ajustar columna de inicio
            for (int dia = 1; dia <= diasEnMes; dia++)
            {
                Button botonDia = new Button
                {
                    Content = dia.ToString(),
                    HorizontalAlignment = HorizontalAlignment.Stretch,
                    VerticalAlignment = VerticalAlignment.Stretch,
                    //Background = dia == DateTime.Now.Day ? Brushes.LightBlue : Brushes.White,
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
        }
    }
}

/* (1)

            // Limpiar el Grid para que no se dupliquen los controles, pero dejando los controles estáticos
            /*
            for (int i = 3; i < gridMain.RowDefinitions.Count; i++)
            {
                for (int j = 0; j < gridMain.ColumnDefinitions.Count; j++)
                {
                    var element = gridMain.Children.Cast<UIElement>()
                        .FirstOrDefault(e => Grid.GetRow(e) == i && Grid.GetColumn(e) == j);
                    if (element != null)
                    {
                        gridMain.Children.Remove(element);
                    }
                }
            }
*/

/* (2)
// Limpiar el Grid a partir de la fila 3
gridMain.Children.Cast<UIElement>()
    .Where(e => Grid.GetRow(e) >= 3)
    .ToList()
    .ForEach(e => gridMain.Children.Remove(e));
*/

/*
--------------------------------------------------------------------------------
Claro, el código proporcionado tiene como objetivo limpiar elementos dinámicos 
de un Grid en filas específicas (a partir de la fila 3), manteniendo los 
elementos estáticos en su lugar. Vamos a simplificarlo y explicarlo paso a paso:
--------------------------------------------------------------------------------
Código simplificado:
--------------------------------------------------------------------------------
// Limpiar el Grid a partir de la fila 3

gridMain.Children.Cast<UIElement>()
    .Where(e => Grid.GetRow(e) >= 3)
    .ToList()
    .ForEach(e => gridMain.Children.Remove(e));
--------------------------------------------------------------------------------
Explicación paso a paso:

Convertir los elementos del Grid a una lista de UIElement:

El método gridMain.Children.Cast<UIElement>() convierte la colección de 
controles (Children) en un enumerable que puede ser filtrado.

Filtrar los elementos por fila (Grid.GetRow(e) >= 3):

Usamos Where para obtener solo los elementos cuya posición esté en filas iguales
o superiores a 3.

Convertir el resultado en una lista:

Usamos ToList() para evitar problemas de modificación concurrente al eliminar
elementos de la colección Children.

Eliminar los elementos con ForEach:

Iteramos sobre los elementos seleccionados y los eliminamos del Grid con
gridMain.Children.Remove(e).
--------------------------------------------------------------------------------
Diferencias clave respecto al código original:

Eliminamos el bucle anidado: No es necesario recorrer columnas, ya que los 
elementos dinámicos a eliminar ya están identificados por su fila.

Uso de LINQ: Hacemos el código más legible y eliminamos la necesidad de manejar
índices manualmente.

Eficiencia: Aunque ambas versiones tienen un comportamiento similar, la versión
simplificada es más concisa y legible.
--------------------------------------------------------------------------------
Consideraciones adicionales:

Performance: La versión simplificada es eficiente para la mayoría de los 
escenarios típicos en WPF. Si tienes un Grid con miles de elementos, podrías
considerar optimizaciones adicionales.

Control estático: Asegúrate de que los controles estáticos no estén en filas
superiores a 2, ya que el filtro eliminará todo a partir de la fila 3. Si 
necesitas excluir elementos específicos (por tipo o nombre), puedes modificar el
filtro, por ejemplo:

.Where(e => Grid.GetRow(e) >= 3 && !(e is TextBlock))
--------------------------------------------------------------------------------
*/