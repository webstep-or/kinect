using Microsoft.Kinect;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Threading;
using System.Windows.Media.Animation;
using System.Reflection;

namespace KinectHandTracking
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        #region Members
                
        

        #endregion

        #region Constructor

        public MainWindow()
        {
            InitializeComponent();
        }

        #endregion

        #region Event handlers

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {           
                        
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            
        }      


        //public void ScrollDown() 
        //{
        //    string script = "var pos = window.pageYOffset;window.scrollTo(0, pos+100);";
        //    //WebBrowser wb = (WebBrowser)sender;
        //    wbSample.InvokeScript("execScript", new Object[] { script, "JavaScript" }); 
        //}

        #endregion
                

        

        private void SetHandOpacity(int counter) 
        {
            

            //if (counter > 0 && counter < 100)
            //{

            //    //int progress = counter;

            //    //TestBoks.Text = string.Format("{0}", progress);

            //    //var progress = (counter / 100);
            //    //RightHandImage.Opacity = RightHandImage.Opacity + progress;


            //    //TestBoks.Text = counter.ToString();

            //    //if (_rightHandStateOpenCounter > 0)
            //    //{
            //    //    var progress = (_rightHandStateOpenCounter / 60) * .75;

            //    //    RightHandImage.Opacity = 0.25 + ;
            //    //}

            //    DoubleAnimation da = new DoubleAnimation();
            //    da.From = 0.25;
            //    da.To = 0.8;
            //    da.Duration = new Duration(TimeSpan.FromSeconds(1));
            //    ////da.AutoReverse = true;
            //    /////da.RepeatBehavior = RepeatBehavior.Forever;

            //    RightHandImage.BeginAnimation(OpacityProperty, da);
            //}
            //else 
            //{
            //    RightHandImage.Opacity = 0.25;
            //}
        }
        
    }
}
