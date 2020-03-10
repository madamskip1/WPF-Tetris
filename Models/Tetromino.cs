using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

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
            Position = StartPosition();
            Rotation = new TetrominoRotation(Shape);
        }

        public static Color getShapeColor(Shapes _shape)
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

            throw new Exception("Unknown Shape");
        }

        private Position StartPosition()
        {
            return new Position();
        }


    }
}
