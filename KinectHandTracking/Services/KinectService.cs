using System;
using System.Linq;
using System.Collections.Generic;
using KinectHandTracking.Interfaces;
using Microsoft.Kinect;

namespace KinectHandTracking.Services
{
    public class KinectService : IKinectService
    {
        KinectSensor _sensor;
        MultiSourceFrameReader _reader;
        //IList<Body> _bodies;

        //private List<ulong> _trackedBodies = new List<ulong>();
        Dictionary<ulong, TrackedBody> _trackedBodies = new Dictionary<ulong, TrackedBody>();

        public KinectService()
        {

        }

        public void Initialize()
        {
            //throw new Exception("test rafdal");

            _sensor = KinectSensor.GetDefault();

            if (_sensor != null)
            {
                _sensor.Open();

                //_reader = _sensor.OpenMultiSourceFrameReader(FrameSourceTypes.Color | FrameSourceTypes.Depth | FrameSourceTypes.Infrared | FrameSourceTypes.Body);
                _reader = _sensor.OpenMultiSourceFrameReader(FrameSourceTypes.Depth | FrameSourceTypes.Body);
                _reader.MultiSourceFrameArrived += Reader_MultiSourceFrameArrived;
            }
        }

        public void Cleanup()
        {
            if (_reader != null)
            {
                _reader.Dispose();
            }

            if (_sensor != null)
            {
                _sensor.Close();
            }
        }

        private int _counterLimit = 60;

        void Reader_MultiSourceFrameArrived(object sender, MultiSourceFrameArrivedEventArgs e)
        {
            var reference = e.FrameReference.AcquireFrame();

            // Body
            using (var frame = reference.BodyFrameReference.AcquireFrame())
            {
                if (frame != null)
                {
                    var bodies = new Body[frame.BodyFrameSource.BodyCount];

                    frame.GetAndRefreshBodyData(bodies);

                    // Get all tracking ids
                    var trackingIds = bodies.Select(p => p.TrackingId);

                    _trackedBodies
                        .Where(p => !trackingIds.Contains(p.Key))
                        .Select(p => p.Key)
                        .ToList()
                        .ForEach((key) =>
                        {
                            BodyLost(key);

                            _trackedBodies.Remove(key);
                            BodyCountUpdated(_trackedBodies.Count);
                        });


                    foreach (var body in bodies)
                    {
                        if (body != null)
                        {
                            if (body.IsTracked)
                            {

                                TrackedBody trackedBody;

                                if (_trackedBodies.TryGetValue(body.TrackingId, out trackedBody))
                                {


                                }
                                else
                                {
                                    BodyFound(body.TrackingId);

                                    trackedBody = new TrackedBody();

                                    _trackedBodies.Add(body.TrackingId, trackedBody);

                                    BodyCountUpdated(_trackedBodies.Count);
                                }
                                                                
                                BodyTracked(body);
                            }
                        }
                    }
                }
            }
        }

        //void Reader_MultiSourceFrameArrived(object sender, MultiSourceFrameArrivedEventArgs e)
        //{
        //    var reference = e.FrameReference.AcquireFrame();

        //    // Color
        //    //using (var frame = reference.ColorFrameReference.AcquireFrame())
        //    //using (var frame = reference.InfraredFrameReference.AcquireFrame())
        //    //{
        //    //    if (frame != null)
        //    //    {
        //    //        camera.Source = frame.ToBitmap();
        //    //    }
        //    //}

        //    // Body
        //    using (var frame = reference.BodyFrameReference.AcquireFrame())
        //    {
        //        if (frame != null)
        //        {
        //            var bodies = new Body[frame.BodyFrameSource.BodyCount];

        //            frame.GetAndRefreshBodyData(bodies);
                    
        //            // Get all tracking ids
        //            var trackingIds = bodies.Select(p => p.TrackingId);

        //            _trackedBodies
        //                .Where(p => !trackingIds.Contains(p.Key))
        //                .Select(p => p.Key)
        //                .ToList()                        
        //                .ForEach((p) => 
        //                {
        //                    _trackedBodies.Remove(p);
        //                    BodyCountUpdated(_trackedBodies.Count);

        //                });

        //            foreach (var body in bodies)
        //            {
        //                if (body != null)
        //                {
        //                    if (body.IsTracked)
        //                    {
        //                        //if (!_trackedBodies.ContainsKey(body.TrackingId))
        //                        //{
        //                        //    _trackedBodies.Add(body.TrackingId, new TrackedBody());
        //                        //    BodyCountUpdated(_trackedBodies.Count);
        //                        //}

        //                        TrackedBody trackedBody;

        //                        if (_trackedBodies.TryGetValue(body.TrackingId, out trackedBody)) 
        //                        {
 

        //                        }
        //                        else
        //                        {
        //                            trackedBody = new TrackedBody();

        //                            _trackedBodies.Add(body.TrackingId, trackedBody);
        //                            BodyCountUpdated(_trackedBodies.Count);
        //                        }

                                
        //                        // Find the joints
        //                        Joint handRight = body.Joints[JointType.HandRight];
        //                        Joint thumbRight = body.Joints[JointType.ThumbRight];

        //                        Joint handLeft = body.Joints[JointType.HandLeft];
        //                        Joint thumbLeft = body.Joints[JointType.ThumbLeft];

        //                        //trackedBody.RightStateHistory.Add(body.HandRightState);
        //                        //trackedBody.LeftStateHistory.Add(body.HandLeftState);

        //                        // Find the hand states
        //                        //string rightHandState = "-";
        //                        //string leftHandState = "-";

        //                        switch (body.HandRightState)
        //                        {
        //                            case HandState.Open:
        //                                trackedBody.RightHand.OpenCounter++;
        //                                break;
        //                            case HandState.Closed:
        //                                trackedBody.RightHand.ClosedCounter++;
        //                                break;
        //                            //case HandState.Lasso:
        //                            //    break;
        //                            //case HandState.Unknown:
        //                            //    break;
        //                            //case HandState.NotTracked:
        //                            //    break;
        //                            default:
        //                                trackedBody.RightHand.OpenCounter = 0;
        //                                break;
        //                        }


        //                        //switch (body.HandLeftState)
        //                        //{
        //                        //    case HandState.Open:                                        
        //                        //        leftHandState = "Open";
        //                        //        break;
        //                        //    case HandState.Closed:
        //                        //        leftHandState = "Closed";
        //                        //        break;
        //                        //    case HandState.Lasso:
        //                        //        leftHandState = "Lasso";
        //                        //        break;
        //                        //    case HandState.Unknown:
        //                        //        leftHandState = "Unknown...";
        //                        //        break;
        //                        //    case HandState.NotTracked:
        //                        //        leftHandState = "Not tracked";
        //                        //        break;
        //                        //    default:
        //                        //        break;
        //                        //}


        //                        if (trackedBody.RightHand.OpenCounter > _counterLimit || trackedBody.RightHand.ClosedCounter > _counterLimit)
        //                        {
        //                            HandState newState = HandState.Closed;

        //                            if (trackedBody.RightHand.OpenCounter > _counterLimit) 
        //                            {
        //                                newState = HandState.Open;
        //                            }

        //                            HandStateUpdated(new HandStateChange() { PersonId = body.TrackingId, RightState = newState });
        //                            trackedBody.RightHand.OpenCounter = 0;
        //                        }
        //                        //else 
        //                        //{
        //                        //    HandStateUpdated(new HandStateChange() { PersonId = body.TrackingId, RightState = HandState.NotTracked }); 
        //                        //}

        //                        //if ()
        //                        //{
        //                        //    HandStateUpdated(new HandStateChange() { PersonId = body.TrackingId, RightState = HandState.Closed, });
        //                        //    trackedBody.RightHand.ClosedCounter = 0;
        //                        //}


        //                    }
        //                }
        //            }

        //        }
        //    }
        //}

        
        public event Action<int> BodyCountUpdated;

        public event Action<HandStateChange> HandStateUpdated;

        public void Dispose()
        {
            Cleanup();
        }


        public event Action<Body> BodyTracked;


        public event Action<ulong> BodyFound;

        public event Action<ulong> BodyLost;
    }
}
