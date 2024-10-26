using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using ToDoList.Commands;
using ToDoList.Model;
using ToDoList.Services;
using ToDoList.Views;

namespace ToDoList.ViewModels
{
    public class MainViewModel : INotifyPropertyChanged
    {
        private DataService _dataService;
        public static ObservableCollection<Model.Task> Tasks { get;set; }

        private Model.Task _selectedTask;
        public Model.Task SelectedTask
        {
            get => _selectedTask;
            set { _selectedTask = value; OnPropertyChanged(); }
        }

        public ICommand AddTaskCommand { get; }
        public ICommand DeleteTaskCommand { get; }
        public ICommand SaveTasksCommand { get; }
        public MainViewModel(DataService dataService)
        {
            _dataService = dataService;
            Tasks = _dataService.GetItems();

            AddTaskCommand = new RelayCommand(AddTask);
            DeleteTaskCommand = new RelayCommand(DeleteTask);
            SaveTasksCommand = new RelayCommand(SaveTasks);
        }
        private void AddTask(object parameter)
        {
            var newTask = new Model.Task
            {
                Name = "New Task",
                Description = "",
                Priority = PriorityLevel.Medium,
                Deadline = DateTime.Now.AddDays(7),
                Status = Model.TaskStatus.ToDo
            };
            var addtaskw = new AddTaskW();
            addtaskw.DataContext = new AddTaskWVM(newTask,_dataService, () => addtaskw.Close());
            addtaskw.ShowDialog();
            _dataService.SaveChanges();
        }

        private void DeleteTask(object parameter)
        {
            if (SelectedTask != null)
            {
                _dataService.RemoveItem(SelectedTask);
                Tasks.Remove(SelectedTask);
            }
        }
        private void SaveTasks(object parameter)
        {
            _dataService.SaveChanges();
        }
        public event PropertyChangedEventHandler? PropertyChanged;

        private void OnPropertyChanged([CallerMemberName] string? propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
