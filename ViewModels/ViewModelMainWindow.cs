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

            PrepareField(mainWindow.MainField, viewModelGrid.GetFields(), new Thickness(20, 20, 0, 0), ViewModelGrid.WIDTH, ViewModelGrid.HEIGHT);
            Thickness NextFieldThickness = new Thickness(CELL_SIZE * (ViewModelGrid.WIDTH + 3), 20, 0, 0);
            PrepareField(mainWindow.NextField, viewModelGrid.GetNextFields(), NextFieldThickness, ViewModelGrid.NEXT_SIZE, ViewModelGrid.NEXT_SIZE);

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

        private void PrepareField(Grid grid, Models.Cell[,] cells, Thickness margin, int width, int height)
        {
            grid.Margin = margin;

            // Add rows
            for (int i = 0; i < height; i++)
                grid.RowDefinitions.Add(new RowDefinition
                {
                    Height = new GridLength(CELL_SIZE)
                });

            // Add cols
            for (int i = 0; i < width; i++)
                grid.ColumnDefinitions.Add(new ColumnDefinition
                {
                    Width = new GridLength(CELL_SIZE)
                });

            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    Border border = new Border();
                    border.BorderThickness = new Thickness(1);
                    SolidColorBrush borderBr = new SolidColorBrush();
                    borderBr.Color = BORDER_COLOR;
                    border.BorderBrush = borderBr;

                    SolidColorBrush brush = new SolidColorBrush();
                    System.Drawing.Color drawingColor = Models.Tetromino.getShapeColor(Models.Tetromino.Shapes.O);
                    brush.Color = Color.FromArgb(drawingColor.A, drawingColor.R, drawingColor.G, drawingColor.B);

                    TextBlock ctrl = new TextBlock();
                    ctrl.Background = brush;
                    ctrl.DataContext = cells[x, y];
                    BindingOperations.SetBinding(brush, SolidColorBrush.ColorProperty, new Binding("Color"));
                    border.Child = ctrl;

                    Grid.SetRow(border, y);
                    Grid.SetColumn(border, x);
                    grid.Children.Add(border);
                }
            }
        }

        private void TestResults()
        {
            results.AddResult(234, DateTime.Now);
            results.AddResult(1230, DateTime.Now);
            results.AddResult(20, DateTime.Now);

        }
    }
}
