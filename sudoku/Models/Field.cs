using System;
namespace sudoku.Models
{
    public class Field
    {
        public int X { get; set; }
        public int Y { get; set; }
        public int Value { get; set; }

        public Field()
        {
            X = 0;
            Y = 0;
       
        }

        public Field(int x, int y) {
            X = x;
            Y = y;
            Value = X + Y;
        }

        public Field(int x, int y, int value)
        {
            X = x;
            Y = y;
            Value = value;
        }
    }
}
