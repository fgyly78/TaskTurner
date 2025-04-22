using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows;
using TaskTurner.ViewModel;
using TaskTurner.Views;

namespace TaskTurner.Infrastructure.Commands
{
    internal class OpenNewWindowCommand : RelayCommand
    {
        private Window _openedWindow;

        public override bool CanExecute(object parameter)
        {
            return _openedWindow == null || !_openedWindow.IsVisible;
        }

        public override void Execute(object parameter)
        {
            if (_openedWindow == null || !_openedWindow.IsVisible)
            {
                _openedWindow = new NewTaskWindow();
                _openedWindow.Closed += (s, e) => CommandManager.InvalidateRequerySuggested(); // Обновить CanExecute
                _openedWindow.Show();

                // Уведомляем систему, что статус CanExecute мог измениться
                CommandManager.InvalidateRequerySuggested();
            }
        }
    }
}
