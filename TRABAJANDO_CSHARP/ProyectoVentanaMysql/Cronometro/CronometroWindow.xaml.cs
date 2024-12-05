using System.Windows;
using System.Windows.Threading;

namespace ProyectoVentanaMysql.Cronometro
{
    /// <summary>
    /// Lógica de interacción para CronometroWindow.xaml
    /// </summary>
    public partial class CronometroWindow : Window
    {
        private DispatcherTimer timer; // Timer para manejar el tiempo (DispatcherTimer = temporizador)
        private TimeSpan tiempoTranscurrido;  // Almacena el tiempo transcurrido (TimeSpan = Intervalo de tiempo)

        public CronometroWindow()
        {
            InitializeComponent();
            // Configuración inicial del cronómetro
            timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromSeconds(1); // Actualiza cada segundo
            timer.Tick += Timer_Tick; //Método para Actualizar
            tiempoTranscurrido = TimeSpan.Zero; // Tiempo inicial
        }

        // Evento que maneja el timer
        private void Timer_Tick(object sender, EventArgs e)
        {
            tiempoTranscurrido = tiempoTranscurrido.Add(TimeSpan.FromSeconds(1));
            TimerLabel.Content = tiempoTranscurrido.ToString(@"hh\:mm\:ss");
        }

        private void StartButton_Click(object sender, RoutedEventArgs e)
        {
            timer.Start();
        }

        private void PauseButton_Click(object sender, RoutedEventArgs e)
        {
            timer.Stop();
        }

        private void ResetButton_Click(object sender, RoutedEventArgs e)
        {
            timer.Stop();
            tiempoTranscurrido = TimeSpan.Zero;
            TimerLabel.Content = "00:00:00";
        }
    }
}
