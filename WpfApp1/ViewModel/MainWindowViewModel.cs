using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Windows.Input;
using TaskTurner.Infrastructure.Commands;
using TaskTurner.Models;
using TaskTurner.Views;

namespace TaskTurner.ViewModel
{
    internal class MainWindowViewModel : ViewModel
    {
        public MainWindowViewModel()
        {
            Tasks = new ObservableCollection<MyTask>();
            IOpenNewWindow = new OpenNewWindowCommand();
            DeleteTaskCommand = new DeleteTaskCommand(this);

            LoadTasks();
            ResetTaskFields();
        }

        #region Commands
        public ICommand AddTaskCommand => _addTaskCommand ??= new AddTaskCommand(this);
        public ICommand DeleteTaskCommand { get; }
        public ICommand IOpenNewWindow { get; }
        #endregion

        #region Service
        private void LoadTasks()
        {
            using var db = new ApplicationContext();
            Tasks = new ObservableCollection<MyTask>(db.Tasks.ToList());
        }

        internal void ResetTaskFields()
        {
            NewTaskTitle = string.Empty;
            NewTaskDescription = string.Empty;
            NewTaskDueDate = DateTime.Now;
            NewTaskStartDate = DateTime.Now;
            NewTaskTimer = TimeSpan.Zero;
            NewTaskState = TaskState.NotStarted;
            NewTaskImportance = TaskImportance.Default;
        }

        public void DeleteSelectedTask()
        {
            if (SelectedTask != null)
            {
                using (var context = new ApplicationContext())
                {
                    context.Tasks.Remove(SelectedTask);
                    context.SaveChanges();
                }

                Tasks.Remove(SelectedTask);

                SelectedTask = null;
            }
        }

        #endregion

        #region Fields
        private string _title;
        private string _description;
        private DateTime _dueDate;
        private DateTime _startDate;
        private TimeSpan _timer;
        private TaskState _taskState;
        private TaskImportance _taskImportance;
        private ObservableCollection<MyTask> _tasks;
        private MyTask _selectedTask;
        private ICommand _addTaskCommand;
        private DateTime _defaultTime = DateTime.Now;
        
        #endregion

        #region Properties
        public string NewTaskTitle { get => _title; set => Set(ref _title, value); }
        public string NewTaskDescription { get => _description; set => Set(ref _description, value); }
        public DateTime NewTaskDueDate { get => _dueDate; set => Set(ref _dueDate, value); }
        public DateTime NewTaskStartDate { get => _startDate; set => Set(ref _startDate, value); }
        public TimeSpan NewTaskTimer { get => _timer; set => Set(ref _timer, value); }
        public TaskState NewTaskState { get => _taskState; set => Set(ref _taskState, value); }
        public TaskImportance NewTaskImportance { get => _taskImportance; set => Set(ref _taskImportance, value); }
        public DateTime DefaultTime
        {
            get => _defaultTime;
            set => Set(ref _defaultTime, value);
        }

        public ObservableCollection<MyTask> Tasks { get => _tasks; set => Set(ref _tasks, value); }

        public MyTask SelectedTask
        {
            get => _selectedTask;
            set => Set(ref _selectedTask, value);
        }

        public IEnumerable<TaskImportance> ImportanceOptions =>
    Enum.GetValues(typeof(TaskImportance)).Cast<TaskImportance>();
        #endregion
    }
}
