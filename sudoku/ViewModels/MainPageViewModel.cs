using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Input;
using sudoku.Models;
using Xamarin.Forms;

namespace sudoku.viewmodel
{
    public class MainPageViewModel : ViewModelBase
    {

        private readonly IGridService _gridService;

        private ObservableCollection<Row> _gridFields;
        public ObservableCollection<Row> GridFields
        {
            get { return _gridFields; }
            set
            {
                _gridFields = value;
            }
        }


        private ICommand _resetGridCommand;
        public ICommand ResetGridCommand
        {
            get
            {
                _resetGridCommand = new Command(InitGrid);
                return _resetGridCommand;
            }
        }

      
        public MainPageViewModel(IGridService gridService)
        {
            _gridService = gridService;
            _gridFields = new ObservableCollection<Row>();

            InitSudoku();
        }

        private void InitSudoku()
        {
            // Initialize a new Sudo Grid
            InitGrid();
        }


        private void InitGrid()
        {
            _gridFields.Clear();

            foreach (var row in _gridService.GenerateInitialGrid())
            {
                _gridFields.Add(row);
            }
        }

    }
}
