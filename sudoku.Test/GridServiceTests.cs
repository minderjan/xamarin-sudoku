using System.Collections.Generic;
using NUnit.Framework;
using sudoku.Models;
using sudoku.Services;

namespace sudoku.Test
{
    public class Tests
    {

        IGridService _gridServcie;
        int totalFieldCount = 81;
        int totalEmptyFieldsCount = 41;

        [SetUp]
        public void Setup()
        {
            _gridServcie = new GridService();

        }

        [Test]
        public void TestGenerateInitialGrid()
        {
            Assert.That(_gridServcie.GenerateInitialGrid(), Has.Count.EqualTo(totalFieldCount));
        }

        [Test]
        public void TestEmptyFieldsCount()
        {
            List<Field> sudokuGrid = _gridServcie.GenerateInitialGrid();
            Assert.That(_gridServcie.GetEmptyFieldsCount(sudokuGrid), Is.EqualTo(totalEmptyFieldsCount));
        }
    }
}