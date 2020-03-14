﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;

namespace _PAIN__WPF___Tetris.Models
{
    class Grid
    {
        public const int WIDTH = 10;
        public const int HEIGHT = 20;

        public const int NEXT_WIDTH = 4;
        public const int NEXT_HEIGHT = 4;

        public Cell[,] Fields { get; private set; }
        public Cell[,] NextFields { get; private set; }

        public Grid()
        {
            Fields = new Cell[WIDTH, HEIGHT];
            NextFields = new Cell[NEXT_WIDTH, NEXT_HEIGHT];
            PrepareGrids();
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
            // Clear Next Tetromino Grid
            foreach (Cell cell in NextFields)
                cell.Shape = null;

            int[,] pattern = tetromino.Rotation.GetCurPattern();
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



        public Cell GetField(int x, int y)
        {
            return Fields[x, y];
        }



        public bool IsEmptyField(int x, int y)
        {
            if (GetField(x, y).Shape != null)
                return false;

            return true;
        }

        private void SetField(int x, int y, Tetromino.Shapes? shape)
        {
            Fields[x, y].Shape = shape;
        }



        public bool CanFitTetromino(Position pos, int[,] pattern)
        {
            int patternHeight = pattern.GetLength(0);
            int patternWidth = pattern.GetLength(1);
            int posX = pos.X;
            int posY = pos.Y;

            for (int x = 0; x < patternWidth; x++)
            {
                for (int y = 0; y < patternHeight; y++)
                {
                    if (pattern[y, x] == 1 && (posX < 0 || posX >= WIDTH || posY >= HEIGHT || (posY >= 0 && !IsEmptyField(posX, posY))))
                        return false;

                    posY++;
                }

                posX++;
                posY = pos.Y;
            }

            return true;
        }



        public void SetTetromino(Position pos, int[,] pattern, Tetromino.Shapes? shape)
        {
            int patternHeight = pattern.GetLength(0);
            int patternWidth = pattern.GetLength(1);

            for (int x = 0; x < patternWidth; x++)
            {
                for (int y = 0; y < patternHeight; y++)
                {
                    if (pattern[y, x] != 0 && (y + pos.Y) >= 0)           // ZASTANOWIC SIĘ
                        SetField((x + pos.X), (y + pos.Y), shape);
                }
            }
        }




        public void RemoveTetromino(Position pos, int[,] pattern)
        {
            SetTetromino(pos, pattern, null);
        }

        public int ClearRows(int startY, List<int> rows)
        {
            int rowsCleaned = 0;
            foreach(int y in rows)
            {
                if (CheckRow((startY + y)))
                {
                    rowsCleaned++;
                    MoveRows((startY + y));
                    ClearRow(0);
                }
            }

            return rowsCleaned;
        }


        private bool CheckRow(int y)
        {
            if (y < 0)
                return false;

            for (int x = 0; x < WIDTH; x++)
            {
                if (Fields[x, y].Shape == null)
                    return false;
            }
            return true;
        }


        private void ClearRow(int y)
        {
            for (int x = 0; x < WIDTH; x++)
                SetField(x, y, null);
        }


        private void MoveRows(int y)
        {
            for (int i = y; i > 0; i--)
            {
                for (int x = 0; x < WIDTH; x++)
                    SetField(x, i, GetField(x, i - 1).Shape);
            }
        }

        public void ClearGrid()
        {
            for (int x = 0; x < WIDTH; x++)
            {
                for (int y = 0; y < HEIGHT; y++)
                    SetField(x, y, null);
            }

        }


        public void PrepareGrids()
        {
            // MAIN GRID
            for (int x = 0; x < WIDTH; x++)
            {
                for (int y = 0; y < HEIGHT; y++)
                    Fields[x, y] = new Cell();
            }

            // NEXT GRID
            for (int x = 0; x < NEXT_WIDTH; x++)
            {
                for (int y = 0; y< NEXT_HEIGHT; y++)
                    NextFields[x, y] = new Cell();
            }
        }
    }
}
