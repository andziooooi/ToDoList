using System.Windows;
using System.Windows.Threading;

namespace ToDoList.Views
{
    /// <summary>
    /// Logika interakcji dla klasy NotificationW.xaml
    /// </summary>
    public partial class NotificationW : Window
    {
        public NotificationW(string message)
        {
            InitializeComponent();
            DataContext = new ViewModels.NotificationWVM(message);
            DispatcherTimer timer = new DispatcherTimer
            {
                Interval = TimeSpan.FromSeconds(2)
            };
            timer.Tick += Timer_Tick;
            timer.Start();
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            DispatcherTimer timer = sender as DispatcherTimer;
            timer.Stop();
            this.Close();
        }
    
    }
}
