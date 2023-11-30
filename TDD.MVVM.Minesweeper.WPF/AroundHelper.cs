using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TDD.MVVM.Minesweeper.WPF.Entities;

namespace TDD.MVVM.Minesweeper.WPF
{
    public class AroundHelper
    {

        public AroundHelper(ObservableCollection<CellItem> cellList, int marginColumnCount)
        {
            _marginColumnCount = marginColumnCount;
            _cellList = cellList;
        }

        private ObservableCollection<CellItem> _cellList;
        private int _marginColumnCount;

        public void OpenAll(CellItem cellItem)
        {
            if (cellItem.Type == CellType.BOM)
            {
                cellItem.State = State.OPEN;
            }
            if (cellItem.State == State.INITIAL)
            {
                cellItem.State = State.OPEN;
                CellItem[] cellItems = Get(cellItem);
                int numberOfBom = cellItems.Count(a => a.Type == CellType.BOM);
                if (numberOfBom > 0)
                {
                    cellItem.Content = numberOfBom.ToString();
                }
                else
                {
                    cellItem.Content = string.Empty;
                    for (int i = 0; i < cellItems.Length; i++)
                    {
                        CellItem aroundItem = cellItems[i];
                        if (aroundItem.State == State.INITIAL && aroundItem.Type == CellType.NOT_BOM)
                        {
                            OpenAll(aroundItem);
                        }
                    }
                }
            }
        }

        public CellItem[] Get(CellItem cellItem)
        {
            int index = cellItem.Index;
            CellItem[] cellItems = new CellItem[8];
            //top
            cellItems[0] = GetCellItemFromIndex(index - _marginColumnCount);
            cellItems[1] = GetCellItemFromIndex(index - _marginColumnCount - 1);
            cellItems[2] = GetCellItemFromIndex(index - _marginColumnCount + 1);
            //middle                           
            cellItems[3] = GetCellItemFromIndex(index - 1);
            cellItems[4] = GetCellItemFromIndex(index + 1);
            //bottom                           
            cellItems[5] = GetCellItemFromIndex(index + _marginColumnCount);
            cellItems[6] = GetCellItemFromIndex(index + _marginColumnCount - 1);
            cellItems[7] = GetCellItemFromIndex(index + _marginColumnCount + 1);
            return cellItems;
        }

        private CellItem GetCellItemFromIndex(int index)
        {
            try
            {
                return _cellList[index];
            }
            catch (Exception)
            {
                return new CellItem(index, "", State.INITIAL, CellType.OUT_OF_RANGE, null);
            }
        }
    }
}
