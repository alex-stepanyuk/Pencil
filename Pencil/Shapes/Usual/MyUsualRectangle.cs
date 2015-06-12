using System;
using System.Collections.Generic;
using System.Windows.Media;
using System.Windows.Shapes;
using Pencil.Shapes.Base;
using Pencil.Shapes.Dotten;

namespace Pencil.Shapes.Usual
{
    class MyUsualRectangle : MyBaseRectangle
    {

        public MyUsualRectangle() : base()
        {
            _rect = new Rectangle
            {
                Stroke = Brushes.Black,
                StrokeThickness = 3,
                Fill = new SolidColorBrush(Colors.Orange) { Opacity = 0.9 }
            };

            AllowedLines = new List<Type>{typeof(MyUsualLine), typeof (MyDottenLine)};
        }

        public override void AddLine(MyBaseLine line, bool isStart)
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
    }
}
