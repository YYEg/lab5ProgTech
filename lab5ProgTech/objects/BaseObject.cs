﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing.Drawing2D;

namespace lab5ProgTech.objects
{
    internal class BaseObject
    {
        public float X;
        public float Y;
        public float Angle;
        public Action<BaseObject, BaseObject> OnOverlap;

        public BaseObject(float x, float y, float angle)
        {
            X = x;
            Y = y;
            Angle = angle;
        }

        public Matrix GetTransform()
        {
            var matrix = new Matrix();
            matrix.Translate(X, Y); // смещаем ее в пространстве
            matrix.Rotate(Angle);
            return matrix;
        }
        public virtual void Render(Graphics g) // виртуальный метод для отрисовки
        {
            // тут пусто
        }

        public virtual GraphicsPath GetGraphicsPath()
        {
            return new GraphicsPath();
        }

        public virtual bool Overlaps(BaseObject obj, Graphics g)
        {
            var path1 = this.GetGraphicsPath();
            var path2 = obj.GetGraphicsPath();

            // применяем к объектам матрицы трансформации
            path1.Transform(this.GetTransform());
            path2.Transform(obj.GetTransform());

            // используем метод Region, который позволяет определить 
            // пересечение объектов в данном графическом контексте
            var region = new Region(path1);
            region.Intersect(path2); // пересекаем формы
            return !region.IsEmpty(g); // если полученная форма не пуста то значит было пересечение
        }
        public virtual void Overlap(BaseObject obj)
        {
            if (this.OnOverlap != null)  //если к полю есть привязанные функции
            {
                this.OnOverlap(this, obj);  //то вызываем их
            }
        }
    }
}
