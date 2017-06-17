using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Plugin.CrossPlacePicker.Abstractions
{
    /// <summary>
    /// Event Arguments for Place Picked Event
    /// </summary>
    public class PlacePickedEventArgs
       : EventArgs
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="error"></param>
        public PlacePickedEventArgs(int id, Exception error)
        {
            RequestId = id;
            Error = error ?? throw new ArgumentNullException("error");
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="isCanceled"></param>
        /// <param name="places"></param>
        public PlacePickedEventArgs(int id, bool isCanceled, Places places = null)
        {
            RequestId = id;
            IsCanceled = isCanceled;
            if (!IsCanceled && places == null)
                throw new ArgumentNullException("place");

            Places = places;
        }
        /// <summary>
        /// 
        /// </summary>
        public int RequestId
        {
            get;
            private set;
        }
        /// <summary>
        /// 
        /// </summary>
        public bool IsCanceled
        {
            get;
            private set;
        }
        /// <summary>
        /// 
        /// </summary>
        public Exception Error
        {
            get;
            private set;
        }
        /// <summary>
        /// 
        /// </summary>
        public Places Places
        {
            get;
            private set;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public Task<Places> ToTask()
        {
            var tcs = new TaskCompletionSource<Places>();

            if (IsCanceled)
                tcs.SetResult(null);
            else if (Error != null)
                tcs.SetException(Error);
            else
                tcs.SetResult(Places);

            return tcs.Task;
        }


    }
}
