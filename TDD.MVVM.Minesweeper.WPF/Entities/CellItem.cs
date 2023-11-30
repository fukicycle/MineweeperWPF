using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using TDD.MVVM.Minesweeper.WPF.Commands;
using TDD.MVVM.Minesweeper.WPF.ViewModels;

namespace TDD.MVVM.Minesweeper.WPF.Entities
{
    public class CellItem : ViewModelBase
    {
        private readonly INotifyOpened _notifyOpened;
        public CellItem(int index, string content, State state, CellType cellType, INotifyOpened notifyOpened)
        {
            _notifyOpened = notifyOpened;
            Index = index;
            Content = content;
            State = state;
            Type = cellType;
        }
        public int Index { get; }
        private string _content = string.Empty;
        public string Content
        {
            get { return _content; }
            set
            {
                SetProperty(ref _content, value);
            }
        }
        private State _state = State.INITIAL;
        public State State
        {
            get { return _state; }
            set
            {
                if (SetProperty(ref _state, value))
                {
                    CheckOpenedItem();
                }
            }
        }
        private CellType _cellType = CellType.NOT_BOM;
        public CellType Type
        {
            get { return _cellType; }
            set
            {
                SetProperty(ref _cellType, value);
            }
        }

        private void CheckOpenedItem()
        {
            if (State == State.OPEN)
            {
                _notifyOpened.OnOpened(this);
            }
        }
    }
}
