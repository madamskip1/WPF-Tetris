using _PAIN__WPF___Tetris.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Media;

namespace _PAIN__WPF___Tetris.ViewModels
{
    class ViewModelMainWindow
    {
        public static Color BORDER_COLOR = Color.FromRgb(196, 196, 196);
        public const int CELL_SIZE = 20;


        private ViewModelGame viewModelGame;
        private ViewModelGrid viewModelGrid;
        private MainWindow mainWindow;
        private Models.Results results;

        public enum Keys { LEFT, RIGHT, UP, DOWN, SPACE, ESC }


        public ViewModelMainWindow(MainWindow mainWin)
        {
            viewModelGame = new ViewModelGame(mainWin);
            viewModelGrid = new ViewModelGrid();
            viewModelGame.SetGrid(viewModelGrid);
            mainWindow = mainWin;

            results = new Models.Results();
            viewModelGame.SetResults(results);

            mainWindow.ResultsList.ItemsSource = results.ResultsValues;
            mainWindow.RowsClearedGrid.DataContext = viewModelGame.RowsCleared;


            mainWindow.MainField.ItemsSource = PrepareList(viewModelGrid.GetFields(), ViewModelGrid.WIDTH, ViewModelGrid.HEIGHT);
            mainWindow.NextField.ItemsSource = PrepareList(viewModelGrid.GetNextFields(), ViewModelGrid.NEXT_SIZE, ViewModelGrid.NEXT_SIZE);


            TestResults();
        }


        public void KeyDownEvents(KeyEventArgs e)
        {
            switch (e.Key)
            {
                case Key.Left:
                    viewModelGame.TetrominoMove(ViewModelGame.MoveDirection.LEFT);
                    break;
                case Key.Right:
                    viewModelGame.TetrominoMove(ViewModelGame.MoveDirection.RIGHT);
                    break;
                case Key.Up:
                    viewModelGame.TetrominoRotation();
                    break;
                case Key.Down:
                    viewModelGame.TetrominoSingleRowDown();
                    break;
                case Key.Space:
                    if (viewModelGame.IsRunning())
                        viewModelGame.TetrominoDown();
                    else
                        viewModelGame.Start();
                    break;
            }

        }

     

        private void TestResults()
        {
            results.AddResult(234, DateTime.Now);
            results.AddResult(1230, DateTime.Now);
            results.AddResult(20, DateTime.Now);

        }

        private List<List<Models.Cell>> PrepareList(Models.Cell[,] cells, int width, int height)
        {
            List<List<Models.Cell>> lista = new List<List<Models.Cell>>();

            for (int i = 0; i < height; i++)
            {
                lista.Add(new List<Models.Cell>());

                for (int j = 0; j < width; j++)
                    lista[i].Add(cells[j, i]);
            }

            return lista;
        }
    }
}
