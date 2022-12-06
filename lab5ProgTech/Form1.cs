using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace lab5ProgTech
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void pbMain_Paint(object sender, PaintEventArgs e)
        {
            var g = e.Graphics; // вытащили объект графики из события

            g.Clear(Color.Aqua); //залили фон

            g.FillRectangle(new SolidBrush(Color.Yellow), 200, 100, 50, 30); // заливка прямоугольника
            g.DrawRectangle(new Pen(Color.Red), 200, 100, 50, 30);  // рисуем прямоугольную рамку
            
        }

    }
}
