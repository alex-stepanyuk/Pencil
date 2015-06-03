using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;

namespace Pencil
{
    public partial class MainWindow
    {

        /*
        double _x1, _y1, _x2, _y2, _oldX, _oldY;
        int _side;
        Rectangle _rectangle;
        int _idInto = -1;
        bool _figure = true;

        public MainWindow()
        {
            InitializeComponent();
            _rectangle = new Rectangle();
            Canvas1.Children.Add(_rectangle);
        }

        public void IsInto(ref int idInto, ref int side)
        {
            double x1 = (Mouse.GetPosition(Canvas1)).X;
            double y1 = (Mouse.GetPosition(Canvas1)).Y;

            for (int i = Canvas1.Children.Count - 1; i >= 0; i--)
            {
                if (Canvas1.Children[i] is Rectangle)
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
                _x2 = (e.GetPosition(Canvas1)).X;
                _y2 = (e.GetPosition(Canvas1)).Y;

                if (_idInto == -1)
                {
                    if (_x2 > _x1) { _rectangle.Width = _x2 - _x1; Canvas.SetLeft(_rectangle, _x1); }
                    else { _rectangle.Width = _x1 - _x2; Canvas.SetLeft(_rectangle, _x2); }

                    if (_y2 > _y1) { _rectangle.Height = _y2 - _y1; Canvas.SetTop(_rectangle, _y1); }
                    else { _rectangle.Height = _y1 - _y2; Canvas.SetTop(_rectangle, _y2); }
                }
                else
                {
                    switch (_side)
                    {
                        case 0:
                            Canvas.SetLeft(Canvas1.Children[_idInto], _x2 - _oldX);
                            Canvas.SetTop(Canvas1.Children[_idInto], _y2 - _oldY);
                            Cursor = Cursors.SizeAll;

                        break;
                        case 1: Canvas.SetLeft(Canvas1.Children[_idInto], _x2); ((Rectangle)Canvas1.Children[_idInto]).Width = _oldX - Canvas.GetLeft(Canvas1.Children[_idInto]); break;
                        case 2: Canvas.SetTop(Canvas1.Children[_idInto], _y2); ((Rectangle)Canvas1.Children[_idInto]).Height = _oldY - Canvas.GetTop(Canvas1.Children[_idInto]); break;
                        case 3: ((Rectangle)Canvas1.Children[_idInto]).Width = _x2 - Canvas.GetLeft(Canvas1.Children[_idInto]); break;
                        case 4: ((Rectangle)Canvas1.Children[_idInto]).Height = _y2 - Canvas.GetTop(Canvas1.Children[_idInto]); break;
                    }
                }
            }
            else _side = 0;
            
            
            if ((_side == 1)||(_side == 3)) Cursor = Cursors.ScrollWE; 
            else if ((_side == 2)||(_side == 4)) Cursor = Cursors.ScrollNS;
        }
            

        private void OnMouseDown(object sender, MouseButtonEventArgs e)
        {
            _x1 = (Mouse.GetPosition(Canvas1)).X;
            _y1 = (Mouse.GetPosition(Canvas1)).Y;

            IsInto(ref _idInto, ref _side);

            label2.Content = _idInto + " | " + _side;

            if (_figure)
            {
                if (_idInto == -1)
                {
                    _rectangle = new Rectangle
                    {
                        Stroke = Brushes.Black,
                        StrokeThickness = 3,
                        Fill = new SolidColorBrush(Colors.Orange) {Opacity = 0.9}
                    };
                    Canvas1.Children.Add(_rectangle);
                }
                else
                {
                    if (_side == 0)
                    {
                        _oldX = _x1 - Canvas.GetLeft(Canvas1.Children[_idInto]);
                        _oldY = _y1 - Canvas.GetTop(Canvas1.Children[_idInto]);
                    }
                    else
                    {
                        _oldX = ((Rectangle)Canvas1.Children[_idInto]).Width + Canvas.GetLeft(Canvas1.Children[_idInto]);
                        _oldY = ((Rectangle)Canvas1.Children[_idInto]).Height + Canvas.GetTop(Canvas1.Children[_idInto]);
                    }
                }
            }
        }
        

        private void ButtonClean_Click(object sender, RoutedEventArgs e)
        {
            Canvas1.Children.Clear();
            _side = 0;
            _idInto = -1;
        }

        private void OnMouseUp(object sender, MouseButtonEventArgs e)
        {
            Cursor = Cursors.Arrow;
        }

        private void ButtonDel_Click(object sender, RoutedEventArgs e)
        {
            Canvas1.Children.Remove(Canvas1.Children[Canvas1.Children.Count - 1]);
        }

        private void ButtonSelect_Click(object sender, RoutedEventArgs e)
        {
            _figure = !_figure;
        }
        */

        private MyCanvas _canvas;
        private MyLine _tempLine;
        private bool _dash = true;
        private bool _figure = false;

        public MainWindow()
        {
            InitializeComponent();
            _canvas = new MyCanvas();

            _canvas.AddRect(new MyRectangle(Canvas1, _dash));
            _canvas.Rectangles[0].X = 100;
            _canvas.Rectangles[0].Y = 100;
            _canvas.Rectangles[0].Height = 100;
            _canvas.Rectangles[0].Width = 100;
            
        }

        private void OnMouseMove(object sender, MouseEventArgs e)
        {
            int mouseX = (int) Mouse.GetPosition(Canvas1).X;
            int mouseY = (int) Mouse.GetPosition(Canvas1).Y;

            Label1.Content = mouseX + " | " + mouseY;


            if ((e.LeftButton == MouseButtonState.Pressed))
            {
                if (_figure)
                {
                    if (_canvas.GetId() != Guid.Empty)
                    {
                        if (_canvas.Rectangles.Any(s => s.Id == _canvas.GetId()))
                        {
                            if (_canvas.GetSide() == 0)
                            {
                                _canvas.MoveRect(mouseX, mouseY);
                            }
                            else
                            {
                                _canvas.ResizeRect(mouseX, mouseY);
                            }
                        }
                    }
                    else
                    {
                        _canvas.DrawRect(mouseX, mouseY, _canvas.Rectangles.Last());
                    }
                }
                else
                {
                    _canvas.DrawLine(mouseX, mouseY, _tempLine);
                }
            }

            
        }

        private void OnMouseDown(object sender, MouseButtonEventArgs e)
        {
            int mouseX = (int)Mouse.GetPosition(Canvas1).X;
            int mouseY = (int)Mouse.GetPosition(Canvas1).Y;

            if (_figure)
            {
                if (_canvas.IsInto(mouseX, mouseY))
                {
                    if (_canvas.Rectangles.Any(s => s.Id == _canvas.GetId()))
                    {
                        MyRectangle temp = _canvas.Rectangles.First(s => s.Id == _canvas.GetId());

                        if (_canvas.GetSide() == 0)
                        {
                            _canvas.OldX = mouseX - (int) temp.X;
                            _canvas.OldY = mouseY - (int) temp.Y;
                        }
                        else
                        {
                            _canvas.OldX = (int) (temp.Width + temp.X);
                            _canvas.OldY = (int) (temp.Height + temp.Y);
                        }
                    }
                }
                else
                {
                    _canvas.OldX = mouseX;
                    _canvas.OldY = mouseY;
                    _canvas.AddRect(new MyRectangle(Canvas1, _dash));
                }
            }
            else
            {
                if (_canvas.IsInto(mouseX, mouseY))
                {
                    _canvas.OldX = mouseX;
                    _canvas.OldY = mouseY;
                    _tempLine = new MyLine(Canvas1, _dash);
                }
            }

            Label2.Content = (_canvas.GetId()== Guid.Empty) + " | " + _canvas.GetSide();
        }

        private void OnMouseUp(object sender, MouseButtonEventArgs e)
        {
            int mouseX = (int)Mouse.GetPosition(Canvas1).X;
            int mouseY = (int)Mouse.GetPosition(Canvas1).Y;

            if (!_figure)
            {
                if (_canvas.Rectangles.Any(s => s.Id == _canvas.GetId()))
                {
                    MyRectangle oldRect = _canvas.Rectangles.First(s => s.Id == _canvas.GetId());
                    if (_canvas.IsInto(mouseX, mouseY))
                    {
                        oldRect.AddLine(_tempLine, true);
                        _canvas.Rectangles.First(s => s.Id == _canvas.GetId()).AddLine(_tempLine, false);

                        _tempLine.Rect1 = oldRect;
                        _tempLine.Rect2 = _canvas.Rectangles.First(s => s.Id == _canvas.GetId());

                        _tempLine.Rect1.UpdateProperty("X");
                        _tempLine.Rect1.UpdateProperty("Y");
                        _tempLine.Rect2.UpdateProperty("X");
                        _tempLine.Rect2.UpdateProperty("Y");

                        _canvas.AddLine(_tempLine);
                    }
                    else
                    {
                        Canvas1.Children.Remove(_tempLine.GetLine());
                    }
                }
            }
        }
        
        private void ButtonDel_Click(object sender, RoutedEventArgs e)
        {
            if (_canvas.Rectangles.Any(s => s.Id == _canvas.GetId()))
            {
                Canvas1.Children.Remove(_canvas.Rectangles.First(s => s.Id == _canvas.GetId()).Rect());
                _canvas.Rectangles.Remove(_canvas.Rectangles.First(s => s.Id == _canvas.GetId()));
            }
        }

        private void RadioType_Checked(object sender, RoutedEventArgs e)
        {
            _dash = !_dash;
        }

        private void RadioFigure_Checked(object sender, RoutedEventArgs e)
        {
            _figure = !_figure;
        }

    }
}
