using System.Windows;
using ToDoList.Model;
using ToDoList.Services;
using ToDoList.ViewModels;

namespace ToDoList
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            var dbContext = new TaskContext();
            var dataService = new DataService(dbContext);
            InitializeComponent();
            DataContext = new MainViewModel(dataService);
        }
    }
}