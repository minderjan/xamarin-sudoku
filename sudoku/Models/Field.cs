using System;
using System.ComponentModel;

namespace sudoku.Models
{
    public class Field
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


        public int _value { get; set; }
        public int Value
        {
            get
            {
                return _value;
            }
            set
            {
                _value = value;
            }
        }

        public Field()
        {
            _x = 0;
            _y = 0;
        }

        public Field(int x, int y) {
            _x = x;
            _y = y;
        }

        public Field(int x, int y, int value)
        {
            _x = x;
            _y = y;
            _value = value;
        }
    }

}
