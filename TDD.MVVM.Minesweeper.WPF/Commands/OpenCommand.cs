using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using TDD.MVVM.Minesweeper.WPF.Entities;
using TDD.MVVM.Minesweeper.WPF.ViewModels;

namespace TDD.MVVM.Minesweeper.WPF.Commands
{
    public class OpenCommand : ICommand
    {
        private readonly MainWindowViewModel _viewModel;
        private readonly AroundHelper _aroundHelper;
        public OpenCommand(MainWindowViewModel viewModel)
        {
            _viewModel = viewModel;
            _viewModel.PropertyChanged += (s, e) => CanExecuteChanged?.Invoke(s, e);
            _aroundHelper = new AroundHelper(_viewModel.CellList, _viewModel.MarginColumnCount);
        }

        public event EventHandler? CanExecuteChanged;

        public bool CanExecute(object? parameter)
        {
            CellItem cellItem = parameter as CellItem ?? throw new ArgumentNullException(nameof(parameter), "Can not null parameter");
            return cellItem.State == State.INITIAL;
        }

        public void Execute(object? parameter)
        {
            CellItem cellItem = parameter as CellItem ?? throw new ArgumentNullException(nameof(parameter), "Can not null parameter");
            if (_viewModel.IsFirstOpen)
            {
                _viewModel.CreateBom(cellItem, _aroundHelper.Get(cellItem));
            }
            _viewModel.IsFirstOpen = false;
            _aroundHelper.OpenAll(cellItem);
        }
    }
}
