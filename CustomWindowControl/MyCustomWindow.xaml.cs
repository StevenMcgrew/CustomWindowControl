using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The User Control item template is documented at https://go.microsoft.com/fwlink/?LinkId=234236

namespace CustomWindowControl
{
    public sealed partial class MyCustomWindow : UserControl
    {
        public MyCustomWindow()
        {
            this.InitializeComponent();
        }


        private void Right_PointerEntered(object sender, PointerRoutedEventArgs e)
        {
            Window.Current.CoreWindow.PointerCursor = new Windows.UI.Core.CoreCursor(Windows.UI.Core.CoreCursorType.SizeWestEast, 1);
            btnForContent.BorderBrush = new SolidColorBrush(Colors.Aqua);
        }

        private void Right_PointerExited(object sender, PointerRoutedEventArgs e)
        {
            Window.Current.CoreWindow.PointerCursor = new Windows.UI.Core.CoreCursor(Windows.UI.Core.CoreCursorType.Arrow, 1);
            btnForContent.BorderBrush = new SolidColorBrush(Colors.Transparent);
        }

        private void btnRemove_Click(object sender, RoutedEventArgs e)
        {
            ((Panel)this.Parent).Children.Remove(this);
        }

        private void Rectangle_ManipulationDelta(object sender, ManipulationDeltaRoutedEventArgs e)
        {
            FrameworkElement prisoner = this;
            FrameworkElement jail = (Panel)this.Parent;

            // Get the top left point of the prisoner in relationship to the jail
            GeneralTransform gt = prisoner.TransformToVisual(jail);
            Point prisonerTopLeftPoint = gt.TransformPoint(new Point(0, 0));

            // Set these variables to represent the edges of the prisoner
            double left = prisonerTopLeftPoint.X + 7;
            double top = prisonerTopLeftPoint.Y + 7;
            double right = left + prisoner.ActualWidth - 14;
            double bottom = top + prisoner.ActualHeight - 14;

            // Combine those edges with the movement value (When these are used in the next step, it keeps the prisoner from getting stuck at the jail boundary)
            double leftAdjust = left + e.Delta.Translation.X;
            double topAdjust = top + e.Delta.Translation.Y;
            double rightAdjust = right + e.Delta.Translation.X;
            double bottomAdjust = bottom + e.Delta.Translation.Y;

            // Allow prisoner movement if within jail boundary (Use two separate "if" statements here, so the movement isn't sticky at the boundary)
            if ((leftAdjust >= 0) && (rightAdjust <= jail.ActualWidth))
            {
                transformUserControl.TranslateX += e.Delta.Translation.X;
            }

            if ((topAdjust >= 0) && (bottomAdjust <= jail.ActualHeight))
            {
                transformUserControl.TranslateY += e.Delta.Translation.Y;
            }
        }

        private void Right_ManipulationDelta(object sender, ManipulationDeltaRoutedEventArgs e)
        {
            FrameworkElement Alice = this;
            FrameworkElement Wonderland = (Panel)this.Parent;

            //Get Alice's top left point inside Wonderland
            GeneralTransform gt = Alice.TransformToVisual(Wonderland);
            Point TopLeftPoint = gt.TransformPoint(new Point(0, 0));

            // Set this variable to represent the right edge of the thing we are resizing
            double right = TopLeftPoint.X + Alice.ActualWidth;

            // Combine the right edge with the movement value.
            double rightAdjust = right + e.Delta.Translation.X;

            // Set this variable to use for restricting the minimum size
            double xadjust = Alice.Width + e.Delta.Translation.X;

            // Restrict adjustment
            if ((rightAdjust <= Wonderland.ActualWidth) && (xadjust >= 30))
            {
                Alice.Width = xadjust;
                //transformUserControl.TranslateX += e.Delta.Translation.X;
            }
        }
    }
}
