using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Controls;

namespace Pencil
{
    class MyView
    {
        private Guid _id;        //id фигуры внутри которой мышка
        private SideType _side;      //сторона фигуры над которой мышка        
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

        public SideType GetSide()
        {
            return _side;
        }

        public bool IsIntoAny(int mouseX, int mouseY)
        {
            for (int i = Rectangles.Count - 1; i >= 0; i--)
            {
                MyRectangle rect = Rectangles[i];
                SideType temp = rect.IsInto(mouseX, mouseY);
                if (temp != SideType.Out)
                {
                    _id = rect.Id;
                    _side = temp;
                    return true;
                }
            }

            _side = SideType.Out;
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

        public void MouseMove(int mouseX, int mouseY)
        {
            if (_id != Guid.Empty)
            {
                if (Rectangles.Any(s => s.Id == _id))
                {
                    if (_side == SideType.Into)
                    {
                        MoveRect(mouseX, mouseY);
                    }
                    else
                    {
                        ResizeRect(mouseX, mouseY);
                    }
                }
            }
            else
            {
                Rectangles.Last().Draw(OldX, OldY, mouseX, mouseY);
            }
        }

        public void MouseDown(int mouseX, int mouseY, Canvas canvas, int dash)
        {
            if (IsIntoAny(mouseX, mouseY))
            {
                if (Rectangles.Any(s => s.Id == _id))
                {
                    MyRectangle temp = Rectangles.First(s => s.Id == _id);

                    if (_side == SideType.Into)
                    {
                        OldX = mouseX - (int)temp.X;
                        OldY = mouseY - (int)temp.Y;
                    }
                    else
                    {
                        OldX = (int)(temp.Width + temp.X);
                        OldY = (int)(temp.Height + temp.Y);
                    }
                }
            }
            else
            {
                OldX = mouseX;
                OldY = mouseY;
                AddRect(canvas, new MyRectangle(dash));
            }
        }

    }
}
