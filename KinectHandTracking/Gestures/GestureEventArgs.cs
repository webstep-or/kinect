﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KinectHandTracking.Gestures
{
    /// <summary>
    /// The gesture event arguments
    /// </summary>
    public class GestureEventArgs : EventArgs
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="GestureEventArgs"/> class.
        /// </summary>
        /// <param name="type">The gesture type.</param>
        /// <param name="trackingID">The tracking ID.</param>
        public GestureEventArgs(string name, ulong trackingId)
        {
            this.TrackingId = trackingId;
            this.GestureName = name;
        }

        /// <summary>
        /// Gets or sets the type of the gesture.
        /// </summary>
        /// <value>
        /// The name of the gesture.
        /// </value>
        public string GestureName { get; set; }

        /// <summary>
        /// Gets or sets the tracking ID.
        /// </summary>
        /// <value>
        /// The tracking ID.
        /// </value>
        public ulong TrackingId { get; set; }
    }
}
