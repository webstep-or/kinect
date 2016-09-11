using KinectHandTracking.Interfaces;
using Microsoft.Kinect;

namespace KinectHandTracking.Gestures.Greeting
{
    public class FreezeLeftSegment1 : IRelativeGestureSegment
    {
        /// <summary>
        /// Checks the gesture.
        /// </summary>
        /// <param name="skeleton">The skeleton.</param>
        /// <returns>GesturePartResult based on if the gesture part has been completed</returns>
        public GesturePartResult CheckGesture(Body skeleton)
        {
            // hand above elbow
            if (skeleton.Joints[JointType.HandLeft].Position.Y > skeleton.Joints[JointType.ElbowLeft].Position.Y)
            {                
                if (skeleton.HandLeftState == HandState.Closed) 
                {
                    return GesturePartResult.Succeed;
                }

                return GesturePartResult.Pausing;
            }

            return GesturePartResult.Fail;
        }
    }
}
