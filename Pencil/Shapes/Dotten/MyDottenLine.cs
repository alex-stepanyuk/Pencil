using System;
using System.Windows.Media;
using System.Windows.Shapes;
using Pencil.Shapes.Base;

namespace Pencil.Shapes.Dotten
{
    class MyDottenLine : MyBaseLine
    {
        public MyDottenLine(): base()
        {
            _line = new Line
            {
                Fill = Brushes.Black,
                Stroke = Brushes.Black,
                StrokeThickness = 2,
                StrokeDashArray = new DoubleCollection {2, 2}
            };
        }
    }
}
