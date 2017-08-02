using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Plugin.CurrentActivity;
using Plugin.CrossPlacePicker.Abstractions;
using Android.Gms.Location.Places.UI;
using Android.Gms.Maps.Model;
using Android.Gms.Common;

namespace Plugin.CrossPlacePicker
{
    [Activity(ConfigurationChanges = Android.Content.PM.ConfigChanges.Orientation | Android.Content.PM.ConfigChanges.ScreenSize)]
    public class PlacePickerActivity : Activity
    {
        internal static event EventHandler<PlacePickedEventArgs> PlacePicked;
        internal const string ExtraId = "ExtraId";
        internal const string ExtraSWLatitude = "ExtraSouthWestLatitude";
        internal const string ExtraSWLongitude = "ExtraSouthWestLongitude";
        internal const string ExtraNELatitude = "ExtraNorthEastLatitude";
        internal const string ExtraNELongitude = "ExtraNorthEastLongitude";

        private double? SWLatitude, SWLongitude, NELatitude, NELongitude;
        private const int REQUEST_PLACE_PICKER = 1;
        private int id;
        void OnPlaceSelected(PlacePickedEventArgs e)
        {
            PlacePicked?.Invoke(this, e);
        }

        public PlacePicker.IntentBuilder intentBuilder;
        public Intent intent;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            var bundle = (savedInstanceState ?? Intent.Extras);
            this.id = bundle.GetInt(ExtraId);
            this.SWLatitude = bundle.GetDouble(ExtraSWLatitude, -9999);
            this.SWLongitude = bundle.GetDouble(ExtraSWLongitude, -9999);
            this.NELatitude = bundle.GetDouble(ExtraNELatitude, -9999);
            this.NELongitude = bundle.GetDouble(ExtraNELongitude, -9999);
            try
            {
                if (SWLatitude != -9999 && SWLongitude != -9999 && NELatitude != -9999 && NELongitude != -9999)
                {
                    LatLng southwest = new LatLng(SWLatitude.Value, SWLongitude.Value);
                    LatLng northeast = new LatLng(NELatitude.Value, NELongitude.Value);
                    var androidBounds = new LatLngBounds(southwest, northeast);
                    intentBuilder = new PlacePicker.IntentBuilder().SetLatLngBounds(androidBounds);
                }
                else
                {
                    intentBuilder = new PlacePicker.IntentBuilder();
                }
                intent = intentBuilder.Build(this);
                StartActivityForResult(intent, REQUEST_PLACE_PICKER);
            }
            catch (GooglePlayServicesRepairableException e)
            {
                OnPlaceSelected(new PlacePickedEventArgs(id, e));
                GoogleApiAvailability.Instance.GetErrorDialog(this, e.ConnectionStatusCode, 0).Show();
                Finish();
            }
            catch (GooglePlayServicesNotAvailableException e)
            {   
                OnPlaceSelected(new PlacePickedEventArgs(id, e));
                Toast.MakeText(this, "Google Play Services is not available.", ToastLength.Long).Show();
                Finish();
            }
            catch (Exception e)
            {
                OnPlaceSelected(new PlacePickedEventArgs(id, e));
                Finish();
            }
        }

        /// <summary>
        /// OnSaved
        /// </summary>
        /// <param name="outState"></param>
        protected override void OnSaveInstanceState(Bundle outState)
        {
            outState.PutInt(ExtraId, this.id);
            if (NELatitude != null)
                outState.PutDouble(ExtraNELatitude, this.NELatitude.Value);
            if (NELongitude != null)
                outState.PutDouble(ExtraNELongitude, this.NELongitude.Value);
            if (SWLatitude != null)
                outState.PutDouble(ExtraSWLatitude, this.SWLatitude.Value);
            if (SWLongitude != null)
                outState.PutDouble(ExtraSWLongitude, this.SWLongitude.Value);
            base.OnSaveInstanceState(outState);
        }



        /// <summary>
        /// OnActivity Result
        /// </summary>
        /// <param name="requestCode"></param>
        /// <param name="resultCode"></param>
        /// <param name="data"></param>
        protected override void OnActivityResult(int requestCode, Result resultCode, Intent data)
        {
            base.OnActivityResult(requestCode, resultCode, data);
            if (requestCode != REQUEST_PLACE_PICKER)
            {
                return;
            }
            if (resultCode == Result.Ok)
            {
                var place = PlacePicker.GetPlace(this, data);
                var name = place.NameFormatted.ToString();
                var placeId = place.Id;
                var coordinate = place.LatLng;
                Coordinates coordinates = null;
                if (place.LatLng?.Latitude != null && place.LatLng?.Longitude != null)
                {
                    coordinates = new Coordinates(coordinate.Latitude, coordinate.Longitude);
                }
                var phone = place.PhoneNumberFormatted?.ToString();
                var address = place.AddressFormatted?.ToString();
                var attribution = place.AttributionsFormatted?.ToString();
                var weburi = place.WebsiteUri?.ToString();
                var priceLevel = place.PriceLevel;
                var rating = place.Rating;
                var swlatitude = place.Viewport?.Southwest.Latitude;
                var swlongitude = place.Viewport?.Southwest.Longitude;
                var nelatitude = place.Viewport?.Northeast.Latitude;
                var nelongitude = place.Viewport?.Northeast.Longitude;
                CoordinateBounds bounds = null;
                if (swlatitude != null && swlongitude != null && nelatitude != null && nelongitude != null)
                {
                    bounds = new CoordinateBounds(new Coordinates(swlatitude.Value, swlongitude.Value), new Coordinates(nelatitude.Value, nelongitude.Value));
                }
                Places places = new Places(name, placeId, coordinates, phone, address, attribution, weburi, priceLevel, rating, bounds);
                OnPlaceSelected(new PlacePickedEventArgs(this.id, false, places));
                Finish();
                return;
            }
            else
            {
                OnPlaceSelected(new PlacePickedEventArgs(this.id, true));
                Finish();
                return;
            }
        }

    }
}