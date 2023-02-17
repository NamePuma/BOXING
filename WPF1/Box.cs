using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
        List<Shapes> shapes;


        public delegate void Delegate(Box box);
        Delegate OtDel;

        private Canvas CAN;

        SolidColorBrush BaseColor = new SolidColorBrush(Colors.Gray);
        SolidColorBrush FocusColor = new SolidColorBrush(Colors.LimeGreen);
        private Point MouesLastPosition;

        private Point CirclePointLast;

        private Border BorderBorder;
        private Canvas canvas1;
        private Shape SHAP;
        private Ellipse UpCircle;
        private Ellipse DownCircle;
        private const int CircleSize = 20;
        private const double defoultSize = 100;
        private bool Solution = false;
        private bool SolutionForMouse = false;

        #region Свойства для базы 
        

        public double GetWidth
        {
            get { return SHAP.Width; }
            set { SHAP.Width = value; }
            
        }

        public double GetHeight
        {
            get { return SHAP.Height; }
            set { SHAP.Height = value; }

        }

        public Brush GetFill
        {
            get { return SHAP.Fill; }
            set { SHAP.Fill = value; }

        }

        public double GetBorder
        {
            get { return BorderBorder.BorderThickness.Top; }
            set { BorderBorder.BorderThickness = new Thickness(value); }

        }

        public Brush GetBorderColors
        {
            get { return SHAP.Stroke; }
            set
            {
                SHAP.Stroke = value;
            }

        }

        public double GetX
        {
            get { return Canvas.GetTop(BorderBorder); ;
            
            }
            set { Canvas.SetTop(BorderBorder, value);}

        }

        public double GetY
        {
            get { return Canvas.GetLeft(BorderBorder); }
            set { Canvas.SetLeft(BorderBorder, value);}

        }




        #endregion
        public Box(Canvas canvas, Shape rectangle)
        {
            //Колеция для класса
            shapes= new List<Shapes>();

            BorderBorder = new Border();
            canvas1 = new Canvas();
            UpCircle = new Ellipse();
            UpCircle.MouseLeftButtonDown += UpCircle_MouseLeftButtonDown;
            UpCircle.MouseMove += UpCircle_MouseMove;
            UpCircle.MouseUp += UpCircle_MouseUp;
            DownCircle = new Ellipse();
            DownCircle.MouseLeftButtonDown += DownCircle_MouseLeftButtonDown;
            DownCircle.MouseMove += DownCircle_MouseMove;
            DownCircle.MouseUp += DownCircle_MouseUp;
            UpCircle.Width = DownCircle.Width = CircleSize;
            UpCircle.Height = DownCircle.Height = CircleSize;
            UpCircle.Fill = DownCircle.Fill = Brushes.Black;
            BorderBorder.BorderThickness = new Thickness(3);
            
            double PosCircle = CircleSize * 0.5 * -1;
            Canvas.SetTop(UpCircle, PosCircle);
            Canvas.SetLeft(UpCircle, PosCircle);
            UpCircle.Visibility = Visibility.Collapsed;
            DownCircle.Visibility = Visibility.Collapsed;
            Canvas.SetBottom(DownCircle, PosCircle);
            Canvas.SetRight(DownCircle, PosCircle);
            CAN = canvas;
            rectangle.Width = defoultSize - 6;
            rectangle.Height = defoultSize - 6;
            rectangle.Fill = BaseColor;
            double left = (canvas.ActualWidth - rectangle.Width) / 2;

            double top = (canvas.ActualHeight - rectangle.Height) / 2;

            Canvas.SetLeft(BorderBorder, left);
            Canvas.SetTop(BorderBorder, top);
            SHAP = rectangle;
            SHAP.Stroke = FocusColor;
            CreateRectangle(canvas);


            

        }
        public void Link(Delegate oDelegate)
        {
            OtDel = oDelegate;
        }
        private void DownCircle_MouseUp(object sender, MouseButtonEventArgs e)
        {
            SolutionForMouse = false;
            Mouse.Capture(null);

        }

        private void DownCircle_MouseMove(object sender, MouseEventArgs e)
        {
            if (SolutionForMouse == false) { return; }
            Point CirclePointFirst = e.GetPosition(CAN);

            double Right = BorderBorder.Width + CirclePointFirst.X - CirclePointLast.X;
            double Height = BorderBorder.Height + CirclePointFirst.Y - CirclePointLast.Y;
            if (Right <= 10 || Height <= 10) { return; }
            BorderBorder.Width = Right;
            BorderBorder.Height = Height;

            SHAP.Width = Right - 6;
            SHAP.Height = Height - 6;
            CirclePointLast = CirclePointFirst;

        }

        private void DownCircle_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            SolutionForMouse = true;
            CirclePointLast = e.GetPosition(CAN);
            canvas1.Background = Brushes.Gray;
            Mouse.Capture(DownCircle);
        }

        private void UpCircle_MouseUp(object sender, MouseButtonEventArgs e)
        {
            SolutionForMouse = false;
            Mouse.Capture(null);
        }

        private void UpCircle_MouseMove(object sender, MouseEventArgs e)
        {
            if (SolutionForMouse == false) { return; }

            Point CirclePoinFirst = e.GetPosition(CAN);

            double Left = BorderBorder.Width + CirclePointLast.X - CirclePoinFirst.X;
            double Top = BorderBorder.Height + CirclePointLast.Y - CirclePoinFirst.Y;
            Canvas.SetLeft(BorderBorder, CirclePoinFirst.X);
            Canvas.SetTop(BorderBorder, CirclePoinFirst.Y);
            if (Left <= 2 || Top <= 2) { return; }
            BorderBorder.Width = Left;
            SHAP.Width = Left - 6;
            BorderBorder.Height = Top;
            SHAP.Height = Top - 6;
            CirclePointLast = CirclePoinFirst;

        }

        private void UpCircle_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            SolutionForMouse = true;
            CirclePointLast = e.GetPosition(CAN);
            Mouse.Capture(UpCircle);
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

            if (Solution == false) { return; }

            Point MouseFirstPosition = e.GetPosition(CAN);

            double left = Canvas.GetLeft(BorderBorder);
            double top = Canvas.GetTop(BorderBorder);

            Canvas.SetLeft(BorderBorder, left + MouseFirstPosition.X - MouesLastPosition.X);
            Canvas.SetTop(BorderBorder, top + MouseFirstPosition.Y - MouesLastPosition.Y);

            MouesLastPosition = MouseFirstPosition;



        }

        private void SHAP_MouseLeftButtonUp(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {

            Solution = false;
            SHAP.Fill = BaseColor;
            
            Mouse.Capture(null);
        }

        private void SHAP_MouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            MouesLastPosition = e.GetPosition(CAN);
            Solution = true;
            
            canvas1.Background = Brushes.Blue;
            SHAP.Fill = FocusColor;
            
            Mouse.Capture(SHAP);

            if (OtDel != null)
            {
                OtDel(this);

            }
        }
        public void Focus()
        {
            UpCircle.Visibility = Visibility.Visible;
            DownCircle.Visibility = Visibility.Visible;
            BorderBorder.BorderBrush = Brushes.Red;

        }
        public void Blur()
        {
            UpCircle.Visibility = Visibility.Collapsed;
            DownCircle.Visibility = Visibility.Collapsed;
            BorderBorder.BorderBrush = null;
        }



        

    }
}
