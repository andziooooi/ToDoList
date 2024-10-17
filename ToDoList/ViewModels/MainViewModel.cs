using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using ToDoList.Commands;
using ToDoList.Model;

namespace ToDoList.ViewModels
{
    public class MainViewModel : INotifyPropertyChanged
    {
        private ObservableCollection<Model.Task> _tasks;
        public ObservableCollection<Model.Task> Tasks
        {
            get => _tasks;
            set { _tasks = value; OnPropertyChanged(); }
        }

        private Model.Task _selectedTask;
        public Model.Task SelectedTask
        {
            get => _selectedTask;
            set { _selectedTask = value; OnPropertyChanged(); }
        }

        public ICommand AddTaskCommand { get; }
        public ICommand DeleteTaskCommand { get; }
        public ICommand SaveTasksCommand { get; }
        public ICommand SaveChangesCommand { get; }

        private TaskContext _context;

        public MainViewModel()
        {
            _context = new TaskContext();
            _context.Database.EnsureCreated();
            Tasks = new ObservableCollection<Model.Task>(_context.Tasks.ToList());

            AddTaskCommand = new RelayCommand(AddTask);
            DeleteTaskCommand = new RelayCommand(DeleteTask);
            SaveTasksCommand = new RelayCommand(SaveTasks);
            SaveChangesCommand = new RelayCommand(SaveChanges);
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
            _context.Tasks.Add(newTask);
            _context.SaveChanges();
            Tasks.Add(newTask);
            SelectedTask = newTask;
        }

        private void DeleteTask(object parameter)
        {
            if (SelectedTask != null)
            {
                _context.Tasks.Remove(SelectedTask);
                _context.SaveChanges();
                Tasks.Remove(SelectedTask);
            }
        }
        private void SaveChanges(object obj)
        {
            _context.SaveChanges();
        }

        private void SaveTasks(object parameter)
        {
            _context.SaveChanges();
        }
        public event PropertyChangedEventHandler? PropertyChanged;

        private void OnPropertyChanged([CallerMemberName] string? propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
