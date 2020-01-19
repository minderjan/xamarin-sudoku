using System;
namespace sudoku.Models
{
    public class Field
    {
        public int X { get; set; }
        public int Y { get; set; }
        public int Value { get; set; }
        public Boolean IsEditable { get; set; }

        public Field()
        {
            X = 0;
            Y = 0;
            IsEditable = true;
        }

        public Field(int x, int y) {
            X = x;
            Y = y;
            IsEditable = true;
        }

        public Field(int x, int y, int value)
        {
            X = x;
            Y = y;
            Value = value;
            IsEditable = false;
        }
    }
}
