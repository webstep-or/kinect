using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KinectHandTracking.Gestures
{
    /// <summary>
    /// the gesture part result
    /// </summary>
    public enum GesturePartResult
    {
        /// <summary>
        /// Gesture part fail
        /// </summary>
        Fail,

        /// <summary>
        /// Gesture part succeed
        /// </summary>
        Succeed,

        /// <summary>
        /// Gesture part result undetermined
        /// </summary>
        Pausing
    }
}
