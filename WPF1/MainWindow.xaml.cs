using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

namespace WPF1
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private Box selectedId = null;

        private ObservableCollection<Box> box1 = new ObservableCollection<Box>();
       

       
        

       
        public MainWindow()
        {
            InitializeComponent();
           
        }
        public
         void Selected(Box select)
        {
            foreach (Box i in box1)
            {
                i.Blur();
            }
            select.Focus();

            selectedId = select;


        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Box rectangle = new Box(MyCanvas, new Rectangle());

            

            
            
           rectangle.Link(Selected);
            box1.Add(rectangle);

        }
        
        public void Funki()
        {
            
        }

        private void MyCanvas_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if(e.Source!= null) {
                

            }
        }
    }
}
