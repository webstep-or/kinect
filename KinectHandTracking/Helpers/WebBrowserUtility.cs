using System;
using System.Windows;
using System.Windows.Controls;

namespace KinectHandTracking.Helpers
{
    public static class WebBrowserUtility
    {
        public static readonly DependencyProperty BindableSourceProperty =
            DependencyProperty.RegisterAttached("BindableSource", typeof(string), typeof(WebBrowserUtility), new UIPropertyMetadata(null, BindableSourcePropertyChanged));

        public static string GetBindableSource(DependencyObject obj)
        {
            return (string)obj.GetValue(BindableSourceProperty);
        }

        public static void SetBindableSource(DependencyObject obj, string value)
        {
            obj.SetValue(BindableSourceProperty, value);
        }

        public static void BindableSourcePropertyChanged(DependencyObject o, DependencyPropertyChangedEventArgs e)
        {
            WebBrowser browser = o as WebBrowser;
            if (browser != null)
            {
                string uri = e.NewValue as string;
                browser.Source = !String.IsNullOrEmpty(uri) ? new Uri(uri) : null;
            }
        }
    }

    public static class WebBrowserUtility2
    {
        public static readonly DependencyProperty ScrollPositionProperty =
            DependencyProperty.RegisterAttached("ScrollPosition", typeof(int), typeof(WebBrowserUtility2), new UIPropertyMetadata(0, ScrollPositionChanged));

        public static int GetScrollPosition(DependencyObject obj)
        {
            return (int)obj.GetValue(ScrollPositionProperty);
        }

        public static void SetScrollPosition(DependencyObject obj, int value)
        {
            obj.SetValue(ScrollPositionProperty, value);
        }

        public static void ScrollPositionChanged(DependencyObject o, DependencyPropertyChangedEventArgs e)
        {
            WebBrowser browser = o as WebBrowser;
            if (browser != null && e.NewValue != null)
            {
                //var script = "{ if(window.scrollTop() + window.height() == document.height()){ return true;} else{ return false;} }";
                var script = "{return window.pageYOffset;}";
                var bottomCheck = string.Format("'' + (function(){0})()", script);
                var test = int.Parse(browser.InvokeScript("eval", bottomCheck).ToString());

                var oldValue = int.Parse(e.OldValue.ToString());
                var newValue = int.Parse(e.NewValue.ToString());
                
                var scrollPos = newValue;

                if (test >= oldValue)
                {
                    string script2 = string.Format("window.scrollTo(0, {0});", scrollPos);
                    
                    browser.InvokeScript("execScript", new Object[] { script2, "JavaScript" });
                }
                else
                {
                    string script2 = string.Format("window.scrollTo(0, 0);");

                    browser.InvokeScript("execScript", new Object[] { script2, "JavaScript" });
                }
            }
        }
               
    }
}
