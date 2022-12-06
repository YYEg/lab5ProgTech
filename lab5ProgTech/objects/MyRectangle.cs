﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab5ProgTech.objects
{
    internal class MyRectangle : BaseObject
    {
        public MyRectangle(float x, float y, float angle) : base(x, y, angle)
        {

        }
        public override void Render(Graphics g)
        {
            g.FillRectangle(new SolidBrush(Color.Yellow), 0, 0, 50, 30);
            g.DrawRectangle(new Pen(Color.Red, 2), 0, 0, 50, 30);
        }
    }
}
