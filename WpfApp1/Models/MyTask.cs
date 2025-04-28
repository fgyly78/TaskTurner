using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Shell;

namespace TaskTurner.Models
{
    public class MyTask
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime DueDate { get; set; }
        public DateTime StartDate { get; set; }
        public TimeSpan Timer { get; set; }
        public TaskState TaskState { get; set; }
        public TaskImportance TaskImportance { get; set; }

    }

    public enum TaskState
    {
        InProgress,
        Complete,
        NotStarted,
        Late,
        Archieved,
        Deleted
    }

    public enum TaskImportance
    {
        Low,
        Medium,
        High,
        Critical
    }

}
