using System;

namespace Plugin.CrossPlacePicker.Abstractions
{
    public class CoordinateBounds
    {
        public CoordinateBounds(Coordinates southwest, Coordinates northeast)
        {
            if (southwest == null)
                throw new ArgumentNullException("southwest", "South West Coordinates can't be null");
            else if (northeast == null)
                throw new ArgumentNullException("northeast", "North East Coordinates can't be null");
            else
            {
                this.southwest = southwest;
                this.northeast = northeast;
            }
        }
        /// <summary>
        /// Northeast corner of the bound. 
        /// </summary>
        public Coordinates southwest { get; set; }
        /// <summary>
        /// Northeast corner of the bound. 
        /// </summary>
        public Coordinates northeast { get; set; }
    }
}