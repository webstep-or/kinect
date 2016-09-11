using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Interactivity;
using System.Windows.Media.Animation;

namespace KinectHandTracking.Helpers
{
    public class TypeWriterBehavior : Behavior<TextBlock>
    {
        protected override void OnAttached()
        {
            AssociatedObject.TargetUpdated += AssociatedObject_TargetUpdated;
                       

        }

        void AssociatedObject_TargetUpdated(object sender, System.Windows.Data.DataTransferEventArgs e)
        {
            Storyboard story = new Storyboard();
            story.FillBehavior = FillBehavior.HoldEnd;
            story.Duration = new Duration(TimeSpan.FromSeconds(AssociatedObject.Text.Length));
            //story.RepeatBehavior = RepeatBehavior.Forever;

            DiscreteStringKeyFrame discreteStringKeyFrame;
            StringAnimationUsingKeyFrames stringAnimationUsingKeyFrames = new StringAnimationUsingKeyFrames();
            //stringAnimationUsingKeyFrames.Duration = new Duration(TimeSpan.FromSeconds(5));

            string tmp = string.Empty;
            foreach (char c in AssociatedObject.Text)
            {
                discreteStringKeyFrame = new DiscreteStringKeyFrame();
                discreteStringKeyFrame.KeyTime = KeyTime.Paced;
                tmp += c;
                discreteStringKeyFrame.Value = tmp;
                stringAnimationUsingKeyFrames.KeyFrames.Add(discreteStringKeyFrame);
            }
            Storyboard.SetTargetName(stringAnimationUsingKeyFrames, AssociatedObject.Name);
            Storyboard.SetTargetProperty(stringAnimationUsingKeyFrames, new PropertyPath(TextBlock.TextProperty));
            story.Children.Add(stringAnimationUsingKeyFrames);

            story.Begin(AssociatedObject);
        }    
    }

    public class TypeWriterBehavior2 : Behavior<TextBox>
    {
        protected override void OnAttached()
        {
            AssociatedObject.TextChanged += AssociatedObject_TextChanged;


        }

        void AssociatedObject_TextChanged(object sender, TextChangedEventArgs e)
        {
            Storyboard story = new Storyboard();
            story.FillBehavior = FillBehavior.HoldEnd;
            story.Duration = new Duration(TimeSpan.FromSeconds(AssociatedObject.Text.Length));
            //story.RepeatBehavior = RepeatBehavior.Forever;

            DiscreteStringKeyFrame discreteStringKeyFrame;
            StringAnimationUsingKeyFrames stringAnimationUsingKeyFrames = new StringAnimationUsingKeyFrames();
            //stringAnimationUsingKeyFrames.Duration = new Duration(TimeSpan.FromSeconds(5));

            string tmp = string.Empty;
            foreach (char c in AssociatedObject.Text)
            {
                discreteStringKeyFrame = new DiscreteStringKeyFrame();
                discreteStringKeyFrame.KeyTime = KeyTime.Paced;
                tmp += c;
                discreteStringKeyFrame.Value = tmp;
                stringAnimationUsingKeyFrames.KeyFrames.Add(discreteStringKeyFrame);
            }
            Storyboard.SetTargetName(stringAnimationUsingKeyFrames, AssociatedObject.Name);
            Storyboard.SetTargetProperty(stringAnimationUsingKeyFrames, new PropertyPath(TextBlock.TextProperty));
            story.Children.Add(stringAnimationUsingKeyFrames);

            story.Begin(AssociatedObject);
        }
        
    }

    public static class TextBlockProperties
    {
        public static readonly DependencyProperty TypeWriterTextProperty =
            DependencyProperty.RegisterAttached("TypeWriterText", typeof(string), typeof(TextBlockProperties), new UIPropertyMetadata(null, TypeWriterTextPropertyChanged));

        public static string GetTypeWriterText(DependencyObject obj)
        {
            return (string)obj.GetValue(TypeWriterTextProperty);
        }

        public static void SetTypeWriterText(DependencyObject obj, string value)
        {
            obj.SetValue(TypeWriterTextProperty, value);
        }

        public static void TypeWriterTextPropertyChanged(DependencyObject o, DependencyPropertyChangedEventArgs e)
        {
            TextBlock txtBlock = o as TextBlock;
            if (txtBlock != null)
            {
                string text = e.NewValue as string;
                //browser.Text = uri;
                //browser.UpdateLayout();
                //browser.Source = !String.IsNullOrEmpty(uri) ? new Uri(uri) : null;

                Storyboard story = new Storyboard();
                story.FillBehavior = FillBehavior.HoldEnd;
                story.Duration = new Duration(TimeSpan.FromSeconds(text.Length));
                //story.RepeatBehavior = RepeatBehavior.Forever;

                DiscreteStringKeyFrame discreteStringKeyFrame;
                StringAnimationUsingKeyFrames stringAnimationUsingKeyFrames = new StringAnimationUsingKeyFrames();
                //stringAnimationUsingKeyFrames.Duration = new Duration(TimeSpan.FromSeconds(5));

                string tmp = string.Empty;
                foreach (char c in text)
                {
                    discreteStringKeyFrame = new DiscreteStringKeyFrame();
                    discreteStringKeyFrame.KeyTime = KeyTime.Paced;
                    tmp += c;
                    discreteStringKeyFrame.Value = tmp;
                    stringAnimationUsingKeyFrames.KeyFrames.Add(discreteStringKeyFrame);
                }

                //string tmp = string.Empty;
                //foreach (var c in text.Split(' '))
                //{
                //    discreteStringKeyFrame = new DiscreteStringKeyFrame();
                //    discreteStringKeyFrame.KeyTime = KeyTime.Paced;
                //    tmp += string.Format("{0} ",c);
                //    discreteStringKeyFrame.Value = tmp;
                //    stringAnimationUsingKeyFrames.KeyFrames.Add(discreteStringKeyFrame);
                //}

                Storyboard.SetTargetName(stringAnimationUsingKeyFrames, txtBlock.Name);
                Storyboard.SetTargetProperty(stringAnimationUsingKeyFrames, new PropertyPath(TextBlock.TextProperty));
                story.Children.Add(stringAnimationUsingKeyFrames);

                story.Begin(txtBlock);
            }
        }
    }
}
