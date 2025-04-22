using System;
using System.Collections.Generic;
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
        public ICommand IOpenNewWindow { get; }
        public MainWindowViewModel()
        {
            IOpenNewWindow = new OpenNewWindowCommand();
        }

        #region Properties

        #region Animation of Imprtance badge in NewTaskWindow

        private TaskImportance _selectedTaskImportance;

        public TaskImportance SelectedTaskImportance
        {
            get => _selectedTaskImportance;
            set
            {
                if (_selectedTaskImportance != value)
                {
                    _selectedTaskImportance = value;
                    OnPropertyChanged(nameof(SelectedTaskImportance));
                }
            }
        }

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
