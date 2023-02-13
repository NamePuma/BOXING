using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPF1
{
    public class Image
    {

        private Image (int _idImage, string _name)
        {
            idImage= _idImage;
            name= _name;
            


        }
        private int idImage { get; set; }

        private string name { get; set; }




    }
}
