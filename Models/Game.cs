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
        private Results Results;
        private readonly Random Random;

        private DateTime previouseDateTime;
        private TimeSpan Delta;
        private TimeSpan Speed = TimeSpan.FromSeconds(0.5);

        public enum MoveDirection { LEFT, RIGHT }
        public enum Keys { LEFT, RIGHT, UP, DOWN, SPACE, ESC }

        public enum GameStates { NOTSTARTED, RUNNING, GAMEOVER }
        private GameStates GameState;

        public Game()
        {
            Grid = new Grid();
            GameState = GameStates.NOTSTARTED;
            Random = new Random();
            RowsCleared = new RowsCleared();
        }

        public void SetResults(Results results)
        {
            Results = results;
        }
        public void KeyDown(Keys key/* key */)
        {
            if (key == Keys.UP)
            {
                if (IsGameState(GameStates.RUNNING))
                    TetrominoRotation();
            }
            else if (key == Keys.LEFT)
            {
                if (IsGameState(GameStates.RUNNING))
                    TetrominoMove(MoveDirection.LEFT);
            }
            else if (key == Keys.RIGHT)
            {
                if (IsGameState(GameStates.RUNNING))
                    TetrominoMove(MoveDirection.RIGHT);
            }
            else if (key == Keys.DOWN)
            {
                if (IsGameState(GameStates.RUNNING))
                    TetrominoSingleRowDown();
            }
            else if (key == Keys.SPACE)
            {
                if (GameState == GameStates.RUNNING)
                    TetrominoDown();
                else if (GameState == GameStates.NOTSTARTED || GameState == GameStates.GAMEOVER)
                    Start();
            }
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
            //PlaceTetromino();
            DrawTetromino();
            Grid.SetNextTetromino(Next);
            RowsCleared.Reset();

            GameState = GameStates.RUNNING;
            previouseDateTime = DateTime.Now;
            MainLoop();
        }



        private async void MainLoop()
        {
            while (GameState == GameStates.RUNNING)
            {
                TimeSpan delta = DateTime.Now - previouseDateTime;
                Delta += delta;

                if (Delta >= Speed)
                    TetrominoSingleRowDown();

                previouseDateTime += delta;
                await Task.Delay(8);
            }
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

            DrawTetromino();
        }




        // Rotate Cur Tetromino
        private void TetrominoRotation()
        {
            int[,] newPattern = Cur.Rotation.GetNextPattern();
            RemoveTetrominoFromGrid();
            if (Grid.CanFitTetromino(Cur.Position, newPattern))
                Cur.Rotation.SetNextPattern();

            DrawTetromino();
        }




        // Move Cur Tetromino one rown down
        private void TetrominoSingleRowDown()
        {
            RemoveTetrominoFromGrid();

            Position newPosition = new Position(Cur.Position.X, Cur.Position.Y + 1);
            if (Grid.CanFitTetromino(newPosition, Cur.Rotation.GetCurPattern()))
            {

                Delta = TimeSpan.Zero;
                Cur.Position = newPosition;
                DrawTetromino();
            }
            else
            {
                PlaceTetromino();
                if (CheckGameOver())
                    GameOver();
                else
                    NextTetromino();
            }
        }



        // place Cur Tetromino as low as possible
        private void TetrominoDown()
        {
            RemoveTetrominoFromGrid();
            Position newPosition = new Position(Cur.Position.X, Cur.Position.Y + 1);

            while (Grid.CanFitTetromino(newPosition, Cur.Rotation.GetCurPattern()))
                newPosition.Y += 1;

            newPosition.Y -= 1;

            Cur.Position = newPosition;
            PlaceTetromino();
            if (CheckGameOver())
                GameOver();
            else
                NextTetromino();
        }


        private bool CheckGameOver()
        {
            return Cur.GetTopPosition() < 0;
        }


        private void RemoveTetrominoFromGrid()
        {
            Grid.RemoveTetromino(Cur.Position, Cur.Rotation.GetCurPattern());
        }


        private void PlaceTetromino()
        {
            DrawTetromino();
            List<int> rows = Cur.Rotation.RowsToCheck();

            Grid.SetTetromino(Cur.Position, Cur.Rotation.GetCurPattern(), Cur.Shape);
            int numberOfRowsCleared = Grid.ClearRows(Cur.Position.Y, rows);
            RowsCleared.Cleared(numberOfRowsCleared);
        }

        private void NextTetromino()
        {
            Cur = Next;
            Next = RandomTetromino();
            Grid.SetNextTetromino(Next);
        }

        private void GameOver()
        {
            GameState = GameStates.GAMEOVER;
            MessageBox.Show("Game Over, Leszczu");

            Results.AddResult(RowsCleared.TotalPoints, DateTime.Now);
        }

        private void DrawTetromino()
        {
            Grid.SetTetromino(Cur.Position, Cur.Rotation.GetCurPattern(), Cur.Shape);
        }


        private bool IsGameState(GameStates state)
        {
            return GameState == state;
        }
    }
}
