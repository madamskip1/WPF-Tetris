using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace _PAIN__WPF___Tetris.Logic
{
    class GameLogic
    {
        public static TimeSpan SPEED = TimeSpan.FromSeconds(0.4);


        private GridLogic Grid;
        private Models.Game Game;
        private Models.Results Results;
        public Models.RowsCleared RowsCleared { get; private set; }

        public enum MoveDirection { LEFT, RIGHT }

        private DateTime previouseDateTime;
        private TimeSpan delta;
        private readonly Random Random;
        

        public GameLogic()
        {
            Game = new Models.Game();
            Random = new Random();
            RowsCleared = new Models.RowsCleared();
            Grid = new GridLogic();
            Results = new Models.Results();
        }


        public void Start()
        {
            Grid.ClearGrid();
            Game.Current = RandomTetromino();
            Game.Next = RandomTetromino();
            DrawTetromino();
            Grid.SetNextTetromino(Game.Next.Rotation.GetCurPattern(), Game.Next.Shape);
            RowsCleared.Reset();
            Game.GameState = Models.Game.GameStates.RUNNING;
            previouseDateTime = DateTime.Now;
            MainLoop();
        }


        public Models.Results GetResults()
        {
            return Results;
        }

        public Logic.GridLogic GetGrid()
<<<<<<< HEAD:Logic/GameLogic.cs
        {
            return Grid;
        }

        public Models.RowsCleared GetRowsCleared()
        {
            return RowsCleared;
        }

        public Models.Game.GameStates GetState()
        {
            return Game.GameState;
        }

=======
        {
            return Grid;
        }

        public Models.RowsCleared GetRowsCleared()
        {
            return RowsCleared;
        }

        public Models.Game.GameStates GetState()
        {
            return Game.GameState;
        }

>>>>>>> master:ViewModels/ViewModelGame.cs

        public bool IsRunning()
        {
            return Game.IsGameState(Models.Game.GameStates.RUNNING);
        }

        private async void MainLoop()
        {
            while (Game.IsGameState(Models.Game.GameStates.RUNNING))
            {
                TimeSpan _delta = DateTime.Now - previouseDateTime;
                delta += _delta;

                if (delta >= SPEED)
                    TetrominoSingleRowDown();

                previouseDateTime += _delta;
                await Task.Delay(8);
            }
        }

        public void TetrominoMove(MoveDirection dir)
        {
            if (!IsRunning())
                return;

            Models.Position newPosition = new Models.Position(Game.Current.Position.X, Game.Current.Position.Y);

            if (dir == MoveDirection.LEFT)
                --newPosition.X;
            else
                ++newPosition.X;

            RemoveTetrominoFromGrid();

            if (Grid.CanFitTetromino(newPosition, Game.Current.Rotation.GetCurPattern()))
                Game.Current.Position = newPosition;

            DrawTetromino();
        }

        public void TetrominoRotation()
        {
            if (!IsRunning())
                return;

            int[,] newPattern = Game.Current.Rotation.GetNextPattern();
            RemoveTetrominoFromGrid();

            if (Grid.CanFitTetromino(Game.Current.Position, newPattern))
                Game.Current.Rotation.SetNextPattern();

            DrawTetromino();
        }

        public void TetrominoDown()
        { 
            if (!IsRunning())
                return;

            RemoveTetrominoFromGrid();
            Models.Position newPosition = new Models.Position(Game.Current.Position.X, Game.Current.Position.Y + 1);

            while (Grid.CanFitTetromino(newPosition, Game.Current.Rotation.GetCurPattern()))
                ++newPosition.Y;

            --newPosition.Y;

            Game.Current.Position = newPosition;
            PlaceTetromino();
            TetrominoDowned();
        }

        public void TetrominoSingleRowDown()
        {
            if (!IsRunning())
                return;


            RemoveTetrominoFromGrid();

            Models.Position newPosition = new Models.Position(Game.Current.Position.X, Game.Current.Position.Y + 1);
            if (Grid.CanFitTetromino(newPosition, Game.Current.Rotation.GetCurPattern()))
            {
                delta = TimeSpan.Zero;
                Game.Current.Position = newPosition;
                DrawTetromino();
            }
            else
            {
                PlaceTetromino();
                TetrominoDown();
            }
        }

        private void TetrominoDowned()
        {
            if (CheckGameOver())
                GameOver();
            else
                NextTetromino();
        }

        private void NextTetromino()
        {
            Game.Current = Game.Next;
            Game.Next = RandomTetromino();
            Grid.SetNextTetromino(Game.Next.Rotation.GetCurPattern(), Game.Next.Shape);
        }

        private Models.Tetromino RandomTetromino()
        {
            Array shapes = Enum.GetValues(typeof(Models.Tetromino.Shapes));
            Models.Tetromino.Shapes shape = (Models.Tetromino.Shapes)shapes.GetValue(Random.Next(shapes.Length));
            return new Models.Tetromino(shape);
        }

        private void PlaceTetromino()
        {
            DrawTetromino();
            Grid.SetTetromino(Game.Current.Position, Game.Current.Rotation.GetCurPattern(), Game.Current.Shape);

            List<int> rows = Game.Current.Rotation.RowsToCheck();
            int numberOfRowsCleared = Grid.ClearRows(Game.Current.Position.Y, rows);
            RowsCleared.Cleared(numberOfRowsCleared);
        }

        private void RemoveTetrominoFromGrid()
        {
            Grid.RemoveTetromino(Game.Current.Position, Game.Current.Rotation.GetCurPattern());
        }

        private void DrawTetromino()
        {
            Grid.SetTetromino(Game.Current.Position, Game.Current.Rotation.GetCurPattern(), Game.Current.Shape);
        }


        private void GameOver()
        {
            Game.GameState = Models.Game.GameStates.GAMEOVER;
            Results.AddResult(RowsCleared.GetTotalPoints(), DateTime.Now);
        }

        private bool CheckGameOver()
        {
            return Game.Current.GetTopPosition() < 0;
        }
    }
}
