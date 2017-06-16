using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace CrossPlacePickerFormsSample
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }

        async private void Button_Clicked(object sender, EventArgs e)
        {
            try
            {
                var result = await Plugin.CrossPlacePicker.CrossCrossPlacePicker.Current.Display();
                await DisplayAlert("Success", result.Name, "Haha");
            }
            catch (Exception xe)
            {
               await DisplayAlert("error", xe.ToString(), "Oops");
            }
        }
    }
}
