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

        private int _id;
        private string _title;
        private string _description;
        private DateTime _dueDate;
        private DateTime _startDate;
        private TimeSpan _timer;
        private TaskState _taskState;
        private TaskImportance _taskImportance;
        private ICommand _addTaskCommand;

        private ObservableCollection<MyTask> _tasks;

        

        public int Id { get => _id; private set => Set(ref _id, value); }
        public string NewTaskTitle { get => _title; set => Set(ref _title, value); }
        public string NewTaskDescription { get => _description; set => Set(ref _description, value); }
        public DateTime NewTaskDueDate { get => _dueDate; set => Set(ref _dueDate, value); }
        public DateTime NewTaskStartDate { get => _startDate; set => Set(ref _startDate, value); }
        public TimeSpan NewTaskTimer { get => _timer; set => Set(ref _timer, value); }
        public TaskState NewTaskState { get => _taskState; set => Set(ref _taskState, value); }
        public TaskImportance NewTaskImportance { get => _taskImportance; set => Set(ref _taskImportance, value); }

        public ICommand AddTaskCommand
        {
            get 
            {
                if (_addTaskCommand == null)
                {
                    _addTaskCommand = new RelayCommand(
                        execute: () => AddTaskToDatabase(),
                        canExecute: () => CanAddTask()
                    );
                }
                return _addTaskCommand;
            }
        }

        public ObservableCollection<MyTask> Tasks
        {
            get => _tasks;
            set => Set(ref _tasks, value);
        }

        public ICommand IOpenNewWindow { get; }
        public MainWindowViewModel()
        {
            IOpenNewWindow = new OpenNewWindowCommand();

            Tasks = new ObservableCollection<MyTask>();

            NewTaskDueDate = DateTime.Now;
            NewTaskStartDate = DateTime.Now;
            NewTaskTimer = TimeSpan.Zero;
            NewTaskState = TaskState.NotStarted;
            NewTaskImportance = TaskImportance.Low;

            LoadTasks();

        }

        private void LoadTasks()
        {
            using (var db = new ApplicationContext())
            {
                var tasksFromDb = db.Tasks.ToList();
                foreach (var task in tasksFromDb)
                {
                    Tasks.Add(task);
                }
            }
        }

        public void AddTaskToDatabase()
        {
            var newTask = new MyTask 
            { 
                Title = NewTaskTitle, 
                Description = NewTaskDescription,
                DueDate = DateTime.SpecifyKind(NewTaskDueDate, DateTimeKind.Utc),
                StartDate = DateTime.SpecifyKind(NewTaskStartDate, DateTimeKind.Utc),
                Timer = NewTaskTimer,
                TaskState = NewTaskState,
                TaskImportance = NewTaskImportance
            };

            using (var db = new ApplicationContext())
            {
                db.Tasks.Add(newTask);
                db.SaveChanges();
            }

            Tasks.Add(newTask);

            NewTaskTitle = string.Empty;
            NewTaskDescription = string.Empty;
            NewTaskDueDate = DateTime.Now;
            NewTaskStartDate = DateTime.Now;
            NewTaskTimer = TimeSpan.Zero;
            NewTaskState = TaskState.NotStarted;
            NewTaskImportance = TaskImportance.Low;
        }



        private bool CanAddTask()
        {
            return !string.IsNullOrWhiteSpace(NewTaskTitle);
        }



        #region Properties

        #region Animation of Imprtance badge in NewTaskWindow

        //private TaskImportance _selectedTaskImportance;

        //public TaskImportance SelectedTaskImportance
        //{
        //    get => _selectedTaskImportance;
        //    set
        //    {
        //        if (_selectedTaskImportance != value)
        //        {
        //            _selectedTaskImportance = value;
        //            OnPropertyChanged(nameof(SelectedTaskImportance));
        //        }
        //    }
        //}

        #endregion

        #region Update time

        private DateTime? _myTime = DateTime.Now;
        public DateTime? MyTime
        {
            get => _myTime;
            set
            {
                _myTime = value;
                OnPropertyChanged(); // если есть INotifyPropertyChanged
            }
        }

        #endregion

        #endregion

    }
}
