using System;
using System.Collections.Generic;
using System.Text;

namespace _PAIN__WPF___Tetris.Models
{
    class TetrominoRotation
    {
        public int RotationCounter { get; private set; }
        public int[,] Pattern { get; private set; }

        private List<int[,]> Patterns;
        private int RotationsNumber;
        private Tetromino.Shapes Shape;

        private TetrominoRotation() { }

        public TetrominoRotation(Tetromino.Shapes shape)
        {
            Patterns = new List<int[,]>();
            Shape = shape;
            RotationCounter = 0;
            InitPatterns();
            Pattern = Patterns[0];
        }


        public int[,] GetCurPattern()
        {
            return Pattern;
        }

        public int[,] GetNextPattern()
        {
            return Patterns[NextPatternNumber()];
        }

        public void SetNextPattern()
        {
            RotationCounter = NextPatternNumber();
            Pattern = Patterns[RotationCounter];
        }

        public List<int> RowsToCheck()
        {
            List<int> rows = new List<int>();

            int patternHeight = Pattern.GetLength(0);
            int patternWidth = Pattern.GetLength(1);

            for (int y = 0; y < patternHeight; y++)
            {
                for (int x = 0; x < patternWidth; x++)
                    if (Pattern[y, x] == 1)
                    {
                        rows.Add(y);
                        break;
                    }
            }

            return rows;
        }

        private int NextPatternNumber()
        {
            if (RotationCounter == (RotationsNumber - 1))
                return 0;
            else
                return (RotationCounter + 1);
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
            Patterns.Add(new int[,]
            {
                { 1, 1 },
                { 1, 1 }
            });

        }

        private void InitPatternsI()
        {
            RotationsNumber = 4;
            Patterns.Add(new int[,]
                    {
                        { 0, 0, 0, 0 },
                        { 1, 1, 1, 1 },
                        { 0, 0, 0, 0 },
                        { 0, 0, 0, 0 }
                    });
            Patterns.Add(new int[,]
                    {
                        { 0, 0, 1, 0 },
                        { 0, 0, 1, 0 },
                        { 0, 0, 1, 0 },
                        { 0, 0, 1, 0 }
                    });
            Patterns.Add(new int[,]
                    {
                        { 0, 0, 0, 0 },
                        { 0, 0, 0, 0 },
                        { 1, 1, 1, 1 },
                        { 0, 0, 0, 0 }
                    });
            Patterns.Add(new int[,]
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
            Patterns.Add(new int[,]
                    {
                        { 1, 0, 0 },
                        { 1, 1, 1 },
                        { 0, 0, 0 }
                    });
            Patterns.Add(new int[,]
                                {
                        { 0, 1, 1 },
                        { 0, 1, 0 },
                        { 0, 1, 0 }
                                });
            Patterns.Add(new int[,]
                                {
                        { 0, 0, 0 },
                        { 1, 1, 1 },
                        { 0, 0, 1 }
                                });
            Patterns.Add(new int[,]
                                {
                        { 0, 1, 0 },
                        { 0, 1, 0 },
                        { 1, 1, 0 }
                                });

        }

        private void InitPatternsS()
        {
            RotationsNumber = 4;
            Patterns.Add(new int[,]
                    {
                        { 0, 1, 1 },
                        { 1, 1, 0 },
                        { 0, 0, 0 }
                    });
            Patterns.Add(new int[,]
                    {
                        { 0, 1, 0 },
                        { 0, 1, 1 },
                        { 0, 0, 1 }
                    });
            Patterns.Add(new int[,]
                    {
                        { 0, 0, 0 },
                        { 0, 1, 1 },
                        { 1, 1, 0 }
                    });
            Patterns.Add(new int[,]
                    {
                        { 1, 0, 0 },
                        { 1, 1, 0 },
                        { 0, 1, 0 }
                    });
        }

        private void InitPatternsZ()
        {
            RotationsNumber = 4;
            Patterns.Add(new int[,]
                    {
                        { 1, 1, 0 },
                        { 0, 1, 1 },
                        { 0, 0, 0 }
                    });
            Patterns.Add(new int[,]
                    {
                        { 0, 0, 1 },
                        { 0, 1, 1 },
                        { 0, 1, 0 }
                    });
            Patterns.Add(new int[,]
                    {
                        { 0, 0, 0 },
                        { 1, 1, 0 },
                        { 0, 1, 1 }
                    });
            Patterns.Add(new int[,]
                    {
                        { 0, 1, 0 },
                        { 1, 1, 0 },
                        { 1, 0, 0 }
                    });
        }

        private void InitPatternsT()
        {
            RotationsNumber = 4;
            Patterns.Add(new int[,]
                    {
                        { 0, 1, 0 },
                        { 1, 1, 1 },
                        { 0, 0, 0 }
                    });
            Patterns.Add(new int[,]
                    {
                        { 0, 1, 0 },
                        { 0, 1, 1 },
                        { 0, 1, 0 }
                    });
            Patterns.Add(new int[,]
                    {
                        { 0, 0, 0 },
                        { 1, 1, 1 },
                        { 0, 1, 0 }
                    });
            Patterns.Add(new int[,]
                    {
                        { 0, 1, 0 },
                        { 1, 1, 0 },
                        { 0, 1, 0 }
                    });
        }

        private void InitPatternsL()
        {
            RotationsNumber = 4;
            Patterns.Add(new int[,]
                    {
                        { 0, 0, 1 },
                        { 1, 1, 1 },
                        { 0, 0, 0 }
                    });
            Patterns.Add(new int[,]
                                {
                        { 0, 1, 0 },
                        { 0, 1, 0 },
                        { 0, 1, 1 }
                                });
            Patterns.Add(new int[,]
                                {
                        { 0, 0, 0 },
                        { 1, 1, 1 },
                        { 1, 0, 0 }
                                });
            Patterns.Add(new int[,]
                                {
                        { 1, 1, 0 },
                        { 0, 1, 0 },
                        { 0, 1, 0 }
                                });

        }
    }
}
