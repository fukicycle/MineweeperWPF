using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using TDD.MVVM.Minesweeper.WPF.Entities;
using TDD.MVVM.Minesweeper.WPF.ViewModels;

namespace TDD.MVVM.Minesweeper.WPF.Commands
{
    public class FlagCommand : ICommand
    {
        public event EventHandler? CanExecuteChanged;

        public bool CanExecute(object? parameter)
        {
            CellItem cellItem = parameter as CellItem ?? throw new ArgumentNullException(nameof(parameter), "Can not null parameter");
            return cellItem.State == State.INITIAL || cellItem.State == State.FLAG;
        }

        public void Execute(object? parameter)
        {
            CellItem cellItem = parameter as CellItem ?? throw new ArgumentNullException(nameof(parameter), "Can not null parameter");
            if (cellItem.State == State.FLAG)
            {
                cellItem.State = State.INITIAL;
            }
            else
            {
                cellItem.State = State.FLAG;
            }
        }
    }
}
