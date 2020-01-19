using System;
using System.Collections.Generic;
using sudoku.Models;

namespace sudoku
{
    public interface IGridService
    {
        List<Row> GenerateInitialGrid();
    }

    public class GridService : IGridService
    {

        public GridService()
        {

        }

        public List<Row> GenerateInitialGrid()
        {

            List<Row> _fields = new List<Row>();

            for (int i = 1; i < 10; i++)
            {
                _fields.Add(generateRow(i));
            }

            return _fields;
        }

        private Row generateRow(int y)
        {
            System.Diagnostics.Debug.WriteLine("Generating Sudoku Rows");

            List<Field> fields = new List<Field>();

            for (int i = 1; i < 10; i++)
            {
                fields.Add(new Field(i, y));
            }


            return new Row(fields);
        }


    }
}
