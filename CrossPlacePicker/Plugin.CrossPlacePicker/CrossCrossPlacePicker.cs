using Plugin.CrossPlacePicker.Abstractions;
using System;

namespace Plugin.CrossPlacePicker
{
  /// <summary>
  /// Cross platform CrossPlacePicker implemenations
  /// </summary>
  public class CrossCrossPlacePicker
  {
    static Lazy<ICrossPlacePicker> Implementation = new Lazy<ICrossPlacePicker>(() => CreateCrossPlacePicker(), System.Threading.LazyThreadSafetyMode.PublicationOnly);

    /// <summary>
    /// Current settings to use
    /// </summary>
    public static ICrossPlacePicker Current
    {
      get
      {
        var ret = Implementation.Value;
        if (ret == null)
        {
          throw NotImplementedInReferenceAssembly();
        }
        return ret;
      }
    }

    static ICrossPlacePicker CreateCrossPlacePicker()
    {
#if PORTABLE
        return null;
#else
        return new CrossPlacePickerImplementation();
#endif
    }

    internal static Exception NotImplementedInReferenceAssembly()
    {
      return new NotImplementedException("This functionality is not implemented in the portable version of this assembly.  You should reference the NuGet package from your main application project in order to reference the platform-specific implementation.");
    }
  }
}
