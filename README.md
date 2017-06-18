## Cross Place Picker for Xamarin Android and iOS

Simple cross platform plugin to pick place using google maps with the help of Cross Platform API.

### Setup
* Available on NuGet: http://www.nuget.org/packages/Fantacode.Plugin.CrossPlacePicker [![NuGet](https://img.shields.io/nuget/v/Fantacode.Plugin.CrossPlacePicker.svg)](https://www.nuget.org/packages/Fantacode.Plugin.CrossPlacePicker)
* Install into your PCL project and Client projects.
* Please see the additional setup for each platforms permissions.



**Platform Support**

|Platform|Supported|Version|
| ------------------- | :-----------: | :------------------: |
|Xamarin.iOS|Yes|iOS 7+|
|Xamarin.Android|Yes|API 15+|


### API Usage

Call **CrossPlacePicker.Current** from any project or PCL to gain access to APIs.

#### Working

Calling PlacePicker Without Bounds
```csharp
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
```

With Bounds

```csharp
try
      {
          var southWest = new Coordinates(85, -180);
          var northEast = new Coordinates(-85, 180);
          var CoordinateBounds = new CoordinateBounds(southWest, northEast);
          var result = await CrossPlacePicker.Current.Display(CoordinateBounds);
      }
      catch (Exception ex)
      {
          await DisplayAlert("Error", ex.ToString(), "Oops");
      }
```

###  Important Permission and Setup Information
Please read these as they must be implemented for all platforms.

#### Android 
The `ACCESS_FINE_LOCATION` permission is required. 

- In your AndroidManifest.xml file, add your API key in a meta-data tag (ensure you are within the `<application>` tag as follows:

```xml
<uses-permission android:name="android.permission.ACCESS_FINE_LOCATION" />
<application
      android:name=".MainApplication"
      ...>
	<meta-data
		android:name="com.google.android.geo.API_KEY"
		android:value="YOUR_ANDROID_API_KEY_HERE"/>
	...
</application>
```

#### iOS
You must request permission to use location services. First add one or both of the following keys to your Info.plist file, to request 'when in use' or 'always' authorization:
`NSLocationWhenInUseUsageDescription`
`NSLocationAlwaysUsageDescription` 
For the place picker, it's enough to request 'when in use' authorization, but you may want to request 'always' authorization for other functionality in your app. For each key, add a string informing the user why you need the location services. For example:
Such as:
```
<key>NSLocationWhenInUseUsageDescription</key>
<string>Show your location on the map</string>
```

If you want the dialogs to be translated you must support the specific languages in your app. Read the [iOS Localization Guide](https://developer.xamarin.com/guides/ios/advanced_topics/localization_and_internationalization/)

- In your AppDelegate.cs file, import the Google Places library by adding `using Google.Maps;` on top of the file.
- Within the `FinishedLaunching` method, instantiate the library as follows:

```csharp
var apikey = "YOUR-API-KEY-HERE";
PlacesClient.ProvideApiKey(apikey);
MapServices.ProvideAPIKey(apikey);
```
#### Troubleshooting

Incase if the place picker is not launching or automatically being hidden, please make sure that you have generated API key from Google Developer Console. If you are facing any other problem, please open an issue with Application Output.
[Google Signup and API Keys](https://developers.google.com/places/android-api/signup)

