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
using Npgsql;
using NpgsqlTypes;


namespace WPF1
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private Box selectedId = null;

        private ObservableCollection<Box> box1 = new ObservableCollection<Box>();
        public ObservableCollection<Image> ImagesForListBox { get; set; } = new ObservableCollection<Image>(); 
        
        

       
        

       
        public MainWindow()
        {
            InitializeComponent();

            Connect("10.14.206.28", "5432", "student", "1234", "Oleshkina");
            DataContext= this;
            LoadImage();
        }
        private NpgsqlConnection Connection;
        public void Connect(string host, string stringport, string username, string password, string database) {
            Connection = new NpgsqlConnection(string.Format("Server={0};Port={1};User Id={2};Password={3};Database={4};", host, stringport, username, password, database));
            Connection.Open();

        }
        public void Selected(Box select)
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

        
        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            #region Запрос на добавление имени в Image
            var textsBoxForText = textBoxForNameImage.Text;
            textsBoxForText.Trim();
            NpgsqlCommand npgsqlCommand = Connection.CreateCommand();
            npgsqlCommand.CommandText = "Insert into \"Image\"(name) values(@name)";
            if (textsBoxForText.Length == 0) { MessageBox.Show("Дурачек, заполни текстовое поле"); return; }
            npgsqlCommand.Parameters.AddWithValue("@name", NpgsqlDbType.Varchar, textsBoxForText);

            npgsqlCommand.ExecuteNonQuery();
            #endregion


            npgsqlCommand.CommandText = "Select _idimage from \"Image\" order by _idimage  desc limit 1";
            var numberImage = npgsqlCommand.ExecuteReader();

            numberImage.Read();
            int imageID =  numberImage.GetInt32(0);
            numberImage.Close();

            npgsqlCommand.CommandText = "Select _idshapetype from \"ShapeType\" order by _idshapetype desc limit 1";
            var numberShapeType = npgsqlCommand.ExecuteReader();

            numberShapeType.Read();
            int shapeType = numberShapeType.GetInt32(0);
            numberShapeType.Close();

            
            #region Запрос для фигуры 
            foreach (Box box in box1)
            {
                
                var widht = (int)box.GetWidth;
              
                var heingt = (int)box.GetHeight;
              
                var fill = box.GetFill.ToString();
               
                var border = (int)box.GetBorder;
                
                var bordercolor = box.GetBorderColors.ToString();
               
                double X = (int)box.GetX;
                double Y = (int)box.GetY;

                NpgsqlCommand npgsqlCommandForShape = Connection.CreateCommand();

                npgsqlCommandForShape.CommandText = "Insert into \"Shape\"(widht, heignt, fill, border, bordercolors, pointx, pointy, typeshape, imageshape) values(@widht,@heingt,@fill,@border,@bordercolor,@X,@Y, @typeshape, @imageshape)";
                npgsqlCommandForShape.Parameters.AddWithValue("@widht", NpgsqlDbType.Integer, widht);
                npgsqlCommandForShape.Parameters.AddWithValue("@heingt", NpgsqlDbType.Integer, heingt);
                npgsqlCommandForShape.Parameters.AddWithValue("@fill", NpgsqlDbType.Varchar, fill);
                npgsqlCommandForShape.Parameters.AddWithValue("@border", NpgsqlDbType.Integer, border);
                npgsqlCommandForShape.Parameters.AddWithValue("@bordercolor", NpgsqlDbType.Varchar, bordercolor);
                npgsqlCommandForShape.Parameters.AddWithValue("@X", NpgsqlDbType.Integer, X);
                npgsqlCommandForShape.Parameters.AddWithValue("@Y", NpgsqlDbType.Integer, Y);
                npgsqlCommandForShape.Parameters.AddWithValue("@typeshape", NpgsqlDbType.Integer, shapeType);
                npgsqlCommandForShape.Parameters.AddWithValue("@imageshape", NpgsqlDbType.Integer, imageID);

                int result = npgsqlCommandForShape.ExecuteNonQuery();

                 
                

                
            }

            #endregion
            LoadNameImage(imageID, textsBoxForText);



        }
        private void LoadNameImage(int id, string name)
        {
            Image image = new Image(id, name);
            ImagesForListBox.Add(image);

        }
        private void LoadImage()
        {
            NpgsqlCommand npgsqlCommand = Connection.CreateCommand();
            npgsqlCommand.CommandText = " select _idimage, name from \"Image\"";
           var rezult = npgsqlCommand.ExecuteReader();
            if (rezult.HasRows)
            {
                while(rezult.Read())
                {
                    ImagesForListBox.Add(new Image(rezult.GetInt32(0), rezult.GetString(1)));


                }
            }
            rezult.Close();


        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            Image image = listForImage.SelectedItem as Image;
            double idForImage = image.idImage;
            
            NpgsqlCommand npgsqlCommand = Connection.CreateCommand();
            npgsqlCommand.CommandText = " select * from \"Shape\" where imageshape = @q";
            npgsqlCommand.Parameters.AddWithValue("@q", NpgsqlDbType.Double, idForImage);
            var rezult = npgsqlCommand.ExecuteReader();
                if (rezult.HasRows)
                {

                    while (rezult.Read())
                    {
                        Box box1 = new Box(MyCanvas, new Rectangle())
                        {

                            GetWidth = rezult.GetDouble(1),
                            GetHeight = rezult.GetDouble(2),
                            GetFill = new SolidColorBrush((Color)ColorConverter.ConvertFromString(rezult.GetString(3))),
                            GetBorder = rezult.GetDouble(4),
                            GetBorderColors = new SolidColorBrush((Color)ColorConverter.ConvertFromString(rezult.GetString(5))),
                            GetX = rezult.GetDouble(6),
                            GetY = rezult.GetDouble(7),
                            


                        };
                        
                   
                    }
                } rezult.Close();
            
            
           
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            
            MyCanvas.Children.Clear();
        }
    }
}
