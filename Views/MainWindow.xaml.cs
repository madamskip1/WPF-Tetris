using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace _PAIN__WPF___Tetris
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        private Models.Game Game;

        public MainWindow()
        {

            InitializeComponent();
            Game = new Models.Game();
            InitEvents();

            SetupMainField(MainField);
            SetupNextField(NextField);
            TestResults();


        }

        private void SetupMainField(Grid grid)
        {
            grid.Margin = new Thickness(20, 20, 0, 0);

            // Add rows
            for (int i = 0; i < Models.Grid.HEIGHT; i++)
                grid.RowDefinitions.Add(new RowDefinition
                {
                    Height = new GridLength(ViewModels.ViewModelGame.CELL_SIZE)
                });

            // Add Cols
            for (int i = 0; i < Models.Grid.WIDTH; i++)
                grid.ColumnDefinitions.Add(new ColumnDefinition
                {
                    Width = new GridLength(ViewModels.ViewModelGame.CELL_SIZE)
                });


            for (int y = 0; y < Models.Grid.HEIGHT; y++)
            {
                for (int x = 0; x < Models.Grid.WIDTH; x++)
                {
                    Border border = new Border();
                    border.BorderThickness = new Thickness(1);
                    border.BorderBrush = Brushes.Black;

                    TextBlock ctrl = new TextBlock();
                    ctrl.Background = Brushes.LightBlue;
                    border.Child = ctrl;

                    Grid.SetRow(border, y);
                    Grid.SetColumn(border, x);
                    grid.Children.Add(border);
                }
            }
        }

        private void SetupNextField(Grid grid)
        {
            grid.Margin = new Thickness((ViewModels.ViewModelGame.CELL_SIZE * (Models.Grid.WIDTH + 3)) , 20, 0, 0);

            // Add rows
            for (int i = 0; i < ViewModels.ViewModelNext.SIZE; i++)
                grid.RowDefinitions.Add(new RowDefinition
                {
                    Height = new GridLength(ViewModels.ViewModelGame.CELL_SIZE)
                });

            // Add Cols
            for (int i = 0; i < ViewModels.ViewModelNext.SIZE; i++)
                grid.ColumnDefinitions.Add(new ColumnDefinition
                {
                    Width = new GridLength(ViewModels.ViewModelGame.CELL_SIZE)
                });


            for (int y = 0; y < Models.Grid.HEIGHT; y++)
            {
                for (int x = 0; x < Models.Grid.WIDTH; x++)
                {
                    Border border = new Border();
                    border.BorderThickness = new Thickness(1);
                    border.BorderBrush = Brushes.Black;

                    TextBlock ctrl = new TextBlock();
                    ctrl.Background = Brushes.LightBlue;
                    border.Child = ctrl;

                    Grid.SetRow(border, y);
                    Grid.SetColumn(border, x);
                    grid.Children.Add(border);
                }
            }
        }


        private void InitEvents()
        {
            this.KeyDown += (sender, e) =>
            {
                switch(e.Key)
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


        public void TestResults()
        {
            Models.Results result = Models.Results.Instance;
            
            result.AddResult(234, DateTime.Now);
            result.AddResult(1230, DateTime.Now);
            result.AddResult(20, DateTime.Now);


            ResultsList.ItemsSource = result.ResultsValues;
            
        }

        private void Start_Button_Click(object sender, RoutedEventArgs e)
        {
            Game.Start();
        }
    }



}
