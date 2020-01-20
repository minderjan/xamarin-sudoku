using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Input;
using sudoku.Controls;
using sudoku.Models;
using sudoku.ViewModels;
using Xamarin.Forms;

namespace sudoku.viewmodel
{
    public class MainPageViewModel : ViewModelBase
    {

        private readonly IGridService _gridService;

        private DeepObservableCollection<GridField> _gridFields;
        public DeepObservableCollection<GridField> GridFields
        {
            get { return _gridFields; }
            set
            {
                _gridFields = value;
                Notify(nameof(GridFields));
                Notify(nameof(EmptyFieldsCount));

                LogEvent("grid item has been changed");
                
            }
        }

        public string EmptyFieldsCount
        {
            get
            {
                LogEvent("Updated Statistics");
                return GetStats().ToString();
            }
        }

        private string _debugConsole { get; set; }
        public string DebugConsole
        {
            get { return _debugConsole; }
            set
            {
                _debugConsole = value;
            }
        }


        private ICommand _triggerGridEventCommand;
        public ICommand TriggerGridEventCommand
        {
            get
            {
                _triggerGridEventCommand = new Command(UpdateStats);
                return _triggerGridEventCommand;
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
            _gridFields = new DeepObservableCollection<GridField>();

            
            InitSudoku();

            // Register Event Handler for this Page
            RegisterPropertyChangedHandler(MainPage_PropertyChanged);

        }

        private void InitSudoku()
        {
            // Initialize a new Sudo Grid
            InitGrid();
        }


        private void InitGrid()
        {
            LogEvent("Initalize Sudoku Grid");
            _gridFields.Clear();

            // initialize a new grid of fields (including predefined ones)
            foreach (Field field in _gridService.GenerateInitialGrid())
            {
                if (field.Value != 0)
                {
                    _gridFields.Add(new GridField(field, false));
                }
                else
                {
                    _gridFields.Add(new GridField(field, true));
                }

            }
        }

        private void UpdateStats()
        {
            Notify(nameof(EmptyFieldsCount));
        }

        private int GetStats()
        {
            return _gridService.GetEmptyFieldsCount(ConvertViewListToModelList(_gridFields));
        }

        private List<Field> ConvertViewListToModelList(IEnumerable<GridField> gridFields)
        {

            List<Field> fields = new List<Field>();

            foreach (GridField gf in gridFields)
            {

                int value = 0;
                try
                {
                    value = Int32.Parse(gf.Value);
                }
                catch (FormatException)
                {
                    // Invalid Format will be ignored
                }

                fields.Add(new Field(gf.X, gf.Y, value));
            }

            return fields;
        }

        private void LogEvent(string message) {

            _debugConsole = message + "\n" + _debugConsole; 
            Notify(nameof(DebugConsole));

        }

        void MainPage_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {

            if (!e.PropertyName.Equals(nameof(DebugConsole))) {
                LogEvent("[Event] " + e.PropertyName);
            }
            
        }

    }
}
