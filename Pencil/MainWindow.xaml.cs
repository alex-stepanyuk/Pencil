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
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        double x1, y1, x2, y2, oldX, oldY;
        int side = 0;
        Rectangle rectangle;
        int idInto = -1;
        bool figure = true;
        Arrow arrow;

        public MainWindow()
        {
            InitializeComponent();
            rectangle = new Rectangle();
            Canvas1.Children.Add(rectangle);
        }

        public void isInto(ref int idInto, ref int side)
        {
            double x1 = (Mouse.GetPosition(Canvas1)).X;
            double y1 = (Mouse.GetPosition(Canvas1)).Y;

            for (int i = Canvas1.Children.Count - 1; i >= 0; i--)
            {
                if (Canvas1.Children[i].GetType() == typeof(Rectangle))
                {
                    if ((Canvas.GetLeft(Canvas1.Children[i]) - 3 < x1) &&
                        ((((Rectangle)Canvas1.Children[i]).Width + Canvas.GetLeft(Canvas1.Children[i]) + 3) > x1) &&
                        (Canvas.GetTop(Canvas1.Children[i]) - 3 < y1) &&
                        ((((Rectangle)Canvas1.Children[i]).Height + Canvas.GetTop(Canvas1.Children[i]) + 3) > y1))
                    {
                        idInto = i;
                        if ((x1 >= Canvas.GetLeft(Canvas1.Children[i]) - 3) && (x1 <= Canvas.GetLeft(Canvas1.Children[i]) + 3)) side = 1;
                        else if ((x1 >= ((Rectangle)Canvas1.Children[i]).Width + Canvas.GetLeft(Canvas1.Children[i]) - 3) && (x1 <= ((Rectangle)Canvas1.Children[i]).Width + Canvas.GetLeft(Canvas1.Children[i]) + 3)) side = 3;
                        else if ((y1 >= Canvas.GetTop(Canvas1.Children[i]) - 3) && (y1 <= Canvas.GetTop(Canvas1.Children[i]) + 3)) side = 2;
                        else if ((y1 >= ((Rectangle)Canvas1.Children[i]).Height + Canvas.GetTop(Canvas1.Children[i]) - 3) && (y1 <= ((Rectangle)Canvas1.Children[i]).Height + Canvas.GetTop(Canvas1.Children[i]) + 3)) side = 4;
                        else side = 0;

                        break;
                    }
                }
                idInto = -1;
            }
        }

        private void OnMouseMove(object sender, MouseEventArgs e)
        {
            label1.Content = Mouse.GetPosition(Canvas1).X + " | " + Mouse.GetPosition(Canvas1).Y;

            
            if ((e.LeftButton == MouseButtonState.Pressed))
            {
                x2 = (e.GetPosition(Canvas1)).X;
                y2 = (e.GetPosition(Canvas1)).Y;

                if (idInto == -1)
                {
                    if (x2 > x1) { rectangle.Width = x2 - x1; Canvas.SetLeft(rectangle, x1); }
                    else { rectangle.Width = x1 - x2; Canvas.SetLeft(rectangle, x2); }

                    if (y2 > y1) { rectangle.Height = y2 - y1; Canvas.SetTop(rectangle, y1); }
                    else { rectangle.Height = y1 - y2; Canvas.SetTop(rectangle, y2); }
                }
                else
                {
                    switch (side)
                    {
                        case 0:
                            Canvas.SetLeft(Canvas1.Children[idInto], x2 - oldX);
                            Canvas.SetTop(Canvas1.Children[idInto], y2 - oldY);
                            this.Cursor = Cursors.SizeAll;

                            for (int i = Canvas1.Children.Count - 1; i >= 0; i--)
                            {
                                if (Canvas1.Children[i].GetType() == typeof(Arrow))
                                {
                                    ((Arrow)Canvas1.Children[i]).Move();
                                }
                            }

                        break;
                        case 1: Canvas.SetLeft(Canvas1.Children[idInto], x2); ((Rectangle)Canvas1.Children[idInto]).Width = oldX - Canvas.GetLeft(Canvas1.Children[idInto]); break;
                        case 2: Canvas.SetTop(Canvas1.Children[idInto], y2); ((Rectangle)Canvas1.Children[idInto]).Height = oldY - Canvas.GetTop(Canvas1.Children[idInto]); break;
                        case 3: ((Rectangle)Canvas1.Children[idInto]).Width = x2 - Canvas.GetLeft(Canvas1.Children[idInto]); break;
                        case 4: ((Rectangle)Canvas1.Children[idInto]).Height = y2 - Canvas.GetTop(Canvas1.Children[idInto]); break;
                    }
                }
            }
            else side = 0;
            
            
            if ((side == 1)||(side == 3)) this.Cursor = Cursors.ScrollWE; 
            else if ((side == 2)||(side == 4)) this.Cursor = Cursors.ScrollNS;
        }
            

        private void OnMouseDown(object sender, MouseButtonEventArgs e)
        {
            x1 = (Mouse.GetPosition(Canvas1)).X;
            y1 = (Mouse.GetPosition(Canvas1)).Y;
            
            /*arrow = new Arrow();
            arrow.Start = (Rectangle)Canvas1.Children[i];
            arrow.Finish = (Rectangle)Canvas1.Children[i];
            arrow.Move();
            arrow.Stroke = Brushes.Black;
            arrow.StrokeThickness = 2;
            Canvas1.Children.Add(arrow);*/

            isInto(ref idInto, ref side);

            label2.Content = idInto + " | " + side;

            if (figure)
            {
                if (idInto == -1)
                {
                    rectangle = new Rectangle();
                    rectangle.Stroke = Brushes.Black;
                    rectangle.StrokeThickness = 3;
                    rectangle.Fill = new SolidColorBrush(Colors.Orange);
                    rectangle.Fill.Opacity = 0.9;
                    Canvas1.Children.Add(rectangle);
                }
                else
                {
                    if (side == 0)
                    {
                        oldX = x1 - Canvas.GetLeft(Canvas1.Children[idInto]);
                        oldY = y1 - Canvas.GetTop(Canvas1.Children[idInto]);
                    }
                    else
                    {
                        oldX = ((Rectangle)Canvas1.Children[idInto]).Width + Canvas.GetLeft(Canvas1.Children[idInto]);
                        oldY = ((Rectangle)Canvas1.Children[idInto]).Height + Canvas.GetTop(Canvas1.Children[idInto]);
                    }
                }
            }
            else
            {
                if (side == 4)
                {
                    arrow = new Arrow();
                    arrow.Start = (Rectangle)Canvas1.Children[idInto];
                    arrow.Finish = (Rectangle)Canvas1.Children[idInto]; 
                    arrow.Move();
                    arrow.Stroke = Brushes.Black;
                    arrow.StrokeThickness = 2;
                    Canvas1.Children.Add(arrow);
                }
            }
        }
        

        private void ButtonClean_Click(object sender, RoutedEventArgs e)
        {
            Canvas1.Children.Clear();
            side = 0;
            idInto = -1;
        }

        private void OnMouseUp(object sender, MouseButtonEventArgs e)
        {
            this.Cursor = Cursors.Arrow;
        }

        private void ButtonDel_Click(object sender, RoutedEventArgs e)
        {
            Canvas1.Children.Remove(Canvas1.Children[Canvas1.Children.Count - 1]);
        }

        private void ButtonSelect_Click(object sender, RoutedEventArgs e)
        {
            figure = !figure;
        }

        
    }
}
