using System;
using System.Windows.Input;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using GalaSoft.MvvmLight.Views;
using KinectHandTracking.Interfaces;

namespace KinectHandTracking.ViewModel
{
    /// <summary>
    /// This class contains properties that the main View can data bind to.
    /// <para>
    /// Use the <strong>mvvminpc</strong> snippet to add bindable properties to this ViewModel.
    /// </para>
    /// <para>
    /// You can also use Blend to data bind with the tool's support.
    /// </para>
    /// <para>
    /// See http://www.galasoft.ch/mvvm
    /// </para>
    /// </summary>
    public class MainViewModel : ViewModelBase
    {
        private IDialogService _dialogService;
        private IKinectService _kinectSvc;
        private ViewMode _mode = ViewMode.Idle;
        private IGestureController _gestureController;

        /// <summary>
        /// Initializes a new instance of the MainViewModel class.
        /// </summary>
        public MainViewModel(IKinectService kinectSvc, IDialogService dialogSvc, IGestureController gestureController)
        {
            ////if (IsInDesignMode)
            ////{
            ////    // Code runs in Blend --> create design time data.
            ////}
            ////else
            ////{
            ////    // Code runs "for real"
            ////}
            _dialogService = dialogSvc;
            _kinectSvc = kinectSvc;
            _gestureController = gestureController;

            gestureController.GestureRecognized += gestureController_GestureRecognized;

            //_kinectSvc.HandStateUpdated += (s) => { HandState = s.RightState.ToString(); };
            //_kinectSvc.BodyCountUpdated += (count) => { BodyCount = count; };
            _kinectSvc.BodyCountUpdated += _kinectSvc_BodyCountUpdated;

            LoadedCommand = new RelayCommand(Initialize);

            GoIdle();

        }

        void gestureController_GestureRecognized(object sender, Gestures.GestureEventArgs e)
        {
            if (_mode != ViewMode.Browsing)
            {
                //switch (e.GestureName)
                //{
                //    case "GreetingRight":
                        
                //        break;
                //    case "GreetingLeft":

                //        break;
                //    case "WaveLeft":

                //        break;
                //    default:
                //        break;
                //}

                GoBrowsing();
            }
        }

        void _kinectSvc_BodyCountUpdated(int bodyCount)
        {
            BodyCount = bodyCount;

            if (bodyCount > 0) 
            {
                //CurrentViewModel = new WebBrowserViewModel(_gestureController);
                _mode = ViewMode.Engaged;
                
                var dialog = new DialogViewModel(){ DialogMessage = string.Format("vink for å starte!", Environment.NewLine) };

                CurrentViewModel = dialog;

                
                //dialog.SetDialogeMessage("Hei!", TimeSpan.FromSeconds(2));
                

            }
            else
            {
                _mode = ViewMode.Idle;
                GoIdle();
            }
        }


        private void GoBrowsing()
        {
            _mode = ViewMode.Browsing;
            CurrentViewModel = new WebBrowserViewModel(_gestureController);
        }

        private void GoIdle() 
        {
            CurrentViewModel = new DialogViewModel() { DialogMessage = "Idle mode!" }; 
        }

        private void Initialize()
        {
            //_dialogService.ShowMessageBox("message", "title");
            // initialize kinect
            try
            {
                _kinectSvc.Initialize();
            }
            catch (Exception ex) 
            {
                _dialogService.ShowError(ex.Message, "Error initializing Kinect Service.", null, null); 
            }


        }

        public ICommand LoadedCommand { get; private set; }

        private ViewModelBase _currentViewModel;
        public ViewModelBase CurrentViewModel
        {
            get { return _currentViewModel; }
            set { Set<ViewModelBase>(() => CurrentViewModel, ref _currentViewModel, value); }
        }

        private string _handdState;
        public string HandState 
        { 
            get { return _handdState; }
            set { Set<string>(() => HandState, ref _handdState, value); }
        }

        private int _bodyCount;
        public int BodyCount
        {
            get { return _bodyCount; }
            set { Set<int>(() => BodyCount, ref _bodyCount, value); }
        }
    }

    public enum ViewMode 
    { 
        Idle = 0,
        Engaged =1,
        Browsing = 2
    }
}