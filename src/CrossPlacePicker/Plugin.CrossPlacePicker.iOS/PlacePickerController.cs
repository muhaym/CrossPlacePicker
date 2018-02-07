using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Foundation;
using Google.Places;
using Google.Places.Picker;
using Plugin.CrossPlacePicker.Abstractions;
using UIKit;

namespace Plugin.CrossPlacePicker
{
    public class PlacePickerController : UIViewController, IPlacePickerViewControllerDelegate
    {
        private PlacePickerConfig config;
        private int? currentRequest;

        public PlacePickerController(PlacePickerConfig config, int? currentRequest)
        {
            this.config = config;
            this.currentRequest = currentRequest;
        }

        internal static event EventHandler<PlacePickedEventArgs> PlacePicked;

        public override void ViewDidLoad()
        {

            base.ViewDidLoad();
            View.BackgroundColor = UIColor.White;
            // Perform any additional setup after loading the view, typically from a nib.
        }

        bool loaded = false;
        public override void ViewWillAppear(bool animated)
        {
            base.ViewWillAppear(animated);
            if (!loaded)
            { 
                loaded = true;
                var placePickerViewController = new PlacePickerViewController(config) { Delegate = this };
                placePickerViewController.ModalPresentationStyle = UIModalPresentationStyle.Popover;
                PresentViewController(placePickerViewController, true, null);
            }
        }
        void OnPlaceSelected(PlacePickedEventArgs e)
        {
            PlacePicked?.Invoke(this, e);
        }

        [Export("placePickerDidCancel:")]
        void DidCancel(PlacePickerViewController viewController)
        {
            DismissViewController(true, null);
            OnPlaceSelected(new PlacePickedEventArgs(currentRequest.Value, true));
        }

        public void DidPickPlace(PlacePickerViewController viewController, Place place)
        {
            if (place != null)
            {
                var name = place.Name;
                var placeId = place.Id;
                var coordinate = place.Coordinate;
                Coordinates coordinates = new Coordinates(coordinate.Latitude, coordinate.Longitude);
                var phone = place.PhoneNumber;
                var address = place.FormattedAddress;
                var attribution = place.Attributions?.ToString();
                var weburi = place.Website?.ToString();
                var priceLevel = (long)place.PriceLevel;
                var rating = place.Rating;
                var swlatitude = place.Viewport?.SouthWest.Latitude;
                var swlongitude = place.Viewport?.SouthWest.Longitude;
                var nelatitude = place.Viewport?.NorthEast.Latitude;
                var nelongitude = place.Viewport?.NorthEast.Longitude;
                Abstractions.CoordinateBounds bounds = null;
                if (swlatitude != null && swlongitude != null && nelatitude != null && nelongitude != null)
                {
                    bounds = new Abstractions.CoordinateBounds(new Coordinates(swlatitude.Value, swlongitude.Value), new Coordinates(nelatitude.Value, nelongitude.Value));
                }
                Places places = new Places(name, placeId, coordinates, phone, address, attribution, weburi, Convert.ToInt32(priceLevel), rating, bounds);
                OnPlaceSelected(new PlacePickedEventArgs(currentRequest.Value, false, places));
            }
            else
            {
               OnPlaceSelected(new PlacePickedEventArgs(currentRequest.Value, true));
            }
           
           DismissViewController(true, null);
           // DismissViewController(false,null);
        }
    }
}