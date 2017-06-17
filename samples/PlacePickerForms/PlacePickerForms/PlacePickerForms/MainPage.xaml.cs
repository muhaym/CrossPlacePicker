using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Plugin.CrossPlacePicker;
using Plugin.CrossPlacePicker.Abstractions;
namespace PlacePickerForms
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }

        async private void NoBounds_Clicked(object sender, EventArgs e)
        {
            try
            {
                var result = await CrossPlacePicker.Current.Display();
                if (result != null)
                {
                    await DisplayAlert(result.Name, "Latitude: " + result.Coordinates.Latitude + "\nLongitude: " + result.Coordinates.Longitude, "OK");
                }
            }
            catch (Exception ex)
            {
                await DisplayAlert("Error", ex.ToString(), "Oops");
            }
        }

        async private void WithBounds_Clicked(object sender, EventArgs e)
        {
            try
            {
                var southWest = new Coordinates(85, -180);
                var northEast = new Coordinates(-85, 180);
                var CoordinateBounds = new CoordinateBounds(southWest, northEast);
                var result = await CrossPlacePicker.Current.Display(CoordinateBounds);
                if (result != null)
                {
                    await DisplayAlert(result.Name, "Latitude: " + result.Coordinates.Latitude + "\nLongitude: " + result.Coordinates.Longitude, "OK");
                }
            }
            catch (Exception ex)
            {
                await DisplayAlert("Error", ex.ToString(), "Oops");
            }
        }
    }
}
