using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Windows.Input;
using Windows.Foundation;
using Windows.UI;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Documents;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Shapes;

namespace CustomWindowControl
{
    public sealed class TemplatedWindowControl : ContentControl
    {
        public TemplatedWindowControl()
        {
            this.DefaultStyleKey = typeof(TemplatedWindowControl);
        }

        CompositeTransform transformWindow;
        Button _closeButton;
        Grid _gridRoot;
        Grid _gridTitleBar;
        ContentPresenter _contentPresenter;
        Rectangle _rectTitleBar;
        Rectangle _rectRight;
        Rectangle _rectTopLeft;
        Rectangle _rectTop;
        Rectangle _rectTopRight;
        Rectangle _rectLeft;
        Rectangle _rectBottomLeft;
        Rectangle _rectBottom;
        Rectangle _rectBottomRight;


        protected override void OnApplyTemplate()
        {
            _closeButton = GeneralizedGetTemplateChild<Button>("btnClose");
            _gridRoot = GeneralizedGetTemplateChild<Grid>("gridRoot");
            _gridTitleBar = GeneralizedGetTemplateChild<Grid>("gridTitleBar");
            _contentPresenter = GeneralizedGetTemplateChild<ContentPresenter>("ContentPresenter");
            _rectTitleBar = GeneralizedGetTemplateChild<Rectangle>("rectTitleBar");
            _rectRight = GeneralizedGetTemplateChild<Rectangle>("Right");
            _rectTopLeft = GeneralizedGetTemplateChild<Rectangle>("TopLeft");
            _rectTop = GeneralizedGetTemplateChild<Rectangle>("Top");
            _rectTopRight = GeneralizedGetTemplateChild<Rectangle>("TopRight");
            _rectLeft = GeneralizedGetTemplateChild<Rectangle>("Left");
            _rectBottomLeft = GeneralizedGetTemplateChild<Rectangle>("BottomLeft");
            _rectBottom = GeneralizedGetTemplateChild<Rectangle>("Bottom");
            _rectBottomRight = GeneralizedGetTemplateChild<Rectangle>("BottomRight");

            this.ManipulationMode = ManipulationModes.TranslateX | ManipulationModes.TranslateY;
            _rectTitleBar.ManipulationMode = ManipulationModes.TranslateX | ManipulationModes.TranslateY;
            _gridRoot.ManipulationMode = ManipulationModes.TranslateX | ManipulationModes.TranslateY;
            _rectRight.ManipulationMode = ManipulationModes.TranslateX;
            _rectTopLeft.ManipulationMode = ManipulationModes.TranslateX | ManipulationModes.TranslateY;
            _rectTop.ManipulationMode = ManipulationModes.TranslateY;
            _rectTopRight.ManipulationMode = ManipulationModes.TranslateX | ManipulationModes.TranslateY;
            _rectLeft.ManipulationMode = ManipulationModes.TranslateX;
            _rectBottomLeft.ManipulationMode = ManipulationModes.TranslateX | ManipulationModes.TranslateY;
            _rectBottom.ManipulationMode = ManipulationModes.TranslateY;
            _rectBottomRight.ManipulationMode = ManipulationModes.TranslateX | ManipulationModes.TranslateY;
            
            _rectTitleBar.ManipulationDelta += _rectTitleBar_ManipulationDelta;
            _closeButton.Click += _closeButton_Click;
            _gridRoot.PointerEntered += _gridRoot_PointerEntered;
            _gridRoot.PointerExited += _gridRoot_PointerExited;

            _rectRight.ManipulationDelta += _rectRight_ManipulationDelta;
            _rectRight.PointerEntered += _PointerEntered;
            _rectRight.PointerExited += _PointerExited;

            _rectTopLeft.ManipulationDelta += _rectTopLeft_ManipulationDelta;
            _rectTopLeft.PointerEntered += _PointerEntered;
            _rectTopLeft.PointerExited += _PointerExited;

            _rectTop.ManipulationDelta += _rectTop_ManipulationDelta;
            _rectTop.PointerEntered += _PointerEntered;
            _rectTop.PointerExited += _PointerExited;

            _rectTopRight.ManipulationDelta += _rectTopRight_ManipulationDelta;
            _rectTopRight.PointerEntered += _PointerEntered;
            _rectTopRight.PointerExited += _PointerExited;

            _rectLeft.ManipulationDelta += _rectLeft_ManipulationDelta;
            _rectLeft.PointerEntered += _PointerEntered;
            _rectLeft.PointerExited += _PointerExited;

            _rectBottomLeft.ManipulationDelta += _rectBottomLeft_ManipulationDelta;
            _rectBottomLeft.PointerEntered += _PointerEntered;
            _rectBottomLeft.PointerExited += _PointerExited;

            _rectBottom.ManipulationDelta += _rectBottom_ManipulationDelta;
            _rectBottom.PointerEntered += _PointerEntered;
            _rectBottom.PointerExited += _PointerExited;

            _rectBottomRight.ManipulationDelta += _rectBottomRight_ManipulationDelta;
            _rectBottomRight.PointerEntered += _PointerEntered;
            _rectBottomRight.PointerExited += _PointerExited;

            transformWindow = new CompositeTransform();
            this.RenderTransform = transformWindow;
        }

        private void _closeButton_Click(object sender, RoutedEventArgs e)
        {
            ((Panel)this.Parent).Children.Remove(this);
        }

        private void _gridRoot_PointerEntered(object sender, PointerRoutedEventArgs e)
        {
            _contentPresenter.BorderBrush = GetColorFromHex("#B2007ACC");
            _gridTitleBar.Visibility = Visibility.Visible;
        }

        private void _gridRoot_PointerExited(object sender, PointerRoutedEventArgs e)
        {
            _contentPresenter.BorderBrush = new SolidColorBrush(Colors.Transparent);
            _gridTitleBar.Visibility = Visibility.Visible;
        }

        private void _PointerEntered(object sender, PointerRoutedEventArgs e)
        {
            string name = (sender as Rectangle).Name;

            switch (name)
            {
                case "BottomRight":
                    Window.Current.CoreWindow.PointerCursor = new Windows.UI.Core.CoreCursor(Windows.UI.Core.CoreCursorType.SizeNorthwestSoutheast, 1);
                    break;
                case "Right":
                    Window.Current.CoreWindow.PointerCursor = new Windows.UI.Core.CoreCursor(Windows.UI.Core.CoreCursorType.SizeWestEast, 1);
                    break;
                case "Bottom":
                    Window.Current.CoreWindow.PointerCursor = new Windows.UI.Core.CoreCursor(Windows.UI.Core.CoreCursorType.SizeNorthSouth, 1);
                    break;
                case "Top":
                    Window.Current.CoreWindow.PointerCursor = new Windows.UI.Core.CoreCursor(Windows.UI.Core.CoreCursorType.SizeNorthSouth, 1);
                    break;
                case "Left":
                    Window.Current.CoreWindow.PointerCursor = new Windows.UI.Core.CoreCursor(Windows.UI.Core.CoreCursorType.SizeWestEast, 1);
                    break;
                case "TopLeft":
                    Window.Current.CoreWindow.PointerCursor = new Windows.UI.Core.CoreCursor(Windows.UI.Core.CoreCursorType.SizeNorthwestSoutheast, 1);
                    break;
                case "TopRight":
                    Window.Current.CoreWindow.PointerCursor = new Windows.UI.Core.CoreCursor(Windows.UI.Core.CoreCursorType.SizeNortheastSouthwest, 1);
                    break;
                case "BottomLeft":
                    Window.Current.CoreWindow.PointerCursor = new Windows.UI.Core.CoreCursor(Windows.UI.Core.CoreCursorType.SizeNortheastSouthwest, 1);
                    break;
            }
        }

        private void _PointerExited(object sender, PointerRoutedEventArgs e)
        {
            Window.Current.CoreWindow.PointerCursor = new Windows.UI.Core.CoreCursor(Windows.UI.Core.CoreCursorType.Arrow, 1);
        }

        #region ManipulationDelta handlers

        private void _rectTitleBar_ManipulationDelta(object sender, ManipulationDeltaRoutedEventArgs e)
        {
            FrameworkElement prisoner = this;
            FrameworkElement jail = (Panel)this.Parent;

            // Get the top left point of the prisoner in relationship to the jail
            GeneralTransform gt = prisoner.TransformToVisual(jail);
            Point prisonerTopLeftPoint = gt.TransformPoint(new Point(0, 0));

            // Set these variables to represent the edges of the prisoner
            double left = prisonerTopLeftPoint.X + 9;
            double top = prisonerTopLeftPoint.Y + 9;
            double right = left + prisoner.ActualWidth - 18;
            double bottom = top + prisoner.ActualHeight - 18;

            // Combine those edges with the movement value (When these are used in the next step, it keeps the prisoner from getting stuck at the jail boundary)
            double leftAdjust = left + e.Delta.Translation.X;
            double topAdjust = top + e.Delta.Translation.Y;
            double rightAdjust = right + e.Delta.Translation.X;
            double bottomAdjust = bottom + e.Delta.Translation.Y;

            // Allow prisoner movement if within jail boundary (Use two separate "if" statements here, so the movement isn't sticky at the boundary)
            if ((leftAdjust >= 0) && (rightAdjust <= jail.ActualWidth))
            {
                transformWindow.TranslateX += e.Delta.Translation.X;
            }

            if ((topAdjust >= 0) && (bottomAdjust <= jail.ActualHeight))
            {
                transformWindow.TranslateY += e.Delta.Translation.Y;
            }
        }

        private void _rectBottomRight_ManipulationDelta(object sender, ManipulationDeltaRoutedEventArgs e)
        {
            FrameworkElement Alice = this;
            FrameworkElement Wonderland = (Panel)this.Parent;

            // Get Alice's top left point in relation to Wonderland.
            GeneralTransform gt = Alice.TransformToVisual(Wonderland);
            Point TopLeftPoint = gt.TransformPoint(new Point(0, 0));

            // Set these variables to represent the edges of Alice that will be adjusted.
            double right = TopLeftPoint.X + Alice.ActualWidth;
            double bottom = TopLeftPoint.Y + Alice.ActualHeight;

            // Combine the adjustable edges with the movement value.
            double rightAdjust = right + e.Delta.Translation.X;
            double bottomAdjust = bottom + e.Delta.Translation.Y;

            // Set these to use for restricting the minimum size
            double yadjust = Alice.ActualHeight + e.Delta.Translation.Y;
            double xadjust = Alice.ActualWidth + e.Delta.Translation.X;

            // Restrict adjustments
            if ((rightAdjust <= Wonderland.ActualWidth + 8) && (xadjust >= 100))
            {
                Alice.Width = xadjust;
            }

            if ((bottomAdjust <= Wonderland.ActualHeight + 8) && (yadjust >= 100))
            {
                Alice.Height = yadjust;
            }
        }

        private void _rectBottom_ManipulationDelta(object sender, ManipulationDeltaRoutedEventArgs e)
        {
            FrameworkElement myCustomWindow = this;
            FrameworkElement panel = (Panel)this.Parent;

            //Get myCustomWindow's top left point inside panel
            GeneralTransform gt = myCustomWindow.TransformToVisual(panel);
            Point TopLeftPoint = gt.TransformPoint(new Point(0, 0));

            // Set this variable to represent the bottom edge of myCustomWindow
            double bottom = TopLeftPoint.Y + myCustomWindow.ActualHeight;

            // Combine the bottom edge with the movement value.
            double bottomAdjust = bottom + e.Delta.Translation.Y;

            // Set this variable to use for restricting the minimum size
            double yadjust = myCustomWindow.ActualHeight + e.Delta.Translation.Y;

            // Restrict adjustment
            if ((bottomAdjust <= panel.ActualHeight + 8) && (yadjust >= 100))
            {
                myCustomWindow.Height = yadjust;
            }
        }

        private void _rectBottomLeft_ManipulationDelta(object sender, ManipulationDeltaRoutedEventArgs e)
        {
            FrameworkElement Alice = this;
            FrameworkElement Wonderland = (Panel)this.Parent;

            // Get Alice's top left point in relation to Wonderland.
            GeneralTransform gt = Alice.TransformToVisual(Wonderland);
            Point TopLeftPoint = gt.TransformPoint(new Point(0, 0));

            // Set these variables to represent the edges of Alice that will be adjusted.
            double left = TopLeftPoint.X;
            double bottom = TopLeftPoint.Y + Alice.ActualHeight;

            // Combine the adjustable edges with the movement value.
            double leftAdjust = left + e.Delta.Translation.X;
            double bottomAdjust = bottom + e.Delta.Translation.Y;

            // Set these to use for restricting the minimum size
            double yadjust = Alice.ActualHeight + e.Delta.Translation.Y;
            double xadjust = Alice.ActualWidth - e.Delta.Translation.X;

            // Restrict adjustments
            if ((leftAdjust >= -8) && (xadjust >= 100))
            {
                transformWindow.TranslateX += e.Delta.Translation.X;
                Alice.Width = xadjust;
            }

            if ((bottomAdjust <= Wonderland.ActualHeight + 8) && (yadjust >= 100))
            {
                Alice.Height = yadjust;
            }
        }

        private void _rectLeft_ManipulationDelta(object sender, ManipulationDeltaRoutedEventArgs e)
        {
            FrameworkElement myCustomWindow = this;
            FrameworkElement panel = (Panel)this.Parent;

            //Get myCustomWindow's top left point inside panel
            GeneralTransform gt = myCustomWindow.TransformToVisual(panel);
            Point TopLeftPoint = gt.TransformPoint(new Point(0, 0));

            // Set this variable to represent the left edge of myCustomWindow
            double left = TopLeftPoint.X;

            // Combine the left edge with the movement value.
            double leftAdjust = left + e.Delta.Translation.X;

            // Set this variable to use for restricting the minimum size
            double xadjust = myCustomWindow.ActualWidth - e.Delta.Translation.X;

            // Restrict adjustment
            if ((leftAdjust >= -8) && (xadjust >= 100))
            {
                transformWindow.TranslateX += e.Delta.Translation.X;
                myCustomWindow.Width = myCustomWindow.ActualWidth - e.Delta.Translation.X;
            }
        }

        private void _rectTopRight_ManipulationDelta(object sender, ManipulationDeltaRoutedEventArgs e)
        {
            FrameworkElement Alice = this;
            FrameworkElement Wonderland = (Panel)this.Parent;

            // Get Alice's top left point in relation to Wonderland.
            GeneralTransform gt = Alice.TransformToVisual(Wonderland);
            Point TopLeftPoint = gt.TransformPoint(new Point(0, 0));

            // Set these variables to represent the edges of Alice that will be adjusted.
            double right = TopLeftPoint.X + Alice.ActualWidth;
            double top = TopLeftPoint.Y;

            // Combine the adjustable edges with the movement value.
            double rightAdjust = right + e.Delta.Translation.X;
            double topAdjust = top + e.Delta.Translation.Y;

            // Set these to use for restricting the minimum size
            double yadjust = Alice.ActualHeight - e.Delta.Translation.Y;
            double xadjust = Alice.ActualWidth + e.Delta.Translation.X;

            // Restrict adjustments
            if ((rightAdjust <= Wonderland.ActualWidth + 8) && (xadjust >= 100))
            {
                Alice.Width = xadjust;
            }

            if ((topAdjust >= -8) && (yadjust >= 100))
            {
                transformWindow.TranslateY += e.Delta.Translation.Y;
                Alice.Height = yadjust;
            }
        }

        private void _rectTop_ManipulationDelta(object sender, ManipulationDeltaRoutedEventArgs e)
        {
            FrameworkElement myCustomWindow = this;
            FrameworkElement panel = (Panel)this.Parent;

            //Get myCustomWindow's top left point inside panel
            GeneralTransform gt = myCustomWindow.TransformToVisual(panel);
            Point TopLeftPoint = gt.TransformPoint(new Point(0, 0));

            // Set this variable to represent the top edge of myCustomWindow
            double top = TopLeftPoint.Y;

            // Combine the top edge with the movement value.
            double topAdjust = top + e.Delta.Translation.Y;

            // Set this variable to use for restricting the minimum size
            double yadjust = myCustomWindow.ActualHeight - e.Delta.Translation.Y;

            // Restrict adjustment
            if ((topAdjust >= -8) && (yadjust >= 100))
            {
                transformWindow.TranslateY += e.Delta.Translation.Y;
                myCustomWindow.Height = myCustomWindow.ActualHeight - e.Delta.Translation.Y;
            }
        }

        private void _rectTopLeft_ManipulationDelta(object sender, ManipulationDeltaRoutedEventArgs e)
        {
            FrameworkElement Alice = this;
            FrameworkElement Wonderland = (Panel)this.Parent;

            // Get Alice's top left point in relation to Wonderland.
            GeneralTransform gt = Alice.TransformToVisual(Wonderland);
            Point TopLeftPoint = gt.TransformPoint(new Point(0, 0));

            // Set these variables to represent the edges of Alice that will be adjusted.
            double left = TopLeftPoint.X;
            double top = TopLeftPoint.Y;

            // Combine the adjustable edges with the movement value.
            double leftAdjust = left + e.Delta.Translation.X;
            double topAdjust = top + e.Delta.Translation.Y;

            // Set these to use for restricting the minimum size
            double yadjust = Alice.ActualHeight - e.Delta.Translation.Y;
            double xadjust = Alice.ActualWidth - e.Delta.Translation.X;

            // Restrict adjustments
            if ((leftAdjust >= -8) && (xadjust >= 100))
            {
                transformWindow.TranslateX += e.Delta.Translation.X;
                Alice.Width = xadjust;
            }

            if ((topAdjust >= -8) && (yadjust >= 100))
            {
                transformWindow.TranslateY += e.Delta.Translation.Y;
                Alice.Height = yadjust;
            }
        }

        private void _rectRight_ManipulationDelta(object sender, ManipulationDeltaRoutedEventArgs e)
        {
            FrameworkElement myCustomWindow = this;
            FrameworkElement panel = (Panel)this.Parent;

            //Get myCustomWindow's top left point inside panel
            GeneralTransform gt = myCustomWindow.TransformToVisual(panel);
            Point TopLeftPoint = gt.TransformPoint(new Point(0, 0));

            // Set this variable to represent the right edge of myCustomWindow
            double right = TopLeftPoint.X + myCustomWindow.ActualWidth;

            // Combine the right edge with the movement value.
            double rightAdjust = right + e.Delta.Translation.X;

            // Set this variable to use for restricting the minimum size
            double xadjust = myCustomWindow.ActualWidth + e.Delta.Translation.X;

            // Restrict adjustment
            if ((rightAdjust <= panel.ActualWidth + 8) && (xadjust >= 100))
            {
                myCustomWindow.Width = xadjust;
            }
        }

        #endregion

        childElement GeneralizedGetTemplateChild<childElement>(string name) where childElement : DependencyObject
        {
            childElement child = GetTemplateChild(name) as childElement;

            if (child == null)
            {
                throw new NullReferenceException(name);
            }

            return child;
        }

        private SolidColorBrush GetColorFromHex(string hexColor)
        {
            return new SolidColorBrush(
                Color.FromArgb(Convert.ToByte(hexColor.Substring(1, 2), 16),
                               Convert.ToByte(hexColor.Substring(3, 2), 16),
                               Convert.ToByte(hexColor.Substring(5, 2), 16),
                               Convert.ToByte(hexColor.Substring(7, 2), 16)));
        }
    }
}
