using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using TaskTurner.Models;
using TaskTurner.Views;



namespace TaskTurner.ViewModel
{
    public class MainWindowViewModel : INotifyPropertyChanged
    {

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

        public event PropertyChangedEventHandler PropertyChanged;

        public ICommand IOpenWindow => new RelayCommand(OpenNewWindow);

        private void OpenNewWindow()
        {
            NewTaskWindow newTaskWindow = new NewTaskWindow();
            newTaskWindow.Show();
        }

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            this.OnPropertyChanged();
        }
    }
}
