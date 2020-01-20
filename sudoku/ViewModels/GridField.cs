using System;
using sudoku.Models;

namespace sudoku.ViewModels
{
    public class GridField : ViewModelBase
    {

        private int _x { get; set; }
        public int X {
            get {
                return _x;
            }
            set {
                _x = value;
            }
        }

        private int _y { get; set; }
        public int Y
        {
            get
            {
                return _y;
            }
            set
            {
                _y = value;
            }
        }


        public Boolean IsEditable { get; set; }

        private string _value { get; set; }
        public string Value
        {

            get
            {

                string val = _value.ToString();

                if (val.Equals("0"))
                {
                    return "";
                }

                return val;
            }

            set
            {
                try
                {
                    int result = Int32.Parse(value);
                    //Notify("Value");
                }
                catch (FormatException)
                {
                    // Invalid Format will be ignored
                }
            }

        }

        public GridField() {}

        public GridField(Field field, Boolean isEditable)
        {
            _value = field.Value.ToString();
            IsEditable = isEditable;
            _x = field.X;
            _y = field.Y;
        }
       
    }
}
