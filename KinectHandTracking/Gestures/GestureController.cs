using System;
using System.Collections.Generic;
using System.Linq;
using KinectHandTracking.Gestures.Greeting;
using KinectHandTracking.Gestures.Wave;
using KinectHandTracking.Interfaces;
using Microsoft.Kinect;

namespace KinectHandTracking.Gestures
{
    public class GestureController : IGestureController
    {
        /// <summary>
        /// The list of all gestures we are currently looking for
        /// </summary>
        private List<Gesture> _gestures = new List<Gesture>();

        /// <summary>
        /// Initializes a new instance of the <see cref="GestureController"/> class.
        /// </summary>
        public GestureController(IKinectService kinectSvc)
        {
            //RegisterGestures();

            kinectSvc.BodyFound += (trackingId) => RegisterGestures(trackingId);
            kinectSvc.BodyLost += (trackingId) => RemoveGestures(trackingId);

            kinectSvc.BodyTracked += (body) => UpdateAllGestures(body);
        }

        private void RegisterGestures(ulong trackingId)
        {
            IRelativeGestureSegment[] waveRightSegments = new IRelativeGestureSegment[6];
            WaveRightSegment1 waveRightSegment1 = new WaveRightSegment1();
            WaveRightSegment2 waveRightSegment2 = new WaveRightSegment2();
            waveRightSegments[0] = waveRightSegment1;
            waveRightSegments[1] = waveRightSegment2;
            waveRightSegments[2] = waveRightSegment1;
            waveRightSegments[3] = waveRightSegment2;
            waveRightSegments[4] = waveRightSegment1;
            waveRightSegments[5] = waveRightSegment2;

            AddGesture(trackingId, "WaveRight", waveRightSegments);

            IRelativeGestureSegment[] waveLeftSegments = new IRelativeGestureSegment[6];
            WaveLeftSegment1 waveLeftSegment1 = new WaveLeftSegment1();
            WaveLeftSegment2 waveLeftSegment2 = new WaveLeftSegment2();
            waveLeftSegments[0] = waveLeftSegment1;
            waveLeftSegments[1] = waveLeftSegment2;
            waveLeftSegments[2] = waveLeftSegment1;
            waveLeftSegments[3] = waveLeftSegment2;
            waveLeftSegments[4] = waveLeftSegment1;
            waveLeftSegments[5] = waveLeftSegment2;

            AddGesture(trackingId, "WaveLeft", waveLeftSegments);

            // Number of frames to hold a static gesture (30 frames pr second)
            var counterLimit = 30;

            IRelativeGestureSegment[] greetingRightSegments = new IRelativeGestureSegment[counterLimit];
            for (int i = 0; i < counterLimit; i++)
            {
                // gesture consists of the same thing 'counterLimit' times 
                greetingRightSegments[i] = new GreetingRightSegment1();
            }
            AddGesture(trackingId, "GreetingRight", greetingRightSegments);

            IRelativeGestureSegment[] greetingLeftSegments = new IRelativeGestureSegment[counterLimit];
            for (int i = 0; i < counterLimit; i++)
            {
                // gesture consists of the same thing 'counterLimit' times 
                greetingLeftSegments[i] = new GreetingLeftSegment1();
            }
            AddGesture(trackingId, "GreetingLeft", greetingLeftSegments);

            IRelativeGestureSegment[] freezeLeftSegments = new IRelativeGestureSegment[counterLimit];
            for (int i = 0; i < counterLimit; i++)
            {
                // gesture consists of the same thing 'counterLimit' times 
                freezeLeftSegments[i] = new FreezeLeftSegment1();
            }
            AddGesture(trackingId, "FreezeLeft", freezeLeftSegments);

            IRelativeGestureSegment[] freezeRightSegments = new IRelativeGestureSegment[counterLimit];
            for (int i = 0; i < counterLimit; i++)
            {
                // gesture consists of the same thing 'counterLimit' times 
                freezeRightSegments[i] = new FreezeRightSegment1();
            }
            AddGesture(trackingId, "FreezeRight", freezeRightSegments);
        }

        /// <summary>
        /// Occurs when [gesture recognised].
        /// </summary>
        public event EventHandler<GestureEventArgs> GestureRecognized;

        /// <summary>
        /// Updates all gestures.
        /// </summary>
        /// <param name="body">The body.</param>
        public void UpdateAllGestures(Body body)
        {
            foreach (Gesture gesture in _gestures.Where(p=> p.TrackingId == body.TrackingId))
            {
                gesture.UpdateGesture(body);
            }
        }

        /// <summary>
        /// Adds the gesture.
        /// </summary>
        /// <param name="name">The gesture type.</param>
        /// <param name="gestureDefinition">The gesture definition.</param>
        public void AddGesture(ulong trackingId, string name, IRelativeGestureSegment[] gestureDefinition)
        {
            Gesture gesture = new Gesture(trackingId, name, gestureDefinition);
            gesture.GestureRecognized += OnGestureRecognized;
            _gestures.Add(gesture);
        }

        public void RemoveGestures(ulong trackingId)
        {
            _gestures.RemoveAll(p => p.TrackingId == trackingId);
        }

        /// <summary>
        /// Handles the GestureRecognized event of the g control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="KinectSkeltonTracker.GestureEventArgs"/> instance containing the event data.</param>
        private void OnGestureRecognized(object sender, GestureEventArgs e)
        {
            if (this.GestureRecognized != null)
            {
                this.GestureRecognized(this, e);
            }

            foreach (Gesture g in _gestures)
            {
                g.Reset();
            }
        }
    }
}
