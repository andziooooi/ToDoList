using System.Collections.ObjectModel;
using ToDoList.Model;

namespace ToDoList.Services
{
    public class DataService
    {
        private readonly TaskContext _dbContext;

        public DataService(TaskContext dbContext)
        {
            _dbContext = dbContext;
        }

        public ObservableCollection<Model.Task> GetItems()
        {
            return new ObservableCollection<Model.Task>(_dbContext.Tasks.ToList());
        }

        public Model.Task AddItem(Model.Task item)
        {
            _dbContext.Tasks.Add(item);
            _dbContext.SaveChanges();
            return item;
        }

        public void UpdateItem(Model.Task item)
        {
            _dbContext.Tasks.Update(item);
            _dbContext.SaveChanges();
        }

        public void RemoveItem(Model.Task item)
        {
            _dbContext.Tasks.Remove(item);
            _dbContext.SaveChanges();
        }
        public void SaveChanges()
        { 
            _dbContext.SaveChanges();
        }
    }
}
