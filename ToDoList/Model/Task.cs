using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToDoList.Model
{
    public class Task
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public PriorityLevel Priority { get; set; }
        public DateTime Deadline { get; set; }
        public TaskStatus Status { get; set; }
    }

    public enum PriorityLevel
    {
        Low,
        Medium,
        High
    }

    public enum TaskStatus
    {
        ToDo,
        InProgress,
        Done
    }
}
