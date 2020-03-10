using System;
using System.Collections.Generic;
using System.Text;

namespace _PAIN__WPF___Tetris.Models
{
    class Grid
    {
        public const short WIDTH = 10;
        public const short HEIGHT = 20;

        private Tetromino.Shapes?[,] Fields;


        public Grid()
        {
            ClearGrid();
        }

        public Tetromino.Shapes? GetField(short x, short y)
        {
            return Fields[x, y];
        }

        public bool IsEmptyField(short x, short y)
        {
            if (x < 0 || x >= WIDTH || y < 0 || y >= HEIGHT || GetField(x, y) != null)
                return false;

            return true;
        }

        private void SetField(short x, short y, Tetromino.Shapes? shape)
        {
            Fields[x, y] = shape;
        }

        public bool CanFitTetromino(Position pos, short[,] pattern)
        {
            int patternHeight = pattern.GetLength(0);
            int patternWidth = pattern.GetLength(1);

            for (int x = pos.X; x < (pos.X + patternWidth); x++)
            {
                for (int y = pos.Y; y < (pos.Y + patternHeight); y++)
                {
                    if (pattern[y, x] == 1 && Fields[x, y] != null)
                        return false;
                }

            }
                    return true;
        }

        public void SetTetromino(Position pos, short[,] pattern, Tetromino.Shapes shape)
        {
            int patternHeight = pattern.GetLength(0);
            int patternWidth = pattern.GetLength(1);

            for (int x = pos.X; x < (pos.X + patternWidth); x++)
            {
                for (int y = pos.Y; y < (pos.Y + patternHeight); y++)
                {
                    if (pattern[y, x] != 0)
                        Fields[x, y] = shape;
                }
            }
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
                if (Fields[x, y] == null)
                    return false;
            }
            return true;
        }

        private void ClearRow(short y)
        {
            for (short x = 0; x < WIDTH; x++)
                Fields[x, y] = null;
        }

        private void MoveRows(short y)
        {
            for (short i = y; i > 0; i--)
            {
                for (short x = 0; x < WIDTH; x++)
                    Fields[x, i] = Fields[x, i - 1];
            }
        }

        public void ClearGrid()
        {
            Fields = new Tetromino.Shapes?[10, 20];

            for (short x = 0; x < WIDTH; x++)
            {
                for (short y = 0; y < HEIGHT; y++)
                {
                    Fields[x, y] = null;
                }
            }
        }
    }
}
