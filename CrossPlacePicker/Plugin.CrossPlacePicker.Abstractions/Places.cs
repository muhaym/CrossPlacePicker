using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Plugin.CrossPlacePicker.Abstractions
{
    public class Places
    {
        public Places()
        {
        }

        public Places(Places places)
        {

        }

        /// <summary>
        /// Returns the name of this Place. 
        /// </summary>
        public string Name { get; private set; }
        /// <summary>
        /// Returns the unique id of this Place. 
        /// </summary>
        public string PlaceId { get; private set; }
        /// <summary>
        /// Returns the location of this Place.
        /// </summary>
        public Coordinates Coordinates { get; private set; }
        /// <summary>
        /// Returns the place's phone number in international format. Returns null if no phone number is known, or the place has no phone number. 
        /// </summary>
        public string Phone { get; private set; }
        /// <summary>
        /// Returns a human readable address for this Place. May return null if the address is unknown. 
        /// </summary>
        public string Address { get; private set; }
        /// <summary>
        ///Returns the attributions to be shown to the user if data from the Place is used.
        ///We recommend placing this information below any place information.
        ///The attributions in HTML format, or null if there are no attributions to display.
        ///<seealso cref="">See Displaying Attributions for more details.</seealso>
        ///<returns>The attributions in HTML format, or null if there are no attributions to display.</returns>
        ///</summary>
        public string Attributions { get; private set; }
        /// <summary>
        ///Returns the URI of the website of this Place.Returns null if no website is known.
        ///This is the URI of the website maintained by the Place, if available.Note this is a third-party website not affiliated with the Places API.
        /// </summary>
        public string WebUri { get; private set; }
        /// <summary>
        ///Returns the price level for this place on a scale from 0 (cheapest) to 4.
        ///If no price level is known, a negative value is returned.
        ///The price level of the place, on a scale of 0 to 4. The exact amount indicated by a specific value will vary from region to region.Price levels are interpreted as follows: 
        ///<list type="bullet">
        ///<item>
        ///<description>0 — Free</description>
        ///</item>
        ///<item>
        ///<description>1 — Inexpensive </description>
        ///</item>
        ///<item>
        ///<description>2 — Moderate </description>
        ///</item>
        ///<item>
        ///<description>3 — Expensive </description>
        ///</item>
        ///<item>
        ///<description>4 — Very Expensive </description>
        ///</item>
        ///</list>
        /// </summary>
        public int PriceLevel { get; set; }
        /// <summary>
        ///Returns the place's rating, from 1.0 to 5.0, based on aggregated user reviews.
        ///If no rating is known, a negative value is returned.
        /// </summary>
        public float Rating { get; set; }
        /// <summary>
        ///The recommended viewport for this place.
        /// May be nil if the size of the place is not known.
        ///This returns a viewport of a size that is suitable for displaying this place. For example, a Place object representing a store may have a relatively small viewport, while a Place object representing a country may have a very large viewport.
        /// </summary>
        public CoordinateBounds ViewPort { get; set; }
    }
}
