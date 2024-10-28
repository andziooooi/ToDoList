using System.ComponentModel;
using System.Runtime.CompilerServices;
using ToDoList.Model;

namespace ToDoList.ViewModels
{
    public class ShowDetailsWVM : INotifyPropertyChanged
    {
        private Model.Task _task;
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

        public DateTime DeadLine
        {
            get => _task.Deadline;
            set
            {
                _task.Deadline = value;
                OnPropertyChanged(nameof(Priority));
            }
        }

        public Model.TaskStatus Status
        {
            get => _task.Status;
            set
            {
                _task.Status = value;
                OnPropertyChanged(nameof(Status));
            }
        }

        public ShowDetailsWVM(Model.Task task) 
        {
            _task = task;
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        private void OnPropertyChanged([CallerMemberName] string? propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
