﻿using System;
using System.Collections.Generic;
using System.Text;

namespace _PAIN__WPF___Tetris.Models
{
    class Position
    {
        public int X { get; set; }
        public int Y { get; set; }

        public Position(int x = 0, int y = 0)
        {
            X = x;
            Y = y;
        }
    }
}
