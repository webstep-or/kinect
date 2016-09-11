using System;
using Microsoft.Kinect;

namespace KinectHandTracking.Interfaces
{
    public interface IKinectService : IDisposable
    {
        void Initialize();
        void Cleanup();
        event Action<HandStateChange> HandStateUpdated;
        event Action<int> BodyCountUpdated;
        event Action<Body> BodyTracked;
        event Action<ulong> BodyFound;
        event Action<ulong> BodyLost;
    }
}
