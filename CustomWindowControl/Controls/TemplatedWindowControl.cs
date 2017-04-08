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

// The Templated Control item template is documented at https://go.microsoft.com/fwlink/?LinkId=234235

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
        ContentPresenter _contentPresenter;
        Rectangle _rectTitleBar;
        Rectangle _rectRight;


        protected override void OnApplyTemplate()
        {
            _closeButton = GeneralizedGetTemplateChild<Button>("btnClose");
            _gridRoot = GeneralizedGetTemplateChild<Grid>("gridRoot");
            _contentPresenter = GeneralizedGetTemplateChild<ContentPresenter>("ContentPresenter");
            _rectTitleBar = GeneralizedGetTemplateChild<Rectangle>("rectTitleBar");
            _rectRight = GeneralizedGetTemplateChild<Rectangle>("Right");

            this.ManipulationMode = ManipulationModes.TranslateX | ManipulationModes.TranslateY;
            _rectTitleBar.ManipulationMode = ManipulationModes.TranslateX | ManipulationModes.TranslateY;
            _gridRoot.ManipulationMode = ManipulationModes.TranslateX | ManipulationModes.TranslateY;
            _rectRight.ManipulationMode = ManipulationModes.TranslateX;

            _rectTitleBar.ManipulationDelta += _rectTitleBar_ManipulationDelta;
            _closeButton.Click += _closeButton_Click;
            _gridRoot.PointerEntered += _gridRoot_PointerEntered;
            _gridRoot.PointerExited += _gridRoot_PointerExited;
            _rectRight.ManipulationDelta += _rectRight_ManipulationDelta;
            _rectRight.PointerEntered += _rectRight_PointerEntered;
            _rectRight.PointerExited += _rectRight_PointerExited;

            transformWindow = new CompositeTransform();
            this.RenderTransform = transformWindow;
        }

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
            if ((rightAdjust <= panel.ActualWidth) && (xadjust >= 100))
            {
                myCustomWindow.Width = xadjust;
            }
        }

        private void _rectRight_PointerExited(object sender, PointerRoutedEventArgs e)
        {
            Window.Current.CoreWindow.PointerCursor = new Windows.UI.Core.CoreCursor(Windows.UI.Core.CoreCursorType.Arrow, 1);
        }

        private void _rectRight_PointerEntered(object sender, PointerRoutedEventArgs e)
        {
            Window.Current.CoreWindow.PointerCursor = new Windows.UI.Core.CoreCursor(Windows.UI.Core.CoreCursorType.SizeWestEast, 1);
        }

        private void _gridRoot_PointerExited(object sender, PointerRoutedEventArgs e)
        {
            _contentPresenter.BorderBrush = new SolidColorBrush(Colors.Transparent);
            _closeButton.Visibility = Visibility.Collapsed;
            _rectTitleBar.Visibility = Visibility.Collapsed;
        }

        private void _gridRoot_PointerEntered(object sender, PointerRoutedEventArgs e)
        {
            _contentPresenter.BorderBrush = GetColorFromHex("#B2007ACC");
            _closeButton.Visibility = Visibility.Visible;
            _rectTitleBar.Visibility = Visibility.Visible;
        }

        childElement GeneralizedGetTemplateChild<childElement>(string name) where childElement : DependencyObject
        {
            childElement child = GetTemplateChild(name) as childElement;

            if (child == null)
            {
                throw new NullReferenceException(name);
            }

            return child;
        }

        private void _closeButton_Click(object sender, RoutedEventArgs e)
        {
            ((Panel)this.Parent).Children.Remove(this);
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
