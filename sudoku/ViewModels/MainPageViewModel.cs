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

        // Variables -----------------------------------------

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

        // Commands ------------------------------------------

        private ICommand _clearLogsCommand;
        public ICommand ClearLogsCommand
        {
            get
            {
                _clearLogsCommand = new Command(ClearLogs);
                return _clearLogsCommand;
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
                _resetGridCommand = new Command(InitSudoku);
                return _resetGridCommand;
            }
        }

        // Construction ----------------------------------------
        

        public MainPageViewModel(IGridService gridService)
        {
            _gridService = gridService;

            InitSudoku();

            // Register Event Handler for this Page
            RegisterPropertyChangedHandler(MainPage_PropertyChanged);

        }

        // Initialization -------------------------------------

        private void InitSudoku()
        {
           
            // Initialize a new Sudo Grid
            InitGrid();

            // more inits ...
        }


        private void InitGrid()
        {

            if (_gridFields != null) {
                // Reset Bindings to unused lists
                GridFields.ResetBindings();
            }

            _gridFields = new BindingList<GridField>();
            _gridFields.ListChanged += GridCollection_PropertyChanged;


            _isGridInitialized = false;

            Log(LogType.Info, "Initalize new sudoku grid");
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

            
            Notify(nameof(GridFields));
        }

        // Calculation --------------------------------------

        private void UpdateStats()
        {
            _emptyFieldsCount = GetStats().ToString();
            Notify(nameof(EmptyFieldsCount));
        }

        private int GetStats()
        {
            return _gridService.GetEmptyFieldsCount(ConvertViewListToModelList(_gridFields));
        }

        // Utils --------------------------------------------

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

        private void Log(string type, string message)
        {
            if (_debugConsole == null) {
                _debugConsole = "";
            }
            _debugConsole = DateTime.Now.ToString("HH:mm:ss") + " [" + type + "] " + message + "\n" + _debugConsole;
            Notify(nameof(DebugConsole));

        }

        private void ClearLogs()
        {

            _debugConsole = "";
            Notify(nameof(DebugConsole));

        }


        // Event Handler ---------------------------------------------

        void MainPage_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {

            if (!e.PropertyName.Equals(nameof(DebugConsole)))
            {

                switch (e.PropertyName)
                {
                    case nameof(GridFields):
                        Log(LogType.Event, "Grid has been changed");
                        break;
                    case nameof(EmptyFieldsCount):
                        Log(LogType.Event, "Empty fields statistic changed");
                        break;
                    default:
                        Log(LogType.Event, e.PropertyName + " has changed");
                        break;
                }


            }

        }

        void GridCollection_PropertyChanged(object sender, ListChangedEventArgs e)
        {

            if (_isGridInitialized)
            {
                if (e.NewIndex > 0 && e.NewIndex <= _gridFields.Count)
                {
                    GridField editedField = _gridFields[e.NewIndex];
                    Log(LogType.Event, "Field(x:" + editedField.X + "/y:" + editedField.Y + ") changed value to " + editedField.Value);
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
        public static string Info { get { return "Info"; } }
    }
}
