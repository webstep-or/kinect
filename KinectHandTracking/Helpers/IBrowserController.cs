
using System;
namespace KinectHandTracking.Helpers
{
    public interface IBrowserController
    {
        event Action ViewGallery;
        event Action NavigateLeft;
        event Action NavigateRight;
        event Action ScrollDown;
        event Action ScrollUp;
    }
}
