namespace ToDoList.ViewModels
{
    internal class NotificationWVM
    {
        private string _notify;
        public string Noti { get => _notify;set{ _notify =value; } }
        public NotificationWVM(string Notify)
        {
            _notify = Notify; 
        }
    }
}
