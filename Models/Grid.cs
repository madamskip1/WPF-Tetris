using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;

namespace _PAIN__WPF___Tetris.Models
{
    class Grid
    {
        public Cell[,] Fields { get; private set; }
        public Cell[,] NextFields { get; private set; }

        private int width;
        private int height;
        private int nextSize;

        public Grid(int _width, int _height, int _nextSize)
        {
            width = _width;
            height = _height;
            nextSize = _nextSize;

            Fields = new Cell[width, height];
            NextFields = new Cell[nextSize, nextSize];
            PrepareGrids();
        }

        public Cell GetField(int x, int y)
        {
            return Fields[x, y];
        }

        public Tetromino.Shapes? GetFieldShape(int x, int y)
        {
            return Fields[x, y].Shape;
        }

        public void SetField(int x, int y, Tetromino.Shapes? shape)
        {
            Fields[x, y].Shape = shape;
        }

        public void SetNextField(int x, int y, Tetromino.Shapes? shape)
        {
            NextFields[x, y].Shape = shape;
        }

        public bool IsEmptyField(int x, int y)
        {
            if (GetFieldShape(x, y) != null)
                return false;

            return true;
        }



        private void PrepareGrids()
        {
            // MAIN GRID
            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                    Fields[x, y] = new Cell();
            }

            // NEXT GRID
            for (int x = 0; x < nextSize; x++)
            {
                for (int y = 0; y < nextSize; y++)
                    NextFields[x, y] = new Cell();
            }
        }

    }
}
