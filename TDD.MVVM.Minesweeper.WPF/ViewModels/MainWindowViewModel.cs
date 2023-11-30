using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using TDD.MVVM.Minesweeper.WPF.Commands;
using TDD.MVVM.Minesweeper.WPF.Entities;

namespace TDD.MVVM.Minesweeper.WPF.ViewModels
{
    public class MainWindowViewModel : ViewModelBase, INotifyOpened
    {
        public MainWindowViewModel()
        {
            OpenCommand = new OpenCommand(this);
            ResetCommand = new ResetCommand(this);
            FlagCommand = new FlagCommand();
            CreateCell();
        }
        public bool IsFirstOpen { get; set; } = true;



        public int MarginRowCount
        {
            get
            {
                return MainWindowViewModelHelper.ROW_COUNT + MainWindowViewModelHelper.MARGIN;
            }
        }


        public int MarginColumnCount
        {
            get
            {
                return MainWindowViewModelHelper.COLUMN_COUNT + MainWindowViewModelHelper.MARGIN;
            }
        }

        private ObservableCollection<CellItem> _cellList = new ObservableCollection<CellItem>();
        public ObservableCollection<CellItem> CellList
        {
            get { return _cellList; }
        }

        public void CreateCell()
        {
            _cellList.Clear();
            for (int index = 0; index < MarginRowCount * MarginColumnCount; index++)
            {
                CellType cellType = CellType.NOT_BOM;
                if (index / MarginColumnCount == 0 || index / MarginColumnCount == MarginRowCount - 1 || index % MarginColumnCount == 0 || (index + 1) % MarginColumnCount == 0)
                {
                    cellType = CellType.OUT_OF_RANGE;
                }
                CellItem cellItem = new CellItem(index, "", State.INITIAL, cellType, this);
                _cellList.Add(cellItem);
            }
        }

        public void OnOpened(CellItem cellItem)
        {
            if (cellItem.Type == CellType.BOM)
            {
                _cellList.Where(a => a.Type == CellType.BOM).ToList().ForEach(a => a.State = State.OPEN);
                StatusText = "Game over!";
            }
            else
            {
                if (MainWindowViewModelHelper.ROW_COUNT * MainWindowViewModelHelper.COLUMN_COUNT - BomCount == _cellList.Count(a => a.State == State.OPEN))
                {
                    StatusText = "Clear!!!";
                }
            }
        }

        public void CreateBom(CellItem cellItem, CellItem[] aroundItems)
        {
            Random r = new Random();
            List<int> indexes = new List<int>();
            indexes.Add(cellItem.Index);
            indexes.AddRange(aroundItems.Select(a => a.Index));
            for (int i = 0; i < BomCount; i++)
            {
                CellItem targetCellItem = _cellList[r.Next(0, _cellList.Count - 1)];
                while (indexes.Contains(targetCellItem.Index) || targetCellItem.Type == CellType.OUT_OF_RANGE)
                {
                    targetCellItem = _cellList[r.Next(0, _cellList.Count - 1)];
                }
                targetCellItem.Type = CellType.BOM;
                indexes.Add(targetCellItem.Index);
            }
        }

        public int BomCount
        {
            get
            {
                return Convert.ToInt32(Math.Floor(MainWindowViewModelHelper.ROW_COUNT * MainWindowViewModelHelper.COLUMN_COUNT * MainWindowViewModelHelper.BOM_COUNT_RATIO));
            }
        }
        private string _statusText = string.Empty;
        public string StatusText
        {
            get { return _statusText; }
            set
            {
                SetProperty(ref _statusText, value);
            }
        }
        public ICommand OpenCommand { get; }

        public ICommand ResetCommand { get; }

        public ICommand FlagCommand { get; set; }

    }
}
