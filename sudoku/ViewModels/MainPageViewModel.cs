using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Input;
using sudoku.Models;
using sudoku.ViewModels;
using Xamarin.Forms;

namespace sudoku.viewmodel
{
    public class MainPageViewModel : ViewModelBase
    {

        private readonly IGridService _gridService;

        private bool _isGridInitialized { get; set; }

        private BindingList<GridField> _gridFields;
        public BindingList<GridField> GridFields
        {
            get { return _gridFields; }
            set
            {
                _gridFields = value;
                Notify(nameof(GridFields));
                Notify(nameof(EmptyFieldsCount));
                Log(LogType.Debug, "Grid has been changed");
            }
        }

        private string _emptyFieldsCount { get; set; }
        public string EmptyFieldsCount
        {
            get
            {
                return _emptyFieldsCount;
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
            _gridFields = new BindingList<GridField>();

            _gridFields.ListChanged += GridCollection_PropertyChanged;

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
            _isGridInitialized = false;

            Log(LogType.Debug, "Initalize Sudoku Grid");
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

            _isGridInitialized = true;

            UpdateStats();

            GridFields.ResetBindings();
            Notify(nameof(GridFields));
        }

        private void UpdateStats()
        {
            _emptyFieldsCount = GetStats().ToString();
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

        private void Log(string type, string message) {

            _debugConsole = DateTime.Now.ToString("HH:mm:ss") + " [" + type + "] " + message + "\n" + _debugConsole; 
            Notify(nameof(DebugConsole));

        }

        void MainPage_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {

            if (!e.PropertyName.Equals(nameof(DebugConsole)))
            {
                Log(LogType.Event, e.PropertyName);
            }
            
        }

        void GridCollection_PropertyChanged(object sender, ListChangedEventArgs e)
        {

            if (_isGridInitialized)
            {
                if (e.NewIndex > 0 && e.NewIndex <= _gridFields.Count) {
                    GridField editedField = _gridFields[e.NewIndex];
                    Log(LogType.Event, "x: " + editedField.X + " y: " + editedField.Y + " val: " + editedField.Value);
                    UpdateStats();
                }
               
            }

        }

    }

    /// <summary>
    /// Log Types used in Debug Console
    /// </summary>
    public static class LogType
    {
        public static string Event { get { return "Event"; } }
        public static string Debug { get { return "Debug"; } }
    }
}
