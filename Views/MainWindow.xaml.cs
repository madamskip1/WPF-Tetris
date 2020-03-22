using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Media;

namespace _PAIN__WPF___Tetris
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        private Models.Game Game;
        private Models.Results Results;

        public MainWindow()
        {
            WindowStartupLocation = WindowStartupLocation.CenterScreen;

            InitializeComponent();

            Game = new Models.Game(this);
            Results = new Models.Results();
            Game.SetResults(Results);
            ResultsList.ItemsSource = Results.ResultsValues;

            InitKeyDownEvents();

            PrepareField(MainField, Game.Grid.Fields, new Thickness(20, 20, 0, 0), ViewModels.ViewModelGame.WIDTH, ViewModels.ViewModelGame.HEIGHT);
            Thickness NextFieldThickness = new Thickness(ViewModels.ViewModelGame.CELL_SIZE * (Models.Grid.WIDTH + 3), 20, 0, 0);
            PrepareField(NextField, Game.Grid.NextFields, NextFieldThickness, ViewModels.ViewModelNext.WIDTH, ViewModels.ViewModelNext.HEIGHT);


            TestResults();

            RowsClearedGrid.DataContext = Game.RowsCleared;

        }

        public void SetMainInfoText(string text)
        {
            MainInfo.Text = text;
        }

        public void SetInfoVisibility(bool visibility)
        {
            InfoGrid.Visibility = (visibility ? Visibility.Visible : Visibility.Hidden);
        }

        private void PrepareField(Grid grid, Models.Cell[,] cells, Thickness margin, int Width, int Height)
        {
            grid.Margin = margin;

            // Add rows
            for (int i = 0; i < Height; i++)
                grid.RowDefinitions.Add(new RowDefinition
                {
                    Height = new GridLength(ViewModels.ViewModelGame.CELL_SIZE)
                });

            // Add cols
            for (int i = 0; i < Width; i++)
                grid.ColumnDefinitions.Add(new ColumnDefinition
                {
                    Width = new GridLength(ViewModels.ViewModelGame.CELL_SIZE)
                });
            

            for (int y = 0; y < Height; y++)
            {
                for (int x = 0; x < Width; x++)
                {
                    Border border = new Border();
                    border.BorderThickness = new Thickness(1);
                    SolidColorBrush borderBr = new SolidColorBrush();
                    borderBr.Color = ViewModels.ViewModelGame.BorderColor;
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


        private void InitKeyDownEvents()
        {
            this.KeyDown += (sender, e) =>
            {
                switch (e.Key)
                {
                    case Key.Left:
                        Game.KeyDown(Models.Game.Keys.LEFT);
                        break;
                    case Key.Right:
                        Game.KeyDown(Models.Game.Keys.RIGHT);
                        break;
                    case Key.Up:
                        Game.KeyDown(Models.Game.Keys.UP);
                        break;
                    case Key.Down:
                        Game.KeyDown(Models.Game.Keys.DOWN);
                        break;
                    case Key.Space:
                        Game.KeyDown(Models.Game.Keys.SPACE);
                        break;
                    case Key.Escape:
                        Game.KeyDown(Models.Game.Keys.ESC);
                        break;
                }
            };
        }


        private void TestResults()
        {
            Results.AddResult(234, DateTime.Now);
            Results.AddResult(1230, DateTime.Now);
            Results.AddResult(20, DateTime.Now);

        }
    }

}
