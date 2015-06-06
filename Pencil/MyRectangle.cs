using System;
using System.Collections.Generic;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace Pencil
{
    class MyRectangle: MyBaseShape
    {
        private Rectangle _rect;
        private readonly List<MyLine> _startLines;
        private readonly List<MyLine> _endLines;

        public void UpdateX()
        {
            foreach (var line in _startLines)
                line.GetLine().X1 = X + Width/ 2;
            foreach (var line in _endLines)
                line.GetLine().X2 = X + Width / 2;
        }

        public void UpdateY()
        {
            
            foreach (var line in _startLines)
                line.GetLine().Y1 = Y + Height / 2;
            foreach (var line in _endLines)
                line.GetLine().Y2 = Y + Height / 2;
        }

        public MyRectangle(int dash)
        {
            _rect = new Rectangle
            {
                Stroke = Brushes.Black,
                StrokeThickness = 3,
                Fill = new SolidColorBrush(Colors.Orange) {Opacity = 0.9}
            };

            if (dash == 2)
                _rect.StrokeDashArray = new DoubleCollection { 2, 2 };
            
            Id = Guid.NewGuid();

            _startLines = new List<MyLine>();
            _endLines = new List<MyLine>();
        }

        public Rectangle GetRect()
        {
            return _rect;
        }

        public Guid Id
        {
            get { return _id; }
            set
            {
                _id = value;
                _rect.Tag = _id;
            }
        }

        public double X
        {
            get { return Canvas.GetLeft(_rect); }
            set
            {
                Canvas.SetLeft(_rect, value);
                UpdateX();
            }
        }

        public double Y
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

        public void AddLine(MyLine line, bool isStart)
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

        public void Resize(int x1, int y1, int x2, int y2, int side)
        {
            switch (side)
            {
                case 1:
                    X = x2;
                    Width = x1 - X;
                    break;
                case 2:
                    Y = y2;
                    Height = y1 - Y;
                    break;
                case 3:
                    Width = x2 - X;
                    break;
                case 4:
                    Height = y2 - Y;
                    break;
            }
        }

        public void Move(int x1, int y1, int x2, int y2)
        {
            X = x2 - x1;
            Y = y2 - y1;
        }

    }
}
