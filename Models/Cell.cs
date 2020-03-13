using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace _PAIN__WPF___Tetris.Models
{
    class Cell : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private Tetromino.Shapes? shap;
        public Tetromino.Shapes? Shape
        {
            get { return shap; }
            set
            {
                shap = value;
                OnPropertyChanged();
            }
        }

        public System.Windows.Media.Color Color
        {
            get
            {
                return Tetromino.getShapeMediaColor(Shape);
            }
        }

        public Cell() { }

        protected void OnPropertyChanged()
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Color"));
        }
    }
}
