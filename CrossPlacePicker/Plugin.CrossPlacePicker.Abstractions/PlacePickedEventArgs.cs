using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Plugin.CrossPlacePicker.Abstractions
{
    public class PlacePickedEventArgs
       : EventArgs
    {
        public PlacePickedEventArgs(int id, Exception error)
        {
            if (error == null)
                throw new ArgumentNullException("error");

            RequestId = id;
            Error = error;
        }

        public PlacePickedEventArgs(int id, bool isCanceled, Places places = null)
        {
            RequestId = id;
            IsCanceled = isCanceled;
            if (!IsCanceled && places == null)
                throw new ArgumentNullException("place");

            Places = places;
        }

        public int RequestId
        {
            get;
            private set;
        }

        public bool IsCanceled
        {
            get;
            private set;
        }

        public Exception Error
        {
            get;
            private set;
        }

        public Places Places
        {
            get;
            private set;
        }

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
