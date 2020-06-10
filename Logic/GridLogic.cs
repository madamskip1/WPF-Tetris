using System;
using System.Collections.Generic;
using System.Text;

namespace _PAIN__WPF___Tetris.Logic
{
    class GridLogic
    {
        public const int WIDTH = 10;
        public const int HEIGHT = 20;
        public const int NEXT_SIZE = 4;
        

        private Models.Grid Grid;

        public GridLogic()
        {
            Grid = new Models.Grid(WIDTH, HEIGHT, NEXT_SIZE);
        }

        public Models.Cell[,] GetFields()
        {
            return Grid.Fields;
        }

        public Models.Cell[,] GetNextFields()
        {
            return Grid.NextFields;
        }

        public bool CanFitTetromino(Models.Position pos, int[,] pattern)
        {
            int patternHeight = pattern.GetLength(0);
            int patternWidth = pattern.GetLength(1);
            int posX = pos.X;
            int posY = pos.Y;

            for (int x = 0; x < patternWidth; x++)
            {
                for (int y = 0; y < patternHeight; y++)
                {
                    if (pattern[y, x] == 1 && (posX < 0 || posX >= WIDTH || posY >= HEIGHT || (posY >= 0 && !Grid.IsEmptyField(posX, posY))))
                        return false;

                    posY++;
                }

                posY = pos.Y;
                posX++;
            }

            return true;
        }

        public void SetTetromino(Models.Position pos, int[,] pattern, Models.Tetromino.Shapes? shape)
        {
            int patternHeight = pattern.GetLength(0);
            int patternWidth = pattern.GetLength(1);

            for (int x = 0; x < patternWidth; x++)
            {
                for (int y = 0; y < patternHeight; y++)
                {
                    if (pattern[y, x] != 0 && (y + pos.Y) >= 0)
                        Grid.SetField((x + pos.X), (y + pos.Y), shape);
                }
            }
        }

        public void RemoveTetromino(Models.Position pos, int[,] pattern)
        {
            SetTetromino(pos, pattern, null);
        }

        public void SetNextTetromino(int[,] pattern, Models.Tetromino.Shapes? shape)
        {
            int patternHeight = pattern.GetLength(0);
            int patternWidth = pattern.GetLength(1);



            for (int x = 0; x < NEXT_SIZE; x++)
            {

                for (int y = 0; y < NEXT_SIZE; y++)
                {
                    if (x < patternWidth && y < patternHeight && pattern[y, x] == 1)
                        Grid.SetNextField(x, y, shape);
                    else
                        Grid.SetNextField(x, y, null);
                }
            }
        }

        public int ClearRows(int startY, List<int> rows)
        {
            int rowsCleared = 0;
            foreach (int y in rows)
            {
                if (CheckRow(startY + y))
                {
                    rowsCleared++;
                    MoveRows(startY + y);
                    ClearRow(0);
                }
            }

            return rowsCleared;
        }

        private void MoveRows(int y)
        {
            for (int i = y; i > 0; i--)
            {
                for (int x = 0; x < WIDTH; x++)
                    Grid.SetField(x, i, Grid.GetFieldShape(x, i - 1));
            }
        }

        private void ClearRow(int y)
        {
            for (int x = 0; x < WIDTH; x++)
                Grid.SetField(x, y, null);
        }

        public void ClearGrid()
        {
            for (int y = 0; y < HEIGHT; y++)
                ClearRow(y);
        }

        private bool CheckRow(int y)
        {
            if (y < 0)
                return false;

            for (int x = 0; x < WIDTH; x++)
            {
                if (Grid.IsEmptyField(x, y))
                    return false;
            }

            return true;
        }
    }
}
