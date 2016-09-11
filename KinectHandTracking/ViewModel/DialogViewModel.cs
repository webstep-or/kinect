using GalaSoft.MvvmLight;

namespace KinectHandTracking.ViewModel
{
    public class DialogViewModel : ViewModelBase
    {
        public DialogViewModel()
        {
            if (IsInDesignMode)
            {
                DialogMessage = "Test info";
            }
        }

        private string _message = string.Empty;

        public string DialogMessage
        {
            get { return _message; }
            set { Set<string>(() => DialogMessage, ref _message, value); }
        }

        
        //private string _typeWriterMessage;
        //public string TypeWriterMessage
        //{

        //    get { return _typeWriterMessage; }
        //    set { Set<string>(() => TypeWriterMessage, ref _typeWriterMessage, value); }

        //}      

        
    }
}
