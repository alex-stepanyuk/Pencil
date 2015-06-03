using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Controls;

namespace Pencil
{
    class MyCanvas
    {
        private Guid _id;        //id фигуры внутри которой мышка
        private int _side;      //сторона фигуры над которой мышка        
        public int OldX;
        public int OldY;

        public readonly List<MyRectangle> Rectangles;
        public readonly List<MyLine> Lines;

        public MyCanvas()
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
                Rectangles.First(s => s.Id == _id).X = mouseX - OldX;
                Rectangles.First(s => s.Id == _id).Y = mouseY - OldY;
            }
        }

        public void ResizeRect(int mouseX, int mouseY)
        {
            switch (_side)
            {
                case 1: 
                    Rectangles.First(s => s.Id == _id).X = mouseX;
                    Rectangles.First(s => s.Id == _id).Width = OldX - Rectangles.First(s => s.Id == _id).X; 
                    break;
                case 2: 
                    Rectangles.First(s => s.Id == _id).Y = mouseY;
                    Rectangles.First(s => s.Id == _id).Height = OldY - Rectangles.First(s => s.Id == _id).Y; 
                    break;
                case 3: 
                    Rectangles.First(s => s.Id == _id).Width = mouseX - Rectangles.First(s => s.Id == _id).X; 
                    break;
                case 4: 
                    Rectangles.First(s => s.Id == _id).Height = mouseY - Rectangles.First(s => s.Id == _id).Y; 
                    break;
            }
        }

        public void DrawRect(int mouseX, int mouseY, MyRectangle rect)
        {
            if (mouseX > OldX)
            {
                rect.Width = mouseX - OldX;
                rect.X = OldX;
            }
            else
            {
                rect.Width = OldX - mouseX ;
                rect.X = mouseX;
            }

            if (mouseY > OldY)
            {
                rect.Height = mouseY - OldY;
                rect.Y = OldY;
            }
            else
            {
                rect.Height = OldY - mouseY;
                rect.Y = mouseY;
            }
        }

        public void AddRect(MyRectangle rect)
        {
            Rectangles.Add(rect);
        }

        public void DrawLine(int mouseX, int mouseY, MyLine line)
        {
            line.GetLine().X1 = OldX;
            line.GetLine().Y1 = OldY;
            line.GetLine().X2 = mouseX;
            line.GetLine().Y2 = mouseY;
        }

        public void AddLine(MyLine line)
        {
            Lines.Add(line);
        }
    }
}
