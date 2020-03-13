using System;
using System.Collections.Generic;
using System.Text;

namespace _PAIN__WPF___Tetris.Models
{
    class TetrominoRotation
    {
        public short RotationCounter { get; private set; }
        public short[,] Pattern { get; private set; }

        private List<short[,]> Patterns;
        private short RotationsNumber;
        private Tetromino.Shapes Shape;

        private TetrominoRotation() { }

        public TetrominoRotation(Tetromino.Shapes shape)
        {
            Patterns = new List<short[,]>();
            Shape = shape;
            RotationCounter = 0;
            InitPatterns();
        }


        public short[,] GetCurPattern()
        {
            return Patterns[RotationCounter];
        }

        public short[,] GetNextPattern()
        {
            return Patterns[NextPatternNumber()];
        }

        public void SetNextPattern()
        {
            RotationCounter = NextPatternNumber();
            Pattern = Patterns[RotationCounter];
        }

        public List<short> GetRows()
        {
            return new List<short>();
        }

        private short NextPatternNumber()
        {
            if (RotationCounter == (RotationsNumber - 1))
                return 0;
            else
                return (short)(RotationCounter + 1);
        }


        private void InitPatterns()
        {
            switch (Shape)
            {
                case Tetromino.Shapes.O:
                    InitPatternsO();
                    break;
                case Tetromino.Shapes.I:
                    InitPatternsI();
                    break;
                case Tetromino.Shapes.J:
                    InitPatternsJ();
                    break;
                case Tetromino.Shapes.T:
                    InitPatternsT();
                    break;
                case Tetromino.Shapes.S:
                    InitPatternsS();
                    break;
                case Tetromino.Shapes.Z:
                    InitPatternsZ();
                    break;
                case Tetromino.Shapes.L:
                    InitPatternsL();
                    break;
            }
        }

        private void InitPatternsO()
        {
            RotationsNumber = 1;
            Patterns.Add(new short[,]
            {
                { 1, 1 },
                { 1, 1 }
            });

        }

        private void InitPatternsI()
        {
            RotationsNumber = 4;
            Patterns.Add(new short[,]
                    {
                        { 0, 0, 0, 0 },
                        { 1, 1, 1, 1 },
                        { 0, 0, 0, 0 },
                        { 0, 0, 0, 0 }
                    });
            Patterns.Add(new short[,]
                    {
                        { 0, 0, 1, 0 },
                        { 0, 0, 1, 0 },
                        { 0, 0, 1, 0 },
                        { 0, 0, 1, 0 }
                    });
            Patterns.Add(new short[,]
                    {
                        { 0, 0, 0, 0 },
                        { 0, 0, 0, 0 },
                        { 1, 1, 1, 1 },
                        { 0, 0, 0, 0 }
                    });
            Patterns.Add(new short[,]
                    {
                        { 0, 1, 0, 0 },
                        { 0, 1, 0, 0 },
                        { 0, 1, 0, 0 },
                        { 0, 1, 0, 0 }
                    });
        }

        private void InitPatternsJ()
        {
            RotationsNumber = 4;
            Patterns.Add(new short[,]
                    {
                        { 1, 0, 0 },
                        { 1, 1, 1 },
                        { 0, 0, 0 }
                    });
            Patterns.Add(new short[,]
                                {
                        { 0, 1, 1 },
                        { 0, 1, 0 },
                        { 0, 1, 0 }
                                });
            Patterns.Add(new short[,]
                                {
                        { 0, 0, 0 },
                        { 1, 1, 1 },
                        { 0, 0, 1 }
                                });
            Patterns.Add(new short[,]
                                {
                        { 0, 1, 0 },
                        { 0, 1, 0 },
                        { 1, 1, 0 }
                                });

        }

        private void InitPatternsS()
        {
            RotationsNumber = 4;
            Patterns.Add(new short[,]
                    {
                        { 0, 1, 1 },
                        { 1, 1, 0 },
                        { 0, 0, 0 }
                    });
            Patterns.Add(new short[,]
                    {
                        { 0, 1, 0 },
                        { 0, 1, 1 },
                        { 0, 0, 1 }
                    });
            Patterns.Add(new short[,]
                    {
                        { 0, 0, 0 },
                        { 0, 1, 1 },
                        { 1, 1, 0 }
                    });
            Patterns.Add(new short[,]
                    {
                        { 1, 0, 0 },
                        { 1, 1, 0 },
                        { 0, 1, 0 }
                    });
        }

        private void InitPatternsZ()
        {
            RotationsNumber = 4;
            Patterns.Add(new short[,]
                    {
                        { 1, 1, 0 },
                        { 0, 1, 1 },
                        { 0, 0, 0 }
                    });
            Patterns.Add(new short[,]
                    {
                        { 0, 0, 1 },
                        { 0, 1, 1 },
                        { 0, 1, 0 }
                    });
            Patterns.Add(new short[,]
                    {
                        { 0, 0, 0 },
                        { 1, 1, 0 },
                        { 0, 1, 1 }
                    });
            Patterns.Add(new short[,]
                    {
                        { 0, 1, 0 },
                        { 0, 1, 0 },
                        { 1, 1, 0 }
                    });
        }

        private void InitPatternsT()
        {
            RotationsNumber = 4;
            Patterns.Add(new short[,]
                    {
                        { 0, 1, 0 },
                        { 1, 1, 1 },
                        { 0, 0, 0 }
                    });
            Patterns.Add(new short[,]
                    {
                        { 0, 1, 0 },
                        { 0, 1, 1 },
                        { 0, 1, 0 }
                    });
            Patterns.Add(new short[,]
                    {
                        { 0, 0, 0 },
                        { 1, 1, 1 },
                        { 0, 1, 0 }
                    });
            Patterns.Add(new short[,]
                    {
                        { 0, 1, 0 },
                        { 1, 1, 0 },
                        { 0, 1, 0 }
                    });
        }

        private void InitPatternsL()
        {
            RotationsNumber = 4;
            Patterns.Add(new short[,]
                    {
                        { 0, 0, 1 },
                        { 1, 1, 1 },
                        { 0, 0, 0 }
                    });
            Patterns.Add(new short[,]
                                {
                        { 0, 1, 0 },
                        { 0, 1, 0 },
                        { 0, 1, 1 }
                                });
            Patterns.Add(new short[,]
                                {
                        { 0, 0, 0 },
                        { 1, 1, 1 },
                        { 1, 0, 0 }
                                });
            Patterns.Add(new short[,]
                                {
                        { 1, 1, 0 },
                        { 0, 1, 0 },
                        { 0, 1, 0 }
                                });

        }
    }
}
