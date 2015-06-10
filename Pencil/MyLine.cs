using System;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace Pencil
{
    class MyLine: MyBaseShape
    {
        private MyRectangle _rect1 = null;
        private MyRectangle _rect2 = null;
        private readonly Line _line;

        public MyLine(int dash)
        {
            _line = new Line
            {
                Fill = Brushes.Black,
                Stroke = Brushes.Black,
                StrokeThickness = 2
            };

            if (dash == 2)
                _line.StrokeDashArray = new DoubleCollection { 2, 2 };

            Id = Guid.NewGuid();
        }

        public override Guid Id
        {
            get { return _id; }
            set
            {
                _id = value;
                _line.Tag = _id;
            }
        }

        public override double X
        {
            get { return _line.X1; }
            set { _line.X1 = value; }
        }

        public override double Y
        {
            get { return _line.Y1; }
            set { _line.Y1 = value; }
        }

        public double X2
        {
            get { return _line.X2; }
            set { _line.X2 = value; }
        }

        public double Y2
        {
            get { return _line.Y2; }
            set { _line.Y2 = value; }
        }

        public Line GetLine()
        {
            return _line;
        }

        public MyRectangle Rect1
        {
            get { return _rect1; }
            set { _rect1 = value; }
        }

        public MyRectangle Rect2
        {
            get { return _rect2; }
            set { _rect2 = value; }
        }

        public override void Draw(int x1, int y1, int x2, int y2)
        {
            X = x1;
            Y = y1;
            X2 = x2;
            Y2 = y2;
        }

    }
}
