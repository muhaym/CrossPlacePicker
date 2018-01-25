using Plugin.CrossPlacePicker.Abstractions;
using System;
using System.Threading.Tasks;
using Google.Maps;
using CoreLocation;
using Foundation;
using System.Threading;
using Google.Places.Picker;
using UIKit;
using System.Linq;
using Google.Places;

namespace Plugin.CrossPlacePicker
{
    /// <summary>
    /// Implementation for CrossPlacePicker
    /// </summary>
    [Preserve(AllMembers = true)]
    public class CrossPlacePickerImplementation : ICrossPlacePicker, IPlacePickerViewControllerDelegate
    {
        internal static event EventHandler<PlacePickedEventArgs> PlacePicked;
        private int requestId;
        private int? currentRequest;
        private TaskCompletionSource<Places> completionSource;
        private PlacePickerViewController placePickerViewController;

        public IntPtr Handle => throw new NotImplementedException();

        private int GetRequestId()
        {
            int id = this.requestId;
            if (this.requestId == Int32.MaxValue)
                this.requestId = 0;
            else
                this.requestId++;

            return id;
        }

        /// <summary>
        /// Displays Place Picker UI.
        /// </summary>
        /// <param name="bounds"></param>
        /// <returns></returns>
        /// 
        UIViewController PlaceViewController;
        public Task<Places> Display(Abstractions.CoordinateBounds bounds = null)
        {
            PlaceViewController = null;
            UIWindow window = UIApplication.SharedApplication.KeyWindow;
            if (window == null)
                throw new InvalidOperationException("There's no current active window");
            if (window.WindowLevel == UIWindowLevel.Normal)
                PlaceViewController = window.RootViewController;

            if (PlaceViewController == null)
            {
                window = UIApplication.SharedApplication.Windows.OrderByDescending(w => w.WindowLevel).FirstOrDefault(w => w.RootViewController != null && w.WindowLevel == UIWindowLevel.Normal);
                if (window == null)
                    throw new InvalidOperationException("Could not find current view controller");
                else
                    PlaceViewController = window.RootViewController;
            }
            while (PlaceViewController.PresentedViewController != null)
                PlaceViewController = PlaceViewController.PresentedViewController;
            currentRequest = GetRequestId();
            var ntcs = new TaskCompletionSource<Places>(currentRequest);
            if (Interlocked.CompareExchange(ref this.completionSource, ntcs, null) != null)
                throw new InvalidOperationException("Only one operation can be active at a time");
            Google.Maps.CoordinateBounds iosBound;
            PlacePickerConfig config;
            if (bounds != null)
            {
                var northEast = new CLLocationCoordinate2D(bounds.Northeast.Latitude, bounds.Northeast.Longitude);
                var southwest = new CLLocationCoordinate2D(bounds.Southwest.Latitude, bounds.Southwest.Longitude);
                iosBound = new Google.Maps.CoordinateBounds(northEast, southwest);
                config = new PlacePickerConfig(iosBound);
            }
            else
            {
                config = new PlacePickerConfig(null);
            }
            placePickerViewController = new PlacePickerViewController(config) { Delegate = this };
            PlaceViewController.PresentViewController(placePickerViewController, true, null);

            EventHandler<PlacePickedEventArgs> handler = null;
            handler = (s, e) =>
            {
                var tcs = Interlocked.Exchange(ref this.completionSource, null);
                PlacePicked -= handler;

                if (e.RequestId != currentRequest)
                    return;
                if (e.IsCanceled)
                    tcs.SetResult(null);
                else if (e.Error != null)
                    tcs.SetException(e.Error);
                else
                    tcs.SetResult(e.Places);
            };
            PlacePicked += handler;
            return completionSource.Task;
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
            if (viewController.NavigationController == PlaceViewController?.NavigationController)
                PlaceViewController?.NavigationController.PopViewController(true);
            else
                PlaceViewController?.DismissViewController(true, null);
        }

        public void Dispose()
        {
            placePickerViewController?.Dispose();
        }

        [Export("placePickerDidCancel:")]
        void DidCancel(PlacePickerViewController viewController)
        {
            PlaceViewController?.DismissViewController(true, null);
            OnPlaceSelected(new PlacePickedEventArgs(currentRequest.Value, true));
        }

        void OnPlaceSelected(PlacePickedEventArgs e)
        {
            PlacePicked?.Invoke(this, e);
        }

    }
}