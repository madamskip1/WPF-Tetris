﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace _PAIN__WPF___Tetris.Models
{
    class RowsCleared : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public int Rows1 { get; private set; }
        public int Rows2 { get; private set; }
        public int Rows3 { get; private set; }
        public int Rows4 { get; private set; }

        public RowsCleared()
        {
            Reset();
        }

        public void Cleared(int rowsNumber)
        {
            switch(rowsNumber)
            {
                case 1:
                    Rows1++;
                    break;
                case 2:
                    Rows2++;
                    break;
                case 3:
                    Rows3++;
                    break;
                case 4:
                    Rows4++;
                    break;
            }

            OnPropertyChanged(rowsNumber);
        }

        public void Reset()
        {
            Rows1 = 0;
            Rows2 = 0;
            Rows3 = 0;
            Rows4 = 0;
        }

        private void OnPropertyChanged(int rowsNumber)
        {
            string property = "Rows" + rowsNumber;
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(property));
        }
    }
}
