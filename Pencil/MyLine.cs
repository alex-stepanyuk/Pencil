using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;


namespace Pencil
{
    public class Arrow: Shape
    {
        public double X1 { get; set; }
        public double Y1 { get; set; }
        public double X2 { get; set; }
        public double Y2 { get; set; }
        public Rectangle Start { get; set; }
        public Rectangle Finish { get; set; }

        protected override Geometry DefiningGeometry
        {
            get
            {
                LineGeometry line = new LineGeometry(
                   new Point(X1, Y1),
                      new Point(X2, Y2));
                return line;
            }
        }

        public void Move()
        {
            X1 = Canvas.GetLeft(Start) + Start.Width / 2;
            Y1 = Canvas.GetTop(Start) + Start.Height;
            X2 = Canvas.GetLeft(Finish) + Finish.Width / 2;
            Y2 = Canvas.GetTop(Finish);
        }
    }
}
