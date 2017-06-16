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
        public Coordinates(double Latitude, double Longitude)
        {
            this.Latitude = Latitude;
            this.Longitude = Longitude;
        }
        /// <summary>
        /// Latitude, in degrees. This value is in the range [-90, 90]. 
        /// </summary>
        public double Latitude { get; set; }
        /// <summary>
        /// Longitude, in degrees. This value is in the range [-180, 180]. 
        /// </summary>
        public double Longitude { get; set; }
    }
}
