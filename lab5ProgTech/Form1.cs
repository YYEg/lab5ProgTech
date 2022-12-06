using lab5ProgTech.objects;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace lab5ProgTech
{
    public partial class Form1 : Form
    {
        List<BaseObject> objects = new List<BaseObject>();
        Player player;
        Marker marker;
        public double vX, vY;

        public Form1()
        {
            InitializeComponent();

            player = new Player(pbMain.Width / 2, pbMain.Height / 2, 0);
            marker = new Marker(pbMain.Width / 2 + 50, pbMain.Height / 2 + 50, 0);

            objects.Add(marker);
            
            objects.Add(player);
          
            objects.Add(new MyRectangle(50, 50, 0));
            objects.Add(new MyRectangle(100, 100, 45));
            objects.Add(new MyRectangle(200, 200, 90));
        }

        private void pbMain_Paint(object sender, PaintEventArgs e)
        {
            var g = e.Graphics;

            g.Clear(Color.White);

            foreach (var obj in objects.ToList())
            {
                if (obj != player && player.Overlaps(obj, g))
                {
                    // это не трогаю
                    txtLog.Text = $"[{DateTime.Now:HH:mm:ss:ff}] Игрок пересекся с {obj}\n" + txtLog.Text;

                    // тут проверяю что достиг маркера
                    if (obj == marker)
                    {
                        // если достиг, то удаляю маркер из оригинального objects
                        objects.Remove(marker);
                        marker = null; // и обнуляю маркер
                    }
                }

                g.Transform = obj.GetTransform();
                obj.Render(g);
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            updatePlayer();

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
                dx /= length;  //разделить переменную на значение и ответ присвоить этой же переменной.
                dy /= length;

                // используем вектор dx, dy
                // как вектор ускорения, точнее даже вектор притяжения
                // который притягивает игрока к маркеру
                player.vX += dx * 1f;
                player.vY += dy * 1f;

                // расчитываем угол поворота игрока 
                player.Angle = (float)(90 - Math.Atan2(player.vX, player.vY) * 180 / Math.PI);
            }
            // тормозящий момент,
            // нужен чтобы, когда игрок достигнет маркера произошло постепенное замедление
            player.vX += -player.vX * 0.1f;
            player.vY += -player.vY * 0.1f;
            // пересчет позиции игрока с помощью вектора скорости
            player.X += player.vX;
            player.Y += player.vY;
        }
    }
}
