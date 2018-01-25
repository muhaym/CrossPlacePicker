using Plugin.CrossPlacePicker;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Test
{
	public partial class MainPage : ContentPage
	{
		public MainPage()
		{
			InitializeComponent();
            ButY.Clicked += ButY_ClickedAsync;
		}

        private async void ButY_ClickedAsync(object sender, EventArgs e)
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
    }
}
