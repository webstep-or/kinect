using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KinectHandTracking.Helpers
{
    public class Timer : IBrowserController
    {

        

        public event Action ViewGallery;

        public event Action NavigateLeft;

        public event Action NavigateRight;

        public event Action ScrollDown;

        public event Action ScrollUp;
    }
}
