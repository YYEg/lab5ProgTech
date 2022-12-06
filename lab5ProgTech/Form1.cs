using lab5ProgTech.objects;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection.Emit;
using System.Runtime.CompilerServices;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Rebar;

namespace lab5ProgTech
{
    public partial class Form1 : Form
    {
        List<BaseObject> objects = new List<BaseObject>();
        Player player;
        Marker marker;
        MyRectangle circleOne;
        MyRectangle circleTwo;
        public double vX, vY;
        RedZone redZone;
        BlackZone blackZone;
        int score;

        Random rand = new Random();

        public Form1()
        {
            InitializeComponent();

            player = new Player(pbMain.Width / 2, pbMain.Height / 2, 0);
            marker = new Marker(pbMain.Width / 2 + 50, pbMain.Height / 2 + 50, 0);
            circleOne = new MyRectangle(rand.Next(30, pbMain.Width - 30), rand.Next(30, pbMain.Height - 30), 0);
            circleTwo = new MyRectangle(rand.Next(30, pbMain.Width - 30), rand.Next(30, pbMain.Height - 30), 0);
            redZone = new RedZone(rand.Next(30, pbMain.Width - 30), rand.Next(30, pbMain.Height - 30), 0);
            blackZone = new BlackZone(-100, 0, 0);



            player.OnOverlap += (p, obj) =>
            {
                txtLog.Text = $"[{DateTime.Now:HH:mm:ss:ff}] Игрок пересекся с {obj}\n" + txtLog.Text;
            };
            

            player.OnMarkerOverlap += (m) =>
            {
                objects.Remove(m);
                marker = null;
            };
            player.OnScoreOverlap += (m) =>
            {

                objects.Remove(m);
                circleOne = null;
                circleOne = new MyRectangle(0, 0, 0);
                objects.Add(circleOne);
                circleOne.X = rand.Next(30, pbMain.Width - 30);
                circleOne.Y = rand.Next(30, pbMain.Height - 30);
                score++;

            };

            player.OnRedOverlap += (m) =>
            {
                objects.Remove(m);
                redZone = null;
                redZone = new RedZone(0, 0, 0);
                objects.Add(redZone);
                redZone.X = rand.Next(30, pbMain.Width - 30);
                redZone.Y = rand.Next(30, pbMain.Height - 30);
                score--;
            };

            blackZone.BlackZoneOverlap += (o) =>
            {
                txtLog.Text = $"[{DateTime.Now:HH:mm:ss:ff}] АААААААААААААААААААААААААААААААААААААААААААААААААААААААААААААААААААА!А!\n";
            };


            objects.Add(blackZone);
            objects.Add(marker);
            objects.Add(player);      
            objects.Add(circleOne);
            objects.Add(circleTwo);
            objects.Add(redZone);
            
        }

        private void pbMain_Paint(object sender, PaintEventArgs e)
        {
            var g = e.Graphics;
            g.Clear(Color.White);

            label1.Text = "Счёт: " + score;

            updatePlayer();

            // пересчитываем пересечения
            foreach (var obj1 in objects.ToList())
            {
                foreach (var obj2 in objects.ToList())
                {
                    if (obj1 != obj2 && obj1.Overlaps(obj2, g))
                    {
                        obj1.Overlap(obj2);
                    }
                }
            }

            // рендерим объекты
            foreach (var obj in objects)
            {
                g.Transform = obj.GetTransform();
                obj.Render(g);
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            ZoneGo();
            pbMain.Invalidate();
        }

        private void pbMain_MouseClick(object sender, MouseEventArgs e)
        {
            if (marker == null)
            {
                marker = new Marker(0, 0, 0);
                objects.Add(marker); // и главное не забыть пололжить в objects
            }

            marker.X = e.X;
            marker.Y = e.Y;
        }

        private void updatePlayer()
        {
            if (marker != null)
            {
                float dx = marker.X - player.X;
                float dy = marker.Y - player.Y;
                float length = (float)Math.Sqrt(dx * dx + dy * dy);
                dx /= length; 
                dy /= length;

                player.vX += dx * 1f;
                player.vY += dy * 1f;

                player.Angle = (float)(90 - Math.Atan2(player.vX, player.vY) * 180 / Math.PI);
            }
            player.vX += -player.vX * 0.1f;
            player.vY += -player.vY * 0.1f;
            player.X += player.vX;
            player.Y += player.vY;
        }
        private void ZoneGo()
        {
            if (blackZone.X < 750)
            {
                blackZone.X += 4;
            }
            else
            {
                blackZone.X = -100;
            }
        }
    }
}
