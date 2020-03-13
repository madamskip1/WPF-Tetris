using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace _PAIN__WPF___Tetris.Models
{
    class Game
    {


        private Tetromino Cur { get; set; }
        private Tetromino Next { get; set; }
        public Grid Grid { get; set; }
        public RowsCleared RowsCleared;

        private readonly Random Random;

        private DateTime previouseDateTime;
        private TimeSpan Delta;
        private TimeSpan Speed = TimeSpan.FromSeconds(0.5);

        public enum MoveDirection { LEFT, RIGHT }
        public enum Keys { LEFT, RIGHT, UP, DOWN, SPACE, ESC }

        public Game()
        {
            
            Grid = new Grid();
            Random = new Random();
            Cur = new Tetromino(Tetromino.Shapes.O);
            Cur.Position.X = 8;
            Cur.Position.Y = 14;
            RowsCleared = new RowsCleared();
        }


        public void KeyDown(Keys key/* key */)
        {
            if (key == Keys.UP)
                TetrominoRotation();
            else if (key == Keys.LEFT)
                TetrominoMove(MoveDirection.LEFT);
            else if (key == Keys.RIGHT)
                TetrominoMove(MoveDirection.RIGHT);
            else if (key == Keys.DOWN)
                TetrominoSingleRowDown();
            else if (key == Keys.SPACE)
                TetrominoDown();
        }

        private Tetromino RandomTetromino()
        {
            Array shapes = Enum.GetValues(typeof(Tetromino.Shapes));
            Tetromino.Shapes shape = (Tetromino.Shapes)shapes.GetValue(Random.Next(shapes.Length));
            return new Tetromino(shape);
        }

        public void Start()
        {
            Grid.ClearGrid();
            Cur = RandomTetromino();
            Next = RandomTetromino();
            Grid.SetNextTetromino(Next);

            previouseDateTime = DateTime.Now;
            MainLoop();
        }

        private async void MainLoop()
        {
            while (IsRunning())
            {
                TimeSpan delta = DateTime.Now - previouseDateTime;
                Delta += delta;

                if (Delta >= Speed)
                {
                    TetrominoSingleRowDown();
                }

                previouseDateTime += delta;
                await Task.Delay(8);
            }
        }

        private bool IsRunning()
        {
            return true;
        }

        private void TetrominoMove(MoveDirection dir)
        {
            Position newPosition = new Position(Cur.Position.X, Cur.Position.Y);

            if (dir == MoveDirection.LEFT)
                newPosition.X -= 1;
            else
                newPosition.X += 1;

            RemoveTetrominoFromGrid();

            if (Grid.CanFitTetromino(newPosition, Cur.Rotation.GetCurPattern()))
                Cur.Position = newPosition;

            PlaceTetromino();
        }

        // Rotate Cur Tetromino
        private void TetrominoRotation()
        {
            short[,] newPattern = Cur.Rotation.GetNextPattern();
            RemoveTetrominoFromGrid();
            if (Grid.CanFitTetromino(Cur.Position, newPattern))
            {
                Cur.Rotation.SetNextPattern();
                
            }
            PlaceTetromino();
        }

        // Move Cur Tetromino one rown down
        private void TetrominoSingleRowDown()
        {
            RemoveTetrominoFromGrid();

            Position newPosition = new Position(Cur.Position.X, Cur.Position.Y + 1);
            if (Grid.CanFitTetromino(newPosition, Cur.Rotation.GetCurPattern()))
            {

                Delta = TimeSpan.Zero;
                // DRAW

                Cur.Position = newPosition;
                PlaceTetromino();
            }
            else
            {
                PlaceTetromino();
                CheckRows();
                NextTetromino();
                
            }
        }

        // place Cur Tetromino as low as possible
        private void TetrominoDown()
        {
            Position newPosition = new Position(Cur.Position.X, Cur.Position.Y + 1);

            while (Grid.CanFitTetromino(newPosition, Cur.Rotation.GetCurPattern()))
                newPosition.Y += 1;

            newPosition.Y -= 1;

            Cur.Position = newPosition;
            PlaceTetromino();
        }

        private void RemoveTetrominoFromGrid()
        {
            Grid.RemoveTetromino(Cur.Position, Cur.Rotation.GetCurPattern());
        }

        private void RotateTetromino()
        {
            Cur.Rotation.SetNextPattern();
        }


        private void PlaceTetromino()
        {
            Grid.SetTetromino(Cur.Position, Cur.Rotation.GetCurPattern(), Cur.Shape);
            Grid.ClearRows((short)Cur.Position.Y, Cur.Rotation.GetRows());

            // Następny tetromino TODO
            //  DRAW
            Delta = TimeSpan.Zero;

        }

        private void NextTetromino()
        {
            Cur = Next;
            Next = RandomTetromino();
            Grid.SetNextTetromino(Next);
        }

        private void CheckRows()
        {
            // Start Y = pos.Y
            List<short> rows = new List<short>();
            short[,] pattern = Cur.Rotation.GetCurPattern();
            int patternHeight = pattern.GetLength(0);
            int patternWidth = pattern.GetLength(1);

            for (int y = 0; y < patternHeight; y++)
            {
                for (int x = 0; x < patternWidth; x++)
                {
                    if (pattern[y, x] == 1)
                    {
                        rows.Add((short)y);
                        break;
                    }
                }
            }

            int rowsCleared = Grid.ClearRows((short)Cur.Position.Y, rows);
            RowsCleared.Cleared(rowsCleared);
        }

    }
}
