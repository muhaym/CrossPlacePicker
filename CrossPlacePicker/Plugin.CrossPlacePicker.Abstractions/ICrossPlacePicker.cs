using System;
using System.Threading.Tasks;

namespace Plugin.CrossPlacePicker.Abstractions
{
    /// <summary>
    /// Interface for CrossPlacePicker
    /// </summary>
    public interface ICrossPlacePicker
    {
        /// <summary>
        /// Display Place Picker UI and Listen for Place Picked Event
        /// </summary>
        Task<Places> Display(CoordinateBounds bounds = null);
    }
}
