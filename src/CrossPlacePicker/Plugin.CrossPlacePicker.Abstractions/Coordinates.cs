using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Plugin.CrossPlacePicker.Abstractions
{
    /// <summary>
    /// Generate or Consume Coordinates.
    /// </summary>
    public class Coordinates
    {
        /// <summary>
        /// Empty Constructor
        /// </summary>
        public Coordinates()
        { 
        
        }
        /// <summary>
        /// Create Coordinates with Latitude and Longitude of type double.
        /// </summary>
        /// <param name="Latitude"></param>
        /// <param name="Longitude"></param>
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
