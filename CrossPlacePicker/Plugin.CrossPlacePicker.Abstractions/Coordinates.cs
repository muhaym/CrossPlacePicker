using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Plugin.CrossPlacePicker.Abstractions
{
    public class Coordinates
    {
        public Coordinates()
        { 
        
        }
        public Coordinates(Coordinates coordinate)
        {
            Latitude = coordinate.Latitude;
            Longitude = coordinate.Longitude;
        }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
    }
}
