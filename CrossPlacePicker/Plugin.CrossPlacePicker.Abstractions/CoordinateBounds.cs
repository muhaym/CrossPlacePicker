namespace Plugin.CrossPlacePicker.Abstractions
{
    public class CoordinateBounds
    {
        public CoordinateBounds(Coordinates southwest, Coordinates northeast)
        {
            this.southwest = southwest;
            this.northeast = northeast;
        }
        public Coordinates southwest { get; set; }
        public Coordinates northeast { get; set; }
    }
}