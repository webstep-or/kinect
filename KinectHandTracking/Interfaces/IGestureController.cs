using System;
using KinectHandTracking.Gestures;
using Microsoft.Kinect;
namespace KinectHandTracking.Interfaces
{
    public interface IGestureController
    {
        //void AddGesture(string name, IRelativeGestureSegment[] gestureDefinition);
        event EventHandler<GestureEventArgs> GestureRecognized;
        //void UpdateAllGestures(Body data);
    }
}
