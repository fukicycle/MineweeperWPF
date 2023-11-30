using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using TDD.MVVM.Minesweeper.WPF.ViewModels;

namespace TDD.MVVM.Minesweeper.WPF.Commands
{
    public class ResetCommand : ICommand
    {
        private readonly MainWindowViewModel _viewModel;
        public ResetCommand(MainWindowViewModel viewModel)
        {
            _viewModel = viewModel;
            _viewModel.PropertyChanged += (s, e) => CanExecuteChanged?.Invoke(s, e);
        }

        public event EventHandler? CanExecuteChanged;

        public bool CanExecute(object? parameter)
        {
            return !string.IsNullOrEmpty(_viewModel.StatusText);
        }

        public void Execute(object? parameter)
        {
            _viewModel.StatusText = string.Empty;
            _viewModel.CreateCell();
            _viewModel.IsFirstOpen = true;
        }
    }
}
