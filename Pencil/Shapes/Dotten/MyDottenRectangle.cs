using System;
using System.Collections.Generic;
using System.Windows.Media;
using System.Windows.Shapes;
using Pencil.Shapes.Base;

namespace Pencil.Shapes.Dotten
{
    class MyDottenRectangle : MyBaseRectangle
    {
        public MyDottenRectangle() : base()
        {
            _rect = new Rectangle
            {
                Stroke = Brushes.Black,
                StrokeThickness = 3,
                Fill = new SolidColorBrush(Colors.Orange) {Opacity = 0.9},
                StrokeDashArray = new DoubleCollection {2, 2}
            };
        }
    }
}
