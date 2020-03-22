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
        public Tetromino Current { get; set; }
        public Tetromino Next { get; set; }
        public GameStates GameState { get; set; }

        public enum GameStates { NOTSTARTED, RUNNING, GAMEOVER }
        
        public Game()
        {
            GameState = GameStates.NOTSTARTED;
        }

        public bool IsGameState(GameStates state)
        {
            return GameState == state;
        }
    }
}



