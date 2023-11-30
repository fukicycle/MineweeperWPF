using TDD.MVVM.Minesweeper.WPF;
using TDD.MVVM.Minesweeper.WPF.Entities;
using TDD.MVVM.Minesweeper.WPF.ViewModels;
namespace TDD.MVVM.Minesweeper.Test
{
    [TestClass]
    public class UnitTest1
    {

        [TestMethod]
        public void MainWindowの初期化処理()
        {
            MainWindowViewModel viewModel = new MainWindowViewModel();
            Assert.AreEqual(MainWindowViewModelHelper.ROW_COUNT + MainWindowViewModelHelper.MARGIN, viewModel.MarginRowCount);
            Assert.AreEqual(MainWindowViewModelHelper.COLUMN_COUNT + MainWindowViewModelHelper.MARGIN, viewModel.MarginColumnCount);
            Assert.AreEqual(viewModel.MarginRowCount * viewModel.MarginColumnCount, viewModel.CellList.Count());
            Assert.AreEqual(MainWindowViewModelHelper.ROW_COUNT * MainWindowViewModelHelper.COLUMN_COUNT * MainWindowViewModelHelper.BOM_COUNT_RATIO, viewModel.BomCount);
            Assert.AreEqual(0, viewModel.CellList.Count(a => a.Type == CellType.BOM));
            Assert.AreEqual("", viewModel.StatusText);
            Assert.AreEqual(true, viewModel.IsFirstOpen);
        }

        [TestMethod]
        public void 初回クリック時に爆弾を指定数配置する()
        {
            MainWindowViewModel viewModel = new MainWindowViewModel();
            CellItem cellItem = new CellItem(1, "", State.INITIAL, CellType.NOT_BOM, new TestHelper());
            viewModel.OpenCommand.Execute(cellItem);
            Assert.AreEqual(MainWindowViewModelHelper.ROW_COUNT * MainWindowViewModelHelper.COLUMN_COUNT * MainWindowViewModelHelper.BOM_COUNT_RATIO, viewModel.BomCount);
            Assert.AreEqual(viewModel.BomCount, viewModel.CellList.Count(a => a.Type == CellType.BOM));
        }

        [TestMethod]
        public void リセット時に初期化する()
        {
            MainWindowViewModel viewModel = new MainWindowViewModel();
            CellItem cellItem = new CellItem(1, "", State.INITIAL, CellType.NOT_BOM, new TestHelper());
            viewModel.OpenCommand.Execute(cellItem);
            viewModel.ResetCommand.Execute(null);
            Assert.AreEqual(MainWindowViewModelHelper.ROW_COUNT + MainWindowViewModelHelper.MARGIN, viewModel.MarginRowCount);
            Assert.AreEqual(MainWindowViewModelHelper.COLUMN_COUNT + MainWindowViewModelHelper.MARGIN, viewModel.MarginColumnCount);
            Assert.AreEqual(viewModel.MarginRowCount * viewModel.MarginColumnCount, viewModel.CellList.Count());
            Assert.AreEqual(MainWindowViewModelHelper.ROW_COUNT * MainWindowViewModelHelper.COLUMN_COUNT * MainWindowViewModelHelper.BOM_COUNT_RATIO, viewModel.BomCount);
            Assert.AreEqual(0, viewModel.CellList.Count(a => a.Type == CellType.BOM));
            Assert.AreEqual("", viewModel.StatusText);
            Assert.AreEqual(true, viewModel.IsFirstOpen);
        }

        [TestMethod]
        public void セル作成と爆弾作成が正しい()
        {
            MainWindowViewModel viewModel = new MainWindowViewModel();
            viewModel.CreateCell();
            Assert.AreEqual(viewModel.MarginRowCount * viewModel.MarginColumnCount, viewModel.CellList.Count());
            Assert.AreEqual(viewModel.CellList.Count() - (MainWindowViewModelHelper.ROW_COUNT * MainWindowViewModelHelper.COLUMN_COUNT), viewModel.CellList.Count(a => a.Type == CellType.OUT_OF_RANGE));
            Assert.AreEqual(0, viewModel.CellList.Count(a => a.Type == CellType.BOM));
            CellItem cellItem = new CellItem(1, "", State.INITIAL, CellType.NOT_BOM, new TestHelper());
            AroundHelper helper = new AroundHelper(viewModel.CellList, viewModel.MarginColumnCount);
            viewModel.CreateBom(cellItem, helper.Get(cellItem));
            Assert.AreEqual(viewModel.BomCount, viewModel.CellList.Count(a => a.Type == CellType.BOM));
        }
    }
    public class TestHelper : INotifyOpened
    {
        public void OnOpened(CellItem cellItem)
        {
            //no action
        }
    }
}