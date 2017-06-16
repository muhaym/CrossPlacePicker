using Android.App;
using Android.Content;

namespace Plugin.CrossPlacePicker
{
    public interface ActivityEventListner
    {
        void onActivityResult(int requestCode, Result resultCode, Intent data);
    }
}