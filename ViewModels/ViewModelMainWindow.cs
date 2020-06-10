using System;
using _PAIN__WPF___Tetris;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows.Media;

namespace _PAIN__WPF___Tetris.ViewModels
{
    class ViewModelMainWindow : System.ComponentModel.INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged(string propertyName) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

        public List<List<Models.Cell>> MainGrid { get { return _MainGrid; } }
        public List<List<Models.Cell>> NextGrid  { get { return _NextGrid; } }
        public ObservableCollection<Result> Results { get { return _Results.ResultsValues; } }
        public Models.RowsCleared RowsCleared { get { return _RowsCleared;  } } 
        public String InfoVisible
        {
            get
            {
                if (_State == Models.Game.GameStates.RUNNING)
                    return "Hidden";
                else
                    return "Visible";
            }
        }

        private List<List<Models.Cell>> _MainGrid;
        private List<List<Models.Cell>> _NextGrid;
        private Models.Results _Results;
        private Models.RowsCleared _RowsCleared;
        private Models.Game.GameStates _State;





        private Logic.GameLogic Game;

        public ViewModelMainWindow()
        {
            Game = new Logic.GameLogic();
            Logic.GridLogic Grid = Game.GetGrid();

            _Results = Game.GetResults();
            _RowsCleared = Game.GetRowsCleared();
            _MainGrid = PrepareList(Grid.GetFields(), Logic.GridLogic.WIDTH, Logic.GridLogic.HEIGHT);
            _NextGrid = PrepareList(Grid.GetNextFields(), Logic.GridLogic.NEXT_SIZE, Logic.GridLogic.NEXT_SIZE);
            _State = Game.GetState();

           TestResults();
        }


        public void KeyDownEvents(KeyEventArgs e)
        {
            switch (e.Key)
            {
                case Key.Left:
                    Game.TetrominoMove(Logic.GameLogic.MoveDirection.LEFT);
                    break;
                case Key.Right:
                    Game.TetrominoMove(Logic.GameLogic.MoveDirection.RIGHT);
                    break;
                case Key.Up:
                    Game.TetrominoRotation();
                    break;
                case Key.Down:
                    Game.TetrominoSingleRowDown();
                    break;
                case Key.Space:
                    if (Game.IsRunning())
                        Game.TetrominoDown();
                    else
                    {
                        Game.Start();
                        _State = Game.GetState();
                        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("InfoVisible"));
                    }
                    break;
            }

        }



        private void TestResults()
        {
            _Results.AddResult(234, DateTime.Now);
            _Results.AddResult(1230, DateTime.Now);
            _Results.AddResult(20, DateTime.Now);

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
