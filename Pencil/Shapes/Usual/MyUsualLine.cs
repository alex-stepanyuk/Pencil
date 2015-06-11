using System.Windows.Media;
using System.Windows.Shapes;
using Pencil.Shapes.Base;

namespace Pencil.Shapes.Usual
{
    class MyUsualLine : MyBaseLine
    {
        public MyUsualLine() : base()
        {
            _line = new Line
            {
                Fill = Brushes.Black,
                Stroke = Brushes.Black,
                StrokeThickness = 2
            };
        }
    }
}
