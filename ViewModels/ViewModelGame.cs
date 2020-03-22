﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace _PAIN__WPF___Tetris.ViewModels
{
    class ViewModelGame
    {
        public static TimeSpan SPEED = TimeSpan.FromSeconds(0.4);

        private MainWindow MainWindow;
        private ViewModelGrid Grid;
        private Models.Game Game;
        private Models.Results Results;
        public Models.RowsCleared RowsCleared { get; private set; }

        public enum MoveDirection { LEFT, RIGHT }

        private DateTime previouseDateTime;
        private TimeSpan delta;
        private readonly Random Random;
        

        public ViewModelGame(MainWindow mainWin)
        {
            MainWindow = mainWin;
            Game = new Models.Game();
            Random = new Random();
            RowsCleared = new Models.RowsCleared();
        }


        public void Start()
        {
            Grid.ClearGrid();
            Game.Current = RandomTetromino();
            Game.Next = RandomTetromino();
            DrawTetromino();
            Grid.SetNextTetromino(Game.Next.Rotation.GetCurPattern(), Game.Next.Shape);
            RowsCleared.Reset();
            MainWindow.SetInfoVisibility(false);
            Game.GameState = Models.Game.GameStates.RUNNING;
            previouseDateTime = DateTime.Now;
            MainLoop();
        }

        public void SetResults(Models.Results results)
        {
            Results = results;
        }

        public void SetGrid(ViewModelGrid grid)
        {
            Grid = grid;
        }

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
            int[,] newPattern = Game.Current.Rotation.GetNextPattern();
            RemoveTetrominoFromGrid();

            if (Grid.CanFitTetromino(Game.Current.Position, newPattern))
                Game.Current.Rotation.SetNextPattern();

            DrawTetromino();
        }

        public void TetrominoDown()
        {
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
            MainWindow.SetMainInfoText("Game Over");
            MainWindow.SetInfoVisibility(true);
            Results.AddResult(RowsCleared.GetTotalPoints(), DateTime.Now);
        }

        private bool CheckGameOver()
        {
            return Game.Current.GetTopPosition() < 0;
        }
    }
}
