using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GalaSoft.MvvmLight;
using KinectHandTracking.Gestures;
using KinectHandTracking.Interfaces;

namespace KinectHandTracking.ViewModel
{
    public class WebBrowserViewModel : ViewModelBase
    {
        private List<string> _fCodes = new List<string>() { "63602086", "63292212", "64771984", "64574010", "59217524", "64401545", "60090353" };
        private string _baseUrl = "http://m.finn.no/realestate/homes/ad.html?finnkode={0}&searchclickthrough=true";//&orgId=1555773628&sort=1&ref=fas
        private string _galleryBase = "http://m.finn.no/realestate/homes/gallery.html?finnkode={0}";
        private int _current = 0;

        public WebBrowserViewModel(IGestureController gestureController)
        {
            var test = string.Format(_baseUrl, _fCodes[_current]);
            //WebAddress = "http://m.finn.no/";
            WebAddress = test;
            //ScrollPosition = "0";
            
            gestureController.GestureRecognized += gestureController_GestureRecognized;

        }

        void gestureController_GestureRecognized(object sender, GestureEventArgs e)
        {
            switch (e.GestureName)
            {
                case "GreetingRight":
                    NavigateRight();
                    ScrollPosition = 0;
                    break;
                case "GreetingLeft":
                    NavigateLeft();
                    ScrollPosition = 0;
                    break;
                case "FreezeRight":
                    ViewGallery();
                    ScrollPosition = 0;
                    break;
                case "FreezeLeft":
                    //ViewGallery();
                    ScrollPosition += 550;
                    break;
                default:
                    break;
            }            
        }

        private void ViewGallery() 
        {
            WebAddress = string.Format(_galleryBase, _fCodes[_current]);
        }

        private void NavigateRight()
        {
            var cursor = _current + 1;

            if (cursor >= _fCodes.Count)
            {
                _current = 0;

                WebAddress = string.Format(_baseUrl, _fCodes[0]);
            }
            else
            {
                WebAddress = string.Format(_baseUrl, _fCodes[cursor]);

                _current = cursor;
            }
        }

        private void NavigateLeft()
        {

            if (_current > 0)
            {
                var cursor = _current - 1;

                WebAddress = string.Format(_baseUrl, _fCodes[cursor]);

                _current = cursor;
            }
            else
            {
                var cursor = _fCodes.Count - 1;

                WebAddress = string.Format(_baseUrl, _fCodes[cursor]);

                _current = cursor;
            }
        }

        private string _webAddress;
        public string WebAddress
        {
            get { return _webAddress; }
            set { Set<string>(() => WebAddress, ref _webAddress, value); }
        }

        private int _scrollPosition;
        public int ScrollPosition
        {
            get { return _scrollPosition; }
            set { Set<int>(() => ScrollPosition, ref _scrollPosition, value); }
        }
    }
}
