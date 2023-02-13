using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPF1
{
    public class Shapes
    {
        public Shapes(int _idshape, int _widht, int _height, string _fill, int _border, string _bordercolors, int _pointX, int _pointY, int _typeshape, int _imageshae)
        {
            idShape= _idshape;
            widht= _widht;
            height= _height;
            fill= _fill;
            border= _border;
            bordercolors= _bordercolors;
            pointX= _pointX;
            pointY= _pointY;
            typeshape= _typeshape;
            imageshae= _imageshae;

        }
        private int idShape { get; set; }

        private int widht { get; set;}

        private int height { get; set; }

        private string fill { get; set; }

        private int border { get; set; }

        private string bordercolors { get; set; }

        private int pointX { get; set; }

        private int pointY { get; set; }

        private int typeshape { get; set; }

        private int imageshae { get; set;
        
        }



    }
}
