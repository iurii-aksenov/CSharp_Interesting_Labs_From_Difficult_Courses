using System;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Animation;

namespace SortVisualisator.Animation
{
    public static class BasicAnimation
    {
        public static ColorAnimation ColorAnimation(Color color, int beginTimeInSeconds = 0)
        {
            var animation = new ColorAnimation();
            //animation.From = ((System.Windows.Media.SolidColorBrush)(element.Background)).Color;
            animation.To = color;
            animation.Duration = TimeSpan.FromSeconds(0);
            animation.FillBehavior = FillBehavior.HoldEnd;
            animation.BeginTime = TimeSpan.FromSeconds(beginTimeInSeconds);
            return animation;
        }

        public static DoubleAnimation MoveVerticalAnimation(double y, double dy, int durationInSeconds = 2, int beginTimeInSeconds = 0)
        {
            var animation = new DoubleAnimation();
            animation.From = y;
            animation.To = animation.From + dy;
            animation.Duration = TimeSpan.FromSeconds(durationInSeconds);
            animation.FillBehavior = FillBehavior.HoldEnd;
            animation.BeginTime = TimeSpan.FromSeconds(beginTimeInSeconds);
            return animation;
        }

        public static DoubleAnimation MoveHorizontalAnimation(double x, double dx, int durationInSeconds = 2, int beginTimeInSeconds = 0)
        {
            var animation = new DoubleAnimation();
            animation.From = x;
            animation.To = animation.From + dx;
            animation.Duration = TimeSpan.FromSeconds(durationInSeconds);
            animation.FillBehavior = FillBehavior.HoldEnd;
            animation.BeginTime = TimeSpan.FromSeconds(beginTimeInSeconds);
            return animation;
        }

        public static DoubleAnimationUsingKeyFrames MoveVerticalAnimationUsingKeyFrames(double y, double dy, int durationInSeconds = 0)
        {
            var animation = new DoubleAnimationUsingKeyFrames();
            var keyFrameStart = new LinearDoubleKeyFrame(y,KeyTime.FromPercent(0));
            var keyFrameFinish = new LinearDoubleKeyFrame(y+dy, KeyTime.FromPercent(100));
            animation.KeyFrames = new DoubleKeyFrameCollection{keyFrameStart,keyFrameFinish};
            animation.Duration= new Duration(TimeSpan.FromSeconds(durationInSeconds));
            return animation;
        }

        public static DoubleAnimationUsingKeyFrames MoveHorizontalAnimationUsingKeyFrames(double x, double dx, int durationInSeconds = 0)
        {
            var animation = new DoubleAnimationUsingKeyFrames();

            var keyFrameStart = new LinearDoubleKeyFrame(x, KeyTime.FromPercent(0));
            var keyFrameFinish = new LinearDoubleKeyFrame(x + dx, KeyTime.FromPercent(100));
            animation.KeyFrames = new DoubleKeyFrameCollection { keyFrameStart, keyFrameFinish };
            animation.Duration = new Duration(TimeSpan.FromSeconds(durationInSeconds));
            return animation;
        }
    }
}
