using System;
using System.Collections.Generic;   
using System.Text;
using System.Drawing;

namespace _PAIN__WPF___Tetris.Models
{
    class Tetromino
    {
        public enum Shapes { I, O, T, S, Z, J, L }

        public Shapes Shape { get; private set; }
        public Color Color { get; private set; }
        public Position Position { get; set; }
        public TetrominoRotation Rotation { get; private set; }


        public Tetromino(Shapes shape)
        {
            Shape = shape;
            Color = Tetromino.getShapeColor(shape);
            StartPosition();
            Rotation = new TetrominoRotation(Shape);
        }

        public static Color getShapeColor(Shapes? _shape)
        {
            switch(_shape)
            {
                case Tetromino.Shapes.I: return Color.Blue;
                case Tetromino.Shapes.O: return Color.Yellow;
                case Tetromino.Shapes.T: return Color.Purple;
                case Tetromino.Shapes.S: return Color.Green;
                case Tetromino.Shapes.Z: return Color.Red;
                case Tetromino.Shapes.J: return Color.DarkBlue;
                case Tetromino.Shapes.L: return Color.Orange;
            }

            return Color.LightBlue;
        }

        public static System.Windows.Media.Color getShapeMediaColor(Shapes? _shape)
        {
            Color color = getShapeColor(_shape);

            return System.Windows.Media.Color.FromArgb(color.A, color.R, color.G, color.B);
        }

        public int GetTopPosition()
        {
            int[,] pattern = Rotation.GetCurPattern();
            int patternHeight = pattern.GetLength(0);
            int patternWidth = pattern.GetLength(1);

            for (int y = 0; y < patternHeight; y++)
            {
                for (int x = 0; x < patternWidth; x++)
                {
                    if (pattern[y, x] == 1)
                        return (Position.Y + y);
                }
            }

            return 0;
        }

        private void StartPosition()
        {
            Position =  new Position();
            Position.X = ViewModels.ViewModelGrid.WIDTH / 2;
            Position.X -= 2;
            Position.Y = -3;

            switch(Shape)
            {
                case Tetromino.Shapes.O:
                    Position.Y = -2;
                    Position.X += 1;
                    break;
                case Tetromino.Shapes.I:
                    Position.Y = -2;
                    break;
                case Tetromino.Shapes.T: 
                case Tetromino.Shapes.S: 
                case Tetromino.Shapes.Z: 
                case Tetromino.Shapes.J: 
                case Tetromino.Shapes.L:
                    break;
            }
        }


    }
}
