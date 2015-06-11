using System.Windows.Media;
using System.Windows.Shapes;
using Pencil.Shapes.Base;

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
        }
    }
}
