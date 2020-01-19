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

        private ObservableCollection<RowGroup> _gridFields;
        public ObservableCollection<RowGroup> GridFields
        {
            get { return _gridFields; }
            set
            {
                _gridFields = value;
            }
        }


        private ICommand _addElementCommand;
        public ICommand AddElementCommand
        {
            get
            {
                return new Command<string>(CalculatorCommand);
            }
        }

        /**
         * Constructor
         */
        public MainPageViewModel()
        {
            _gridFields = new ObservableCollection<RowGroup>();
            InitData();
        }

        private void InitData()
        {

            _gridFields.Clear();



            /*
            row.Field1 = generateField();
            row.Field2 = generateField();
            row.Field3 = generateField();
            row.Field4 = generateField();
            row.Field5 = generateField();
            row.Field6 = generateField();
            row.Field7 = generateField();
            row.Field8 = generateField();
            row.Field9 = generateField();

            _gridFields.Add(row);
            _gridFields.Add(row);
            _gridFields.Add(row);
            _gridFields.Add(row);
            _gridFields.Add(row);
            _gridFields.Add(row);
            _gridFields.Add(row);
            _gridFields.Add(row);
            _gridFields.Add(row);
            */

            _gridFields.Add(generateRow(1));
            _gridFields.Add(generateRow(2));
            _gridFields.Add(generateRow(3));
            _gridFields.Add(generateRow(4));
            _gridFields.Add(generateRow(5));
            _gridFields.Add(generateRow(6));
            _gridFields.Add(generateRow(7));
            _gridFields.Add(generateRow(8));
            _gridFields.Add(generateRow(9));

        }

        private Field generateField(int x, int y)
        {
            return new Field(x, y);
        }

        private RowGroup generateRow(int y)
        {

            List<Field> fields = new List<Field>();

            fields.Add(generateField(1, y));
            fields.Add(generateField(2, y));
            fields.Add(generateField(3, y));
            fields.Add(generateField(4, y));
            fields.Add(generateField(5, y));
            fields.Add(generateField(6, y));
            fields.Add(generateField(7, y));
            fields.Add(generateField(8, y));
            fields.Add(generateField(9, y));

            return new RowGroup(fields);

        }

        private void CalculatorCommand(string args)
        {

        }

    }
}
