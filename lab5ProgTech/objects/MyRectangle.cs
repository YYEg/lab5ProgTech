using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
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
            if (color)
            {
                g.FillEllipse(new SolidBrush(Color.LawnGreen), -15, -15, 30, 30);
            }
            else
            {
                g.FillEllipse(new SolidBrush(Color.White), -15, -15, 30, 30);
            }

        }

        public override GraphicsPath GetGraphicsPath()
        {
            var path = base.GetGraphicsPath();
            path.AddEllipse(0, 0, 30, 30);

            return path;
        }
    }
}
