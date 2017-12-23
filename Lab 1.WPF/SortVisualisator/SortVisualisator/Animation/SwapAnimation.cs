using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Animation;

namespace SortVisualisator.Animation
{

    public static class SwapCanvasAnimation
    {
       
        public static TimelineCollection CreateTimelineForSwappedElemnt(UIElement element, double dx,double dy=55, int beginTimeInSeconds = 0)
        {
            var moveVertical = BasicAnimation.MoveVerticalAnimation(Canvas.GetTop(element), dy, 1, beginTimeInSeconds);

            Storyboard.SetTarget(moveVertical, element);
            Storyboard.SetTargetProperty(moveVertical, new PropertyPath(Canvas.TopProperty));

            var moveHorizontal = BasicAnimation.MoveHorizontalAnimation(Canvas.GetLeft(element), dx, 1, beginTimeInSeconds + 1);

            Storyboard.SetTarget(moveHorizontal, element);
            Storyboard.SetTargetProperty(moveHorizontal, new PropertyPath(Canvas.LeftProperty));

            var moveVerticalReverse = BasicAnimation.MoveVerticalAnimation(Canvas.GetTop(element)+dy, -dy, 1, beginTimeInSeconds + 2);

            Storyboard.SetTarget(moveVerticalReverse, element);
            Storyboard.SetTargetProperty(moveVerticalReverse, new PropertyPath(Canvas.TopProperty));

            return new TimelineCollection { moveVertical,moveHorizontal,moveVerticalReverse };
        }



    }

}
