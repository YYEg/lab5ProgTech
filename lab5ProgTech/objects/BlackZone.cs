using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Text;
using System.Linq;
using lab5ProgTech.objects;

namespace lab5ProgTech.objects
{
    class BlackZone : BaseObject
    {
        public List<BaseObject> inZone = new List<BaseObject>();
        public List<BaseObject> outZone = new List<BaseObject>();
        public BlackZone(float X, float Y, float Angle) : base(X, Y, Angle) { }

        public Action<BaseObject> BlackZoneOverlap;

        public override void Render(Graphics g)
        {
            foreach(var c in inZone)
            {
                if (!outZone.Contains(c))
                {
                    c.color = false;
                }
            foreach(var c1 in outZone)
                {
                    if (!inZone.Contains(c1))
                    {
                        c1.color = true;
                    }
                }
            }
            /*foreach (var obj in outZone)
            {
                if (!inZone.Contains(obj))
                {
                    obj.color = true;
                }
            }*/
            g.FillRectangle(new SolidBrush(Color.Black), -100, -200, 200, 700);
            outZone = inZone.ToList();
            inZone.Clear();
        }

        public override GraphicsPath GetGraphicsPath()
        {
            var path = base.GetGraphicsPath();
            path.AddRectangle(new Rectangle(-100, -200, 200, 700));
            return path;
        }

        public override void Overlap(BaseObject obj)
        {
            inZone.Add(obj);
        }
    }
}
