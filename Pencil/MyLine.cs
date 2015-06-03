using System.ComponentModel;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace Pencil
{
    class MyLine
    {
        private MyRectangle _rect1 = null;
        private MyRectangle _rect2 = null;
        private readonly Line _line;
        private int id;

        public MyLine(Canvas canvas, bool dash)
        {
            _line = new Line
            {
                Fill = Brushes.Black,
                Stroke = Brushes.Black,
                StrokeThickness = 2
            };

            if (dash)
                _line.StrokeDashArray = new DoubleCollection { 2, 2 };

            canvas.Children.Add(_line);
        }

        public int Id
        {
            get { return id; }
            set
            {
                id = value;
                _line.Tag = id;
            }
        }

        public Line GetLine()
        {
            return _line;
        }

        public MyRectangle Rect1
        {
            get{ return _rect1; }
            set
            {
                _rect1 = value;
            }
        }

        public MyRectangle Rect2
        {
            get { return _rect2; }
            set
            {
                _rect2 = value;
            }
        }

    }
}
