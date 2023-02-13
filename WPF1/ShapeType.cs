using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPF1
{
    public class ShapeType
    {


        public ShapeType(int _idType, string _shapeType) 
        {
            idType= _idType;
            shapeType= _shapeType;
        
        }

        private int idType { get; set; }
        private string shapeType { get; set; }


    }
}
