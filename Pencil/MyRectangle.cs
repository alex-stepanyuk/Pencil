using System;
using System.Collections.Generic;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace Pencil
{
    class MyRectangle
    {
        private Rectangle _rect;
        private Guid _id;
        private readonly List<MyLine> _startLines;
        private readonly List<MyLine> _endLines;

        public void UpdateProperty(string property)
        {
            switch (property)
            {
                case "X":
                    foreach (var line in _startLines)
                        line.GetLine().X1 = X + Width/ 2;
                    foreach (var line in _endLines)
                        line.GetLine().X2 = X + Width / 2;
                    break;
                case "Y":
                    foreach (var line in _startLines)
                        line.GetLine().Y1 = Y + Height / 2;
                    foreach (var line in _endLines)
                        line.GetLine().Y2 = Y + Height / 2;
                    break;
            }
        }

        public MyRectangle(Canvas canvas, bool dash)
        {
            _rect = new Rectangle
            {
                Stroke = Brushes.Black,
                StrokeThickness = 3,
                Fill = new SolidColorBrush(Colors.Orange) {Opacity = 0.9}
            };

            if (dash)
                _rect.StrokeDashArray = new DoubleCollection { 2, 2 };
            
            canvas.Children.Add(_rect);
            Id = Guid.NewGuid();

            _startLines = new List<MyLine>();
            _endLines = new List<MyLine>();
        }

        public Rectangle Rect()
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
                UpdateProperty("X");
            }
        }

        public double Y
        {
            get { return Canvas.GetTop(_rect); }
            set
            {
                Canvas.SetTop(_rect, value);
                UpdateProperty("Y");
            }
        }

        public double Width
        {
            get { return _rect.Width; }
            set
            {
                _rect.Width = value;
                UpdateProperty("X");
            }
        }

        public double Height
        {
            get { return _rect.Height; }
            set
            {
                _rect.Height = value;
                UpdateProperty("Y");
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

    }
}
