﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Snake
{
    public class SnakePart
    {
        public int X { get; set; }
        public int Y { get; set; }

        public SnakePart(int x, int y)
        {
            X = x;
            Y = y;
        }
    }
}
