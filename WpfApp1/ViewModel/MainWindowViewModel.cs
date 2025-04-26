using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
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
        private ICommand _addTaskCommand;
        private ObservableCollection<Task> _tasks;

        

        public int Id { get => _id; private set => Set(ref _id, value); }
        public string NewTaskTitle { get => _title; set => Set(ref _title, value); }
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

        public ObservableCollection<Task> Tasks
        {
            get => _tasks;
            set => Set(ref _tasks, value);
        }

        public ICommand IOpenNewWindow { get; }
        public MainWindowViewModel()
        {
            IOpenNewWindow = new OpenNewWindowCommand();

            Tasks = new ObservableCollection<Task>();

            LoadTasks();

        }

        private void LoadTasks()
        {
            using (var db = new TaskManagerContext())
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
            var newTask = new Task { Title = NewTaskTitle };

            using (var db = new TaskManagerContext())
            {
                db.Tasks.Add(newTask);
                db.SaveChanges(); 
            }

            Tasks.Add(newTask);     

            NewTaskTitle = string.Empty;
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
