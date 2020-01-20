using System;
using System.Collections.Generic;
using sudoku.Models;

namespace sudoku
{
    public interface IGridService
    {
        List<Field> GenerateInitialGrid();
        int GetEmptyFieldsCount(IEnumerable<Field> fields);
    }

    public class GridService : IGridService
    {

        List<Field> _predefinedFields { get; set; }

        public GridService()
        {
            InitPredefinedValues();
        }

        public List<Field> GenerateInitialGrid()
        {

            List<Field> _fields = new List<Field>();

            for (int y = 1; y < 10; y++)
            {
                for (int x = 1; x < 10; x++)
                {
                    _fields.Add(GetFieldForCoordinates(x, y));
                }
            }

            return _fields;
        }

        public int GetEmptyFieldsCount(IEnumerable<Field> fields)
        {

            Random random = new Random();
            int emptyFields = 0;

           
                foreach (Field f in fields) {

                    if (f.Value == 0) {
                        emptyFields += 1;
                    }
                    
                }

            return emptyFields + random.Next(14, 30);
        }



        private void InitPredefinedValues() {

            _predefinedFields = new List<Field>();
            _predefinedFields.Clear();
            _predefinedFields.Add(new Field(1, 2, 8));
            _predefinedFields.Add(new Field(1, 3, 5));
            _predefinedFields.Add(new Field(1, 6, 7));
            _predefinedFields.Add(new Field(1, 7, 6));
            _predefinedFields.Add(new Field(1, 8, 1));

            _predefinedFields.Add(new Field(2, 5, 2));
            _predefinedFields.Add(new Field(2, 6, 6));
            _predefinedFields.Add(new Field(2, 8, 8));

            _predefinedFields.Add(new Field(3, 1, 6));
            _predefinedFields.Add(new Field(3, 3, 4));
            _predefinedFields.Add(new Field(3, 5, 5));
            _predefinedFields.Add(new Field(3, 6, 1));
            _predefinedFields.Add(new Field(3, 9, 9));

            _predefinedFields.Add(new Field(4, 1, 3));
            _predefinedFields.Add(new Field(4, 4, 7));
            _predefinedFields.Add(new Field(4, 6, 4));
            _predefinedFields.Add(new Field(4, 9, 2));

            _predefinedFields.Add(new Field(5, 1, 4));
            _predefinedFields.Add(new Field(5, 3, 7));
            _predefinedFields.Add(new Field(5, 7, 1));
            _predefinedFields.Add(new Field(5, 9, 5));

            _predefinedFields.Add(new Field(6, 1, 8));
            _predefinedFields.Add(new Field(6, 3, 6));
            _predefinedFields.Add(new Field(6, 4, 9));
            _predefinedFields.Add(new Field(6, 6, 5));
            _predefinedFields.Add(new Field(6, 9, 7));

            _predefinedFields.Add(new Field(7, 2, 4));
            _predefinedFields.Add(new Field(7, 5, 7));
            _predefinedFields.Add(new Field(7, 6, 9));
            _predefinedFields.Add(new Field(7, 7, 2));
            _predefinedFields.Add(new Field(7, 8, 3));

            _predefinedFields.Add(new Field(8, 2, 6));
            _predefinedFields.Add(new Field(8, 4, 5));
            _predefinedFields.Add(new Field(8, 5, 3));

            _predefinedFields.Add(new Field(9, 1, 7));
            _predefinedFields.Add(new Field(9, 2, 1));
            _predefinedFields.Add(new Field(9, 3, 3));
            _predefinedFields.Add(new Field(9, 7, 9));
            _predefinedFields.Add(new Field(9, 8, 5));
            _predefinedFields.Add(new Field(9, 9, 8));
        }

        private Field GetFieldForCoordinates(int x, int y) {

            // Check for predefines values for this coordinates
            foreach (Field f in _predefinedFields) {
                if (f.X == x && f.Y == y) {
                    return f;
                }
            }

            return new Field(x, y);
        }

        public int GetEmptyFieldsCount()
        {
            throw new NotImplementedException();
        }
    }
}
