﻿using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
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
        public RelayCommand ShowDetailsCommand { get; }

        public MainViewModel(DataService dataService)
        {
            _dataService = dataService;
            Tasks = _dataService.GetItems();


            AddTaskCommand = new RelayCommand(AddTask);
            DeleteTaskCommand = new RelayCommand(DeleteTask);
            SaveTasksCommand = new RelayCommand(SaveTasks);
            ShowDetailsCommand = new RelayCommand(ShowDetails);
        }

        private void ShowDetails(object obj)
        {
            if(obj is Model.Task task)
            {
                var showd = new ShowDetailsW();
                showd.DataContext = new ShowDetailsWVM(task);
                showd.Show();
            }
        }

        private void AddTask(object parameter)
        {
            var newTask = new Model.Task
            {
                Name = "New Task",
                Description = "",
                Priority = PriorityLevel.Medium,
                Deadline = DateTime.Now,
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
            var childWindow = new NotificationW("Changes Saved!");
            var parentWindow = Application.Current.MainWindow;
            childWindow.Owner = parentWindow;
            
            childWindow.WindowStartupLocation = WindowStartupLocation.Manual;
            childWindow.Left = parentWindow.Left + (parentWindow.Width - childWindow.Width) / 2;
            childWindow.Top=parentWindow.Top+ parentWindow.Height-100;

            childWindow.Show();
        }
        public event PropertyChangedEventHandler? PropertyChanged;

        private void OnPropertyChanged([CallerMemberName] string? propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
