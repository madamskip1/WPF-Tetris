using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace _PAIN__WPF___Tetris.Models
{
    class Game
    {
        private Tetromino Cur { get; set; }
        private Tetromino Next { get; set; }
        private Grid Grid { get; set; }

        private readonly Random Random;

        private DateTime previouseDateTime;
        private TimeSpan Delta;
        private TimeSpan Speed = TimeSpan.FromSeconds(1);

        public enum MoveDirection { LEFT, RIGHT }
        public enum Keys { LEFT, RIGHT, UP, DOWN, SPACE, ESC }

        public Game()
        {
            
            Grid = new Grid();
            Random = new Random();
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

            previouseDateTime = DateTime.Now;
            MessageBox.Show("DUPA");
            //MainLoop();
        }

        private async void MainLoop()
        {
            while (IsRunning())
            {
                TimeSpan delta = DateTime.Now - previouseDateTime;
                Delta += delta;

                if (Delta >= Speed)
                    TetrominoSingleRowDown();

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
            if (Grid.CanFitTetromino(newPosition, Cur.Rotation.GetCurPattern()))
            {
                Cur.Position = newPosition;
                // DRAW
            }
        }

        // Rotate Cur Tetromino
        private void TetrominoRotation()
        {
            short[,] newPattern = Cur.Rotation.GetNextPattern();
            if (Grid.CanFitTetromino(Cur.Position, newPattern))
            {
                Cur.Rotation.SetNextPattern();
                // DRAW
            }
        }

        // Move Cur Tetromino one rown down
        private void TetrominoSingleRowDown()
        {
            Position newPosition = new Position(Cur.Position.X, Cur.Position.Y + 1);
            if (Grid.CanFitTetromino(newPosition, Cur.Rotation.GetCurPattern()))
            {
                Cur.Position = newPosition;
                Delta = TimeSpan.Zero;
                // DRAW
            }
            else
                PlaceTetromino();
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

        private void PlaceTetromino()
        {
            Grid.SetTetromino(Cur.Position, Cur.Rotation.GetCurPattern(), Cur.Shape);
            Grid.ClearRows((short)Cur.Position.Y, Cur.Rotation.GetRows());

            // Następny tetromino TODO
            //  DRAW
            Delta = TimeSpan.Zero;

        }
    }
}
