using System;

namespace Plugin.CrossPlacePicker.Abstractions
{
    /// <summary>
    /// Generation or Consuming Coordinate Bounds
    /// </summary>
    public class CoordinateBounds
    {
        /// <summary>
        /// Empty Constructor
        /// </summary>
        public CoordinateBounds()
        {

        }

        /// <summary>
        /// Setup Coordinate Bounds with SouthWest and NorthEast Coordinates
        /// </summary>
        /// <param name="southwest"></param>
        /// <param name="northeast"></param>
        public CoordinateBounds(Coordinates southwest, Coordinates northeast)
        {
            if (southwest == null)
                throw new ArgumentNullException("southwest", "South West Coordinates can't be null");
            else if (northeast == null)
                throw new ArgumentNullException("northeast", "North East Coordinates can't be null");
            else
            {
                this.Southwest = southwest;
                this.Northeast = northeast;
            }
        }
        /// <summary>
        /// Northeast corner of the bound. 
        /// </summary>
        public Coordinates Southwest { get; set; }
        /// <summary>
        /// Northeast corner of the bound. 
        /// </summary>
        public Coordinates Northeast { get; set; }
    }
}