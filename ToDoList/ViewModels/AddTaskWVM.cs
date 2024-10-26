using ToDoList.Model;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using ToDoList.Commands;
using ToDoList.Services;


namespace ToDoList.ViewModels
{
    public class AddTaskWVM : INotifyPropertyChanged
    {
        private DataService _dataService;
        private Model.Task _task;
        private readonly Action _closeAction;

        public RelayCommand AcceptTaskCommand { get; }

        public string Name
        {
            get => _task.Name;
            set
            {
                _task.Name = value;
                OnPropertyChanged(nameof(Name));
            }
        }

        public string Description
        {
            get => _task.Description;
            set
            {
                _task.Description = value;
                OnPropertyChanged(nameof(Description));
            }
        }

        public PriorityLevel Priority
        {
            get => _task.Priority;
            set
            {
                _task.Priority = value;
                OnPropertyChanged(nameof(Priority));
            }
        }

        public AddTaskWVM(Model.Task task,DataService dataService,Action closeAction)
        {
            _closeAction = closeAction;
            _dataService = dataService;
            _task = task;
            AcceptTaskCommand = new RelayCommand(AcceptTask);

        }

        private void AcceptTask(object obj)
        {
            var newTask = new Model.Task
            {
                Name = Name,
                Description = Description,
                Priority = Priority,
                Deadline = DateTime.Now.AddDays(7),
                Status = Model.TaskStatus.ToDo
            };
            MainViewModel.Tasks.Add(newTask);
            _dataService.AddItem(newTask);
            _dataService.SaveChanges();
            _closeAction?.Invoke();
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        private void OnPropertyChanged([CallerMemberName] string? propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
