using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using TaskTurner.ViewModel;

namespace TaskTurner.Infrastructure.Commands
{
    internal class DeleteTaskCommand : RelayCommand
    {
        private readonly MainWindowViewModel _viewModel;

        public DeleteTaskCommand(MainWindowViewModel viewModel)
        {
            _viewModel = viewModel ?? throw new ArgumentNullException(nameof(viewModel));
        }

        public override bool CanExecute(object parameter)
        {
            // Можем удалить только если задача выбрана
            return _viewModel.SelectedTask != null;
        }

        public override void Execute(object parameter)
        {
            _viewModel.DeleteSelectedTask();
        }
    }

}
