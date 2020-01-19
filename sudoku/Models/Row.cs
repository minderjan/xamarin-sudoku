using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace sudoku.Models
{
    public class Row : List<Field>
    {

        public Field Field1 { get; set; }
        public Field Field2 { get; set; }
        public Field Field3 { get; set; }
        public Field Field4 { get; set; }
        public Field Field5 { get; set; }
        public Field Field6 { get; set; }
        public Field Field7 { get; set; }
        public Field Field8 { get; set; }
        public Field Field9 { get; set; }



        public Row(List<Field> fields) : base(fields)
        {
            Field1 = new Field();
            Field2 = new Field();
            Field3 = new Field();
            Field4 = new Field();
            Field5 = new Field();
            Field6 = new Field();
            Field7 = new Field();
            Field8 = new Field();
            Field9 = new Field();
        }

  
    }
}
