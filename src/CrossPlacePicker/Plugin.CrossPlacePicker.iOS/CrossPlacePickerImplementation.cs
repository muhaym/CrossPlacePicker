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
    public class CrossPlacePickerImplementation : ICrossPlacePicker
    {
        private int requestId;
        private int? currentRequest;
        private TaskCompletionSource<Places> completionSource;
        private PlacePickerViewController placePickerViewController;


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

        public Task<Places> Display(Abstractions.CoordinateBounds bounds = null)
        {
            UIViewController CurrentController = null;
            UIWindow window = UIApplication.SharedApplication.KeyWindow;
            if (window == null)
                throw new InvalidOperationException("There's no current active window");
            if (window.WindowLevel == UIWindowLevel.Normal)
                CurrentController = window.RootViewController;

            if (CurrentController == null)
            {
                window = UIApplication.SharedApplication.Windows.OrderByDescending(w => w.WindowLevel).FirstOrDefault(w => w.RootViewController != null && w.WindowLevel == UIWindowLevel.Normal);
                if (window == null)
                    throw new InvalidOperationException("Could not find current view controller");
                else
                    CurrentController = window.RootViewController;
            }
            while (CurrentController.PresentedViewController != null)
                CurrentController = CurrentController.PresentedViewController;
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
            PlacePickerController controller = new PlacePickerController(config, currentRequest);
            CurrentController.PresentViewController(controller, true, null);
            EventHandler<PlacePickedEventArgs> handler = null;
            handler = (s, e) =>
            {
                var tcs = Interlocked.Exchange(ref this.completionSource, null);
                PlacePickerController.PlacePicked -= handler;
                CurrentController.DismissViewController(false, null);
                if (e.RequestId != currentRequest)
                    return;
                if (e.IsCanceled)
                    tcs.SetResult(null);
                else if (e.Error != null)
                    tcs.SetException(e.Error);
                else
                    tcs.SetResult(e.Places);
            };
            PlacePickerController.PlacePicked += handler;
            return completionSource.Task;
        }
    }
}