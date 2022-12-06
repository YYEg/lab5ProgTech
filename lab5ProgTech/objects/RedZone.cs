using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;

using System.Drawing.Drawing2D;
using lab5ProgTech.objects;

namespace _5_laba.Objects
{
    class RedZone : BaseObject
    {
        int x = 3;
        int y = 3;
        public RedZone(float x, float y, float angle) : base(x, y, angle)
        {

        }

        // переопределяем Render
        public override void Render(Graphics g)
        {
            g.FillEllipse(new SolidBrush(Color.Pink), 0 - x / 2, 0 - y / 2, x, y);
            x++;
            y++;

        }
        //---
        public override GraphicsPath GetGraphicsPath()
        {
            var path = base.GetGraphicsPath();
            path.AddEllipse(0 - x / 2, 0 - y / 2, x, y);

            return path;
        }


    }
}