using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab5ProgTech.objects
{
    internal class Player : BaseObject

    {
        public float vX, vY;
        public Action<Marker> OnMarkerOverlap;
        public Action<RedZone> OnRedOverlap;
        public Action<MyRectangle> OnScoreOverlap;
        

        public Player(float x, float y, float angle) : base(x, y, angle)
        {

        }

        public override void Render(Graphics g)
        {
            if (color)
            {
                g.FillEllipse(new SolidBrush(Color.DodgerBlue), -15, -15, 30, 30);
                g.DrawEllipse(new Pen(Color.Black, 2), -15, -15, 30, 30);
                g.DrawLine(new Pen(Color.Black, 2), 0, 0, 25, 0);
            }
            else
            {
                g.FillEllipse(new SolidBrush(Color.White), -15, -15, 30, 30);
                g.DrawEllipse(new Pen(Color.Black, 2), -15, -15, 30, 30);
                g.DrawLine(new Pen(Color.Black, 2), 0, 0, 25, 0);
            }
        }
        public override GraphicsPath GetGraphicsPath()
        {
            var path = base.GetGraphicsPath();
            path.AddEllipse(-15, -15, 30, 30);

            return path;
        }
        public override void Overlap(BaseObject obj)
        {
            base.Overlap(obj);
            if (obj is Marker)
            {
                OnMarkerOverlap(obj as Marker);
            }
            
            if (obj is MyRectangle)
            {
                OnScoreOverlap(obj as MyRectangle);
            }
            if (obj is RedZone)
            {
                OnRedOverlap(obj as RedZone);
            }
            
        }
    }
}
