using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPF1
{
    public class Image
    {

        public Image (int _idImage, string _name)
        {
            idImage= _idImage;
            name= _name;
            


        }
        public int idImage { get; set; }

        public string name { get; set; }




    }
}
