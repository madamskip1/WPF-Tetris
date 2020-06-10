using System;
using _PAIN__WPF___Tetris;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows.Media;
using _PAIN__WPF___Tetris.MVVM;
using System.Windows;

namespace _PAIN__WPF___Tetris.ViewModels
{
    class TetrisViewModel : System.ComponentModel.INotifyPropertyChanged, MVVM.IViewModel
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged(string propertyName) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

        public List<List<Models.Cell>> MainGrid { get { return _MainGrid; } }
        public List<List<Models.Cell>> NextGrid { get { return _NextGrid; } }
        public ObservableCollection<Result> Results { get { return _Results.ResultsValues; } }
        public Models.RowsCleared RowsCleared { get { return _RowsCleared; } }
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

        public Action Close { get; set; }

        public RelayCommand<object> Left => (left = left ?? new RelayCommand<object>(o => Game.TetrominoMove(Logic.GameLogic.MoveDirection.LEFT)));
        public RelayCommand<object> Right => (right = right ?? new RelayCommand<object>(o => Game.TetrominoMove(Logic.GameLogic.MoveDirection.RIGHT)));
        public RelayCommand<object> Down => (down = down ?? new RelayCommand<object>(o => Game.TetrominoSingleRowDown()));
        public RelayCommand<object> Fix => (fix = fix ?? new RelayCommand<object>(o => fixTetromino()));
        public RelayCommand<object> Rotation => (rotation = rotation ?? new RelayCommand<object>(o => Game.TetrominoRotation()));




        private List<List<Models.Cell>> _MainGrid;
        private List<List<Models.Cell>> _NextGrid;
        private Models.Results _Results;
        private Models.RowsCleared _RowsCleared;
        private Models.Game.GameStates _State;

        private RelayCommand<object> left;
        private RelayCommand<object> right;
        private RelayCommand<object> down;
        private RelayCommand<object> fix;
        private RelayCommand<object> rotation;

        private Logic.GameLogic Game;

        public TetrisViewModel(Logic.GameLogic game)
        {

            // Game = new Logic.GameLogic();
            Game = game;
            Logic.GridLogic Grid = Game.GetGrid();

            _Results = Game.GetResults();
            _RowsCleared = Game.GetRowsCleared();
            _MainGrid = PrepareList(Grid.GetFields(), Logic.GridLogic.WIDTH, Logic.GridLogic.HEIGHT);
            _NextGrid = PrepareList(Grid.GetNextFields(), Logic.GridLogic.NEXT_SIZE, Logic.GridLogic.NEXT_SIZE);
            _State = Game.GetState();

           TestResults();
        }


        private void fixTetromino()
        {
            if (Game.IsRunning())
                Game.TetrominoDown();
            else
            {
                Game.Start();
                _State = Game.GetState();
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("InfoVisible"));
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
