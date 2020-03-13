using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;

namespace _PAIN__WPF___Tetris.Models
{
    class Grid
    {
        public const short WIDTH = 10;
        public const short HEIGHT = 20;

        public const short NEXT_WIDTH = 4;
        public const short NEXT_HEIGHT = 4;

        public Cell[,] Fields { get; private set; }
        public Cell[,] NextFields { get; private set; }

        public Grid()
        {
            Fields = new Cell[WIDTH, HEIGHT];
            NextFields = new Cell[NEXT_WIDTH, NEXT_HEIGHT];
            PrepareGrids();
            ClearGrid();
        }

        public void Test()
        {
            Fields[2, 2].Shape = Tetromino.Shapes.T;
            Fields[5, 7].Shape = Tetromino.Shapes.T;
        }

        public void Test2()
        {
            Fields[9, 9].Shape = Tetromino.Shapes.J;
            Fields[0,0].Shape = Tetromino.Shapes.J;
        }

        public void SetNextTetromino(Tetromino tetromino)
        {
            foreach (Cell cell in NextFields)
                cell.Shape = null;

            short[,] pattern = tetromino.Rotation.GetCurPattern();
            int patternHeight = pattern.GetLength(0);
            int patternWidth = pattern.GetLength(1);

            for (int x = 0; x < patternWidth; x++)
            {
                for (int y = 0; y < patternHeight; y++)
                {
                    if (pattern[y, x] == 1)
                        NextFields[x, y].Shape = tetromino.Shape;
                }
            }
        }

        public Cell GetField(short x, short y)
        {
            return Fields[x, y];
        }

        public bool IsEmptyField(short x, short y)
        {
            if (x < 0 || x >= WIDTH || y < 0 || y >= HEIGHT || GetField(x, y).Shape != null)
                return false;

            return true;
        }

        private void SetField(short x, short y, Tetromino.Shapes? shape)
        {
            Fields[x, y].Shape = shape;
        }

        public bool CanFitTetromino(Position pos, short[,] pattern)
        {
            int patternHeight = pattern.GetLength(0);
            int patternWidth = pattern.GetLength(1);
            int posX = pos.X;
            int posY = pos.Y;
            for (int x = 0; x < patternWidth; x++)
            {
                for (int y = 0; y < patternHeight; y++)
                {
                    if (pattern[y, x] == 1 && !IsEmptyField((short)posX, (short)posY))
                        return false;

                    posY++;
                }
                posX++;
                posY = pos.Y;

            }
                    return true;
        }

        public void SetTetromino(Position pos, short[,] pattern, Tetromino.Shapes? shape)
        {
            int patternHeight = pattern.GetLength(0);
            int patternWidth = pattern.GetLength(1);

            for (int x = 0; x < patternWidth; x++)
            {
                for (int y = 0; y < patternHeight; y++)
                {
                    if (pattern[y, x] != 0)
                        Fields[(x + pos.X), (y + pos.Y)].Shape = shape;
                }
            }
        }

        public void RemoveTetromino(Position pos, short[,] pattern)
        {
            SetTetromino(pos, pattern, null);
        }

        public int ClearRows(short startY, List<short> rows)
        {
            int rowsCleaned = 0;
            foreach(short y in rows)
            {
                if (CheckRow((short)(startY + y)))
                {
                    rowsCleaned++;
                    MoveRows((short)(startY + y));
                    ClearRow(0);
                }
            }

            return rowsCleaned;
        }

        private bool CheckRow(short y)
        {
            for (short x = 0; x < WIDTH; x++)
            {
                if (Fields[x, y].Shape == null)
                    return false;
            }
            return true;
        }

        private void ClearRow(short y)
        {
            for (short x = 0; x < WIDTH; x++)
                Fields[x, y].Shape = null;
        }

        private void MoveRows(short y)
        {
            for (short i = y; i > 0; i--)
            {
                for (short x = 0; x < WIDTH; x++)
                    Fields[x, i].Shape = Fields[x, i - 1].Shape;
            }
        }

        public void ClearGrid()
        {
            for (short x = 0; x < WIDTH; x++)
            {
                for (short y = 0; y < HEIGHT; y++)
                {
                    Fields[x, y].Shape = null;
                }
            }

        }
        public void PrepareGrids()
        {
            // MAIN GRID
            for (short x = 0; x < WIDTH; x++)
            {
                for (short y = 0; y < HEIGHT; y++)
                {
                    Fields[x, y] = new Cell();
                    Fields[x, y].Shape = null;
                }
            }

            // NEXT GRID
            for (short x = 0; x < NEXT_WIDTH; x++)
            {
                for (short y = 0; y< NEXT_HEIGHT; y++)
                {
                    NextFields[x, y] = new Cell();
                    NextFields[x, y].Shape = null;
                }
            }
        }
    }
}
