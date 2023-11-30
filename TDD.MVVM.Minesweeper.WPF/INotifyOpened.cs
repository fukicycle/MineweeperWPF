using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TDD.MVVM.Minesweeper.WPF.Entities;

namespace TDD.MVVM.Minesweeper.WPF
{
    public interface INotifyOpened
    {
        void OnOpened(CellItem cellItem);
    }
}
