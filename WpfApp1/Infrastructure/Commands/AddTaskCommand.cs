using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using TaskTurner.Models;
using TaskTurner.ViewModel;

namespace TaskTurner.Infrastructure.Commands
{
    internal class AddTaskCommand : RelayCommand
    {
        private readonly MainWindowViewModel _vm;

        public AddTaskCommand(MainWindowViewModel vm) : base(
            execute: () => ExecuteAdd(vm),
            canExecute: () => CanExecuteAdd(vm))
        {
            _vm = vm;
            vm.PropertyChanged += (s, e) =>
            {
                if (e.PropertyName == nameof(MainWindowViewModel.NewTaskTitle))
                    CommandManager.InvalidateRequerySuggested();
            };
        }

        private static void ExecuteAdd(MainWindowViewModel vm)
        {
            var task = new MyTask
            {
                Title = vm.NewTaskTitle,
                Description = vm.NewTaskDescription,
                DueDate = DateTime.SpecifyKind(vm.NewTaskDueDate, DateTimeKind.Utc),
                StartDate = DateTime.SpecifyKind(vm.NewTaskStartDate, DateTimeKind.Utc),
                Timer = vm.NewTaskTimer,
                TaskState = vm.NewTaskState,
                TaskImportance = vm.NewTaskImportance
            };

            using var db = new ApplicationContext();
            db.Tasks.Add(task);
            db.SaveChanges();

            vm.Tasks.Add(task);
            vm.NewTaskTitle = string.Empty;
            CommandManager.InvalidateRequerySuggested();
        }

        private static bool CanExecuteAdd(MainWindowViewModel vm)
            => !string.IsNullOrWhiteSpace(vm.NewTaskTitle);
    }
}
