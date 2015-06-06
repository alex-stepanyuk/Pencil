using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Controls;

namespace Pencil
{
    class MyView
    {
        private Guid _id;        //id фигуры внутри которой мышка
        private int _side;      //сторона фигуры над которой мышка        
        public int OldX;
        public int OldY;

        public readonly List<MyRectangle> Rectangles;
        public readonly List<MyLine> Lines;

        public MyView()
        {
            Rectangles = new List<MyRectangle>();
            Lines = new List<MyLine>();
        }

        public Guid GetId()
        {
            return _id;
        }

        public int GetSide()
        {
            return _side;
        }

        public bool IsInto(int mouseX, int mouseY)
        {
            for (int i = Rectangles.Count - 1; i >= 0; i--)
            {
                MyRectangle rect = Rectangles[i];
                if ((rect.X - 3 < mouseX) && (rect.X + rect.Width + 3 > mouseX) && (rect.Y - 3 < mouseY) && (rect.Y + rect.Height + 3 > mouseY))
                {
                    _id = rect.Id;
                    if ((mouseX >= rect.X - 3) && (mouseX <= rect.X + 3)) _side = 1;
                    else if ((mouseX >= rect.X + rect.Width - 3) && (mouseX <= rect.X + rect.Width + 3)) _side = 3;
                    else if ((mouseY >= rect.Y - 3) && (mouseY <= rect.Y + 3)) _side = 2;
                    else if ((mouseY >= rect.Y + rect.Height - 3) && (mouseY <= rect.Y + rect.Height + 3)) _side = 4;
                    else _side = 0;
                    return true;
                }
            }

            _side = 0;
            _id = Guid.Empty;
            return false;
        }

        public void MoveRect(int mouseX, int mouseY)
        {
            if (Rectangles.Any(s => s.Id == _id))
            {
                Rectangles.First(s => s.Id == _id).Move(OldX, OldY, mouseX, mouseY);
            }
        }

        public void ResizeRect(int mouseX, int mouseY)
        {
            MyRectangle temp = Rectangles.First(s => s.Id == _id);
            temp.Resize(OldX, OldY, mouseX, mouseY, _side);
        }

        public void AddRect(Canvas canvas, MyRectangle rect)
        {
            Rectangles.Add(rect);
            canvas.Children.Add(rect.GetRect());
        }

        public void AddLine(MyLine line)
        {
            Lines.Add(line);
        }

    }
}
