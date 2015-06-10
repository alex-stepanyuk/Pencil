using System;
using System.Linq;
using System.Windows;
using System.Windows.Input;

namespace Pencil
{
    public partial class MainWindow
    {

        private readonly MyView _view;
        private MyLine _tempLine;
        private int _dash = 1;
        private bool _figure;

        public MainWindow()
        {
            InitializeComponent();
            _view = new MyView();
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
                    _view.MouseMove(mouseX, mouseY);
                }
                else
                {
                    _tempLine.Draw(_view.OldX, _view.OldY, mouseX, mouseY);
                }
            }

            
        }

        private void OnMouseDown(object sender, MouseButtonEventArgs e)
        {
            int mouseX = (int)Mouse.GetPosition(Canvas1).X;
            int mouseY = (int)Mouse.GetPosition(Canvas1).Y;

            if (_figure)
            {
                _view.MouseDown(mouseX, mouseY, Canvas1, _dash);
            }
            else
            {
                if (_view.IsIntoAny(mouseX, mouseY))
                {
                    _view.OldX = mouseX;
                    _view.OldY = mouseY;
                    _tempLine = new MyLine(_dash);
                    Canvas1.Children.Add(_tempLine.GetLine());
                }
            }

            Label2.Content = (_view.GetId()== Guid.Empty) + " | " + _view.GetSide();
        }

        private void OnMouseUp(object sender, MouseButtonEventArgs e)
        {
            int mouseX = (int)Mouse.GetPosition(Canvas1).X;
            int mouseY = (int)Mouse.GetPosition(Canvas1).Y;

            if (!_figure)
            {
                if (_view.Rectangles.Any(s => s.Id == _view.GetId()))
                {
                    MyRectangle oldRect = _view.Rectangles.First(s => s.Id == _view.GetId());
                    if (_view.IsIntoAny(mouseX, mouseY))
                    {
                        oldRect.AddLine(_tempLine, true);
                        _view.Rectangles.First(s => s.Id == _view.GetId()).AddLine(_tempLine, false);

                        _tempLine.Rect1 = oldRect;
                        _tempLine.Rect2 = _view.Rectangles.First(s => s.Id == _view.GetId());

                        _tempLine.Rect1.UpdateX();
                        _tempLine.Rect1.UpdateY();
                        _tempLine.Rect2.UpdateX();
                        _tempLine.Rect2.UpdateY();

                        _view.AddLine(_tempLine);
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
            if (_view.Rectangles.Any(s => s.Id == _view.GetId()))
            {
                MyRectangle temp = _view.Rectangles.First(s => s.Id == _view.GetId());
                temp.RemoveLines(Canvas1);
                Canvas1.Children.Remove(temp.GetRect());
                _view.Rectangles.Remove(temp);
            }
        }

        private void RadioType_Checked(object sender, RoutedEventArgs e)
        {
            if (Rb1.IsChecked == true) _dash = 1; 
            else if (Rb2.IsChecked == true) _dash = 2;
        }

        private void RadioFigure_Checked(object sender, RoutedEventArgs e)
        {
            _figure = !_figure;
        }

    }
}
