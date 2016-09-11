using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Kinect;

namespace KinectHandTracking
{
    public class TrackedBody
    {
        public TrackedBody()
        {
            //RightStateHistory = new List<HandState>();
            //LeftStateHistory = new List<HandState>();

            RightHand = new TrackedHand();
            LeftHand = new TrackedHand();
        }

        //public List<HandState> LeftStateHistory { get; set; }
        //public List<HandState> RightStateHistory { get; set; }

        public ulong BodyId { get; set; }

        public TrackedHand RightHand { get; set; }
        public TrackedHand LeftHand { get; set; }
    }

    public class TrackedHand
    {
        private int _openCounter = 0;
        public int OpenCounter 
        {
            get { return _openCounter; } 
            set
            {
                _openCounter = value;
                _closedCounter = 0;
            } 
        }

        private int _closedCounter = 0;
        public int ClosedCounter 
        {
            get { return _closedCounter; } 
            set
            {
                _closedCounter = value;
                _openCounter = 0;
            } 
        }
    }
}
