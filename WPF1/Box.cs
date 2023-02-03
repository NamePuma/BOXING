using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;

namespace WPF1
{
    public class Box
    {
        private Canvas CAN;

        SolidColorBrush BaseColor = new SolidColorBrush(Colors.Gray);
        SolidColorBrush FocusColor = new SolidColorBrush(Colors.LimeGreen);
        private Point MouesLastPosition;

        private Border BorderBorder;
        private Canvas canvas1;
        private Shape SHAP;
        private Ellipse UpCircle;
        private Ellipse DownCircle;
        private const int CircleSize = 20;
        private const double defoultSize = 100;
        private bool Solution = false;
        public Box(Canvas canvas, Shape rectangle)
        {
            BorderBorder = new Border();
            canvas1 = new Canvas();
            canvas1.Background = Brushes.White;
            UpCircle = new Ellipse();
            DownCircle = new Ellipse();
            UpCircle.Width = DownCircle.Width = CircleSize;
            UpCircle.Height = DownCircle.Height = CircleSize;
            UpCircle.Fill = DownCircle.Fill = Brushes.Black;
            double PosCircle = CircleSize * 0.5 * -1;
            Canvas.SetTop(UpCircle, PosCircle);
            Canvas.SetLeft(UpCircle, PosCircle);
            UpCircle.Visibility = Visibility.Collapsed;
            DownCircle.Visibility = Visibility.Collapsed;
            Canvas.SetBottom(DownCircle, PosCircle);
            Canvas.SetRight(DownCircle, PosCircle);
            CAN = canvas;
            rectangle.Width = defoultSize -2;
            rectangle.Height = defoultSize - 2;
            rectangle.Fill = BaseColor;
            double left = (canvas.ActualWidth - rectangle.Width) / 2;

            double top = (canvas.ActualHeight - rectangle.Height) / 2;
            
            Canvas.SetLeft(BorderBorder, left);
            Canvas.SetTop(BorderBorder, top);
            SHAP = rectangle;
            CreateRectangle(canvas);




        }
        public void CreateRectangle(Canvas canvas)
        {

            
            BorderBorder.Width = defoultSize;
            BorderBorder.Height = defoultSize;
            BorderBorder.Child = canvas1;
            canvas1.Children.Add(SHAP);
            canvas1.Children.Add(UpCircle);
            canvas1.Children.Add(DownCircle);

            canvas.Children.Add(BorderBorder);

            SHAP.MouseLeftButtonDown += SHAP_MouseLeftButtonDown;
            SHAP.MouseLeftButtonUp += SHAP_MouseLeftButtonUp;
            SHAP.MouseMove += SHAP_MouseMove;


        }

        private void SHAP_MouseMove(object sender, System.Windows.Input.MouseEventArgs e)
        {

            if(Solution == false) { return; }

            Point MouseFirstPosition = e.GetPosition(CAN);

           double left =  Canvas.GetLeft(BorderBorder);
            double top = Canvas.GetTop(BorderBorder);
            
            Canvas.SetLeft(BorderBorder, left + MouseFirstPosition.X - MouesLastPosition.X);
            Canvas.SetTop(BorderBorder, top + MouseFirstPosition.Y - MouesLastPosition.Y);

            MouesLastPosition = MouseFirstPosition;



        }

        private void SHAP_MouseLeftButtonUp(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            
            Solution = false;
            SHAP.Fill = BaseColor;
            BorderBorder.BorderBrush = null;
            Mouse.Capture(null);
        }

        private void SHAP_MouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            MouesLastPosition = e.GetPosition(CAN);
            Solution = true;
            BorderBorder.BorderBrush = Brushes.Yellow;
            BorderBorder.BorderThickness = new Thickness(3);
            canvas1.Background = Brushes.Blue;
            SHAP.Fill = FocusColor;
            Mouse.Capture(SHAP);
            BorderBorder.BorderBrush = Brushes.DarkGreen;
            UpCircle.Visibility = Visibility.Visible;
            DownCircle.Visibility = Visibility.Visible;


        }
    }
}
