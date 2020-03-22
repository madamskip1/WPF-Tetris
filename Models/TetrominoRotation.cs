using System;
using System.Collections.Generic;
using System.Text;

namespace _PAIN__WPF___Tetris.Models
{
    class TetrominoRotation
    {
        public int[,] Pattern { get; private set; }

        private List<int[,]> patterns;
        private int rotationsNumber;
        private Tetromino.Shapes Shape;
        private int rotationCounter;

        public TetrominoRotation(Tetromino.Shapes shape)
        {
            patterns = new List<int[,]>();
            Shape = shape;
            rotationCounter = 0;
            InitPatterns();
            Pattern = patterns[0];
        }


        public int[,] GetCurPattern()
        {
            return Pattern;
        }

        public int[,] GetNextPattern()
        {
            return patterns[NextPatternNumber()];
        }

        public void SetNextPattern()
        {
            rotationCounter = NextPatternNumber();
            Pattern = patterns[rotationCounter];
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
            if (rotationCounter == (rotationsNumber - 1))
                return 0;
            else
                return (rotationCounter + 1);
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
            rotationsNumber = 1;
            patterns.Add(new int[,]
            {
                { 1, 1 },
                { 1, 1 }
            });

        }

        private void InitPatternsI()
        {
            rotationsNumber = 4;
            patterns.Add(new int[,]
                    {
                        { 0, 0, 0, 0 },
                        { 1, 1, 1, 1 },
                        { 0, 0, 0, 0 },
                        { 0, 0, 0, 0 }
                    });
            patterns.Add(new int[,]
                    {
                        { 0, 0, 1, 0 },
                        { 0, 0, 1, 0 },
                        { 0, 0, 1, 0 },
                        { 0, 0, 1, 0 }
                    });
            patterns.Add(new int[,]
                    {
                        { 0, 0, 0, 0 },
                        { 0, 0, 0, 0 },
                        { 1, 1, 1, 1 },
                        { 0, 0, 0, 0 }
                    });
            patterns.Add(new int[,]
                    {
                        { 0, 1, 0, 0 },
                        { 0, 1, 0, 0 },
                        { 0, 1, 0, 0 },
                        { 0, 1, 0, 0 }
                    });
        }

        private void InitPatternsJ()
        {
            rotationsNumber = 4;
            patterns.Add(new int[,]
                    {
                        { 1, 0, 0 },
                        { 1, 1, 1 },
                        { 0, 0, 0 }
                    });
            patterns.Add(new int[,]
                                {
                        { 0, 1, 1 },
                        { 0, 1, 0 },
                        { 0, 1, 0 }
                                });
            patterns.Add(new int[,]
                                {
                        { 0, 0, 0 },
                        { 1, 1, 1 },
                        { 0, 0, 1 }
                                });
            patterns.Add(new int[,]
                                {
                        { 0, 1, 0 },
                        { 0, 1, 0 },
                        { 1, 1, 0 }
                                });

        }

        private void InitPatternsS()
        {
            rotationsNumber = 4;
            patterns.Add(new int[,]
                    {
                        { 0, 1, 1 },
                        { 1, 1, 0 },
                        { 0, 0, 0 }
                    });
            patterns.Add(new int[,]
                    {
                        { 0, 1, 0 },
                        { 0, 1, 1 },
                        { 0, 0, 1 }
                    });
            patterns.Add(new int[,]
                    {
                        { 0, 0, 0 },
                        { 0, 1, 1 },
                        { 1, 1, 0 }
                    });
            patterns.Add(new int[,]
                    {
                        { 1, 0, 0 },
                        { 1, 1, 0 },
                        { 0, 1, 0 }
                    });
        }

        private void InitPatternsZ()
        {
            rotationsNumber = 4;
            patterns.Add(new int[,]
                    {
                        { 1, 1, 0 },
                        { 0, 1, 1 },
                        { 0, 0, 0 }
                    });
            patterns.Add(new int[,]
                    {
                        { 0, 0, 1 },
                        { 0, 1, 1 },
                        { 0, 1, 0 }
                    });
            patterns.Add(new int[,]
                    {
                        { 0, 0, 0 },
                        { 1, 1, 0 },
                        { 0, 1, 1 }
                    });
            patterns.Add(new int[,]
                    {
                        { 0, 1, 0 },
                        { 1, 1, 0 },
                        { 1, 0, 0 }
                    });
        }

        private void InitPatternsT()
        {
            rotationsNumber = 4;
            patterns.Add(new int[,]
                    {
                        { 0, 1, 0 },
                        { 1, 1, 1 },
                        { 0, 0, 0 }
                    });
            patterns.Add(new int[,]
                    {
                        { 0, 1, 0 },
                        { 0, 1, 1 },
                        { 0, 1, 0 }
                    });
            patterns.Add(new int[,]
                    {
                        { 0, 0, 0 },
                        { 1, 1, 1 },
                        { 0, 1, 0 }
                    });
            patterns.Add(new int[,]
                    {
                        { 0, 1, 0 },
                        { 1, 1, 0 },
                        { 0, 1, 0 }
                    });
        }

        private void InitPatternsL()
        {
            rotationsNumber = 4;
            patterns.Add(new int[,]
                    {
                        { 0, 0, 1 },
                        { 1, 1, 1 },
                        { 0, 0, 0 }
                    });
            patterns.Add(new int[,]
                                {
                        { 0, 1, 0 },
                        { 0, 1, 0 },
                        { 0, 1, 1 }
                                });
            patterns.Add(new int[,]
                                {
                        { 0, 0, 0 },
                        { 1, 1, 1 },
                        { 1, 0, 0 }
                                });
            patterns.Add(new int[,]
                                {
                        { 1, 1, 0 },
                        { 0, 1, 0 },
                        { 0, 1, 0 }
                                });

        }
    }
}
