using Microsoft.Kinect;

namespace KinectHandTracking
{
    public class HandStateChange
    {
        public ulong PersonId { get; set; }
        public int RightFrameCount { get; set; }
        public int LeftFrameCount { get; set; }
        public HandState RightState { get; set; }
        public HandState LeftState { get; set; }
    }
}
