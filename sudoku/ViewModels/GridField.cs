using System;
using sudoku.Models;

namespace sudoku.ViewModels
{
    public class GridField : ViewModelBase
    {

        private int _x { get; set; }
        public int X
        {
            get
            {
                return _x;
            }
            set
            {
                _x = value;
                Notify(nameof(X));
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
                Notify(nameof(Y));
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
                int val;

                try
                {
                    val = Int32.Parse(value);
                }
                catch (Exception e)
                {
                    val = 0;
                }

                // validation
                if (val > 0 && val <= 9)
                {
                    _value = value;
                }
                else
                {
                    _value = "0";
                }


                Notify(nameof(Value));
                Notify("DummyEvent");
            }

        }

        public GridField() { }

        public GridField(int x, int y, string value)
        {
            _x = x;
            _y = y;
            _value = value;
            IsEditable = false;
        }

        public GridField(Field field, Boolean isEditable)
        {
            _value = field.Value.ToString();
            IsEditable = isEditable;
            _x = field.X;
            _y = field.Y;
        }

    }
}
