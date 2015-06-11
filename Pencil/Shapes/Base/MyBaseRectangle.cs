using System;
using System.Collections.Generic;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace Pencil.Shapes.Base
{
    class MyBaseRectangle: MyBaseShape
    {
        protected Rectangle _rect;
        protected List<MyBaseLine> _startLines;
        protected List<MyBaseLine> _endLines;

        public void UpdateX()
        {
            foreach (var line in _startLines)
                line.X = X + Width/ 2;
            foreach (var line in _endLines)
                line.X2 = X + Width / 2;
        }

        public void UpdateY()
        {
            foreach (var line in _startLines)
                line.Y = Y + Height / 2;
            foreach (var line in _endLines)
                line.Y2 = Y + Height / 2;
        }

        public MyBaseRectangle()
        {
            _rect = new Rectangle
            {
                Stroke = Brushes.Black,
                StrokeThickness = 3,
                Fill = new SolidColorBrush(Colors.Orange) {Opacity = 0.9}
            };
            
            Id = Guid.NewGuid();

            _startLines = new List<MyBaseLine>();
            _endLines = new List<MyBaseLine>();
        }

        public Rectangle GetRect()
        {
            return _rect;
        }

        public override Guid Id
        {
            get { return _id; }
            set
            {
                _id = value;
                _rect.Tag = _id;
            }
        }

        public override double X
        {
            get { return Canvas.GetLeft(_rect); }
            set
            {
                Canvas.SetLeft(_rect, value);
                UpdateX();
            }
        }

        public override double Y
        {
            get { return Canvas.GetTop(_rect); }
            set
            {
                Canvas.SetTop(_rect, value);
                UpdateY();
            }
        }

        public double Width
        {
            get { return _rect.Width; }
            set
            {
                _rect.Width = value;
                UpdateX();
            }
        }

        public double Height
        {
            get { return _rect.Height; }
            set
            {
                _rect.Height = value;
                UpdateY();
            }
        }

        public void AddLine(MyBaseLine line, bool isStart)
        {
            if (isStart)
            {
                _startLines.Add(line);
                line.Rect1 = this;
            }
            else
            {
                _endLines.Add(line);
                line.Rect2 = this;
            }
        }

        public void RemoveLines(Canvas canvas)
        {
            foreach (var line in _startLines)
            {
                line.Rect2._endLines.Remove(line);
                canvas.Children.Remove(line.GetLine());
            }

            foreach (var line in _endLines)
            {
                line.Rect1._startLines.Remove(line);
                canvas.Children.Remove(line.GetLine());
            }

            _startLines.Clear();
            _endLines.Clear();
        }

        public override void Draw(int x1, int y1, int x2, int y2)
        {
            if (x2 > x1)
            {
                Width = x2 - x1;
                X = x1;
            }
            else
            {
                Width = x1 - x2;
                X = x2;
            }

            if (y2 > y1)
            {
                Height = y2 - y1;
                Y = y1;
            }
            else
            {
                Height = y1 - y2;
                Y = y2;
            }
        }

        public void Resize(int x1, int y1, int x2, int y2, SideType side)
        {
            switch (side)
            {
                case SideType.Left:
                    X = x2;
                    Width = x1 - X;
                    break;
                case SideType.Top:
                    Y = y2;
                    Height = y1 - Y;
                    break;
                case SideType.Right:
                    Width = x2 - X;
                    break;
                case SideType.Bottom:
                    Height = y2 - Y;
                    break;
            }
        }

        public void Move(int x1, int y1, int x2, int y2)
        {
            X = x2 - x1;
            Y = y2 - y1;
        }

        public SideType IsInto(int mouseX, int mouseY)
        {
            if ((X - 3 < mouseX) && (X + Width + 3 > mouseX) && (Y - 3 < mouseY) && (Y + Height + 3 > mouseY))
            {
                if ((mouseX >= X - 3) && (mouseX <= X + 3)) return SideType.Left;
                else if ((mouseX >= X + Width - 3) && (mouseX <= X + Width + 3)) return SideType.Right;
                else if ((mouseY >= Y - 3) && (mouseY <= Y + 3)) return SideType.Top;
                else if ((mouseY >= Y + Height - 3) && (mouseY <= Y + Height + 3)) return SideType.Bottom;
                else return SideType.Into;
            }
            return SideType.Out;
        }
        
    }
}
