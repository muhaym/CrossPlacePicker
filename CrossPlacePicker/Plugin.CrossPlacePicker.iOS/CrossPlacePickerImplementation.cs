using Plugin.CrossPlacePicker.Abstractions;
using System;
using System.Threading.Tasks;

namespace Plugin.CrossPlacePicker
{
    /// <summary>
    /// Implementation for CrossPlacePicker
    /// </summary>
    public class CrossPlacePickerImplementation : ICrossPlacePicker
    {
        public Task<Places> Display(CoordinateBounds bounds = null)
        {
            throw new NotImplementedException();
        }
    }
}