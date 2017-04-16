using System;
using Windows.Foundation;
using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Data;
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

        CompositeTransform transform_myWindow;
        Button _closeButton;
        Grid _myBoundary;
        Grid _myWindow;
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
        Slider _sliderOpacity;

        protected override void OnApplyTemplate()
        {
            _sliderOpacity = GeneralizedGetTemplateChild<Slider>("sliderOpacity");
            _closeButton = GeneralizedGetTemplateChild<Button>("btnClose");
            _myBoundary = GeneralizedGetTemplateChild<Grid>("myBoundary");
            _myWindow = GeneralizedGetTemplateChild<Grid>("myWindow");
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
            
            _rectTitleBar.ManipulationMode = ManipulationModes.TranslateX | ManipulationModes.TranslateY;
            _myWindow.ManipulationMode = ManipulationModes.TranslateX | ManipulationModes.TranslateY;
            _rectRight.ManipulationMode = ManipulationModes.TranslateX;
            _rectTopLeft.ManipulationMode = ManipulationModes.TranslateX | ManipulationModes.TranslateY;
            _rectTop.ManipulationMode = ManipulationModes.TranslateY;
            _rectTopRight.ManipulationMode = ManipulationModes.TranslateX | ManipulationModes.TranslateY;
            _rectLeft.ManipulationMode = ManipulationModes.TranslateX;
            _rectBottomLeft.ManipulationMode = ManipulationModes.TranslateX | ManipulationModes.TranslateY;
            _rectBottom.ManipulationMode = ManipulationModes.TranslateY;
            _rectBottomRight.ManipulationMode = ManipulationModes.TranslateX | ManipulationModes.TranslateY;

            this.Loaded += TemplatedWindowControl_Loaded;
            _rectTitleBar.ManipulationDelta += _rectTitleBar_ManipulationDelta;
            _closeButton.Click += _closeButton_Click;
            _myWindow.PointerEntered += _myWindow_PointerEntered;
            _myWindow.PointerExited += _myWindow_PointerExited;

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

            transform_myWindow = new CompositeTransform();
            _myWindow.RenderTransform = transform_myWindow;
        }

        #region Loaded event

        private void TemplatedWindowControl_Loaded(object sender, RoutedEventArgs e)
        {
            // Check if a width and height were set by the developer when this custom control was created (we use this later)
            double widthSetCheck = this.Width;
            double heightSetCheck = this.Height;

            // Get a few of the initial properties that were set, as well
            double width = this.ActualWidth;
            double height = this.ActualHeight;
            Thickness margin = this.Margin;
            double top = margin.Top;
            double left = margin.Left;

            // Clear these property settings from this control because we want _myBoundary to Stretch, and then we will set _myWindow to match these properties.
            this.ClearValue(WidthProperty);
            this.ClearValue(HeightProperty);
            this.ClearValue(MarginProperty);

            // Here we set the width and height of _myWindow, but only if they were set by the developer, otherwise we do nothing so _myWindow will Auto size to it's Content.
            if (!Double.IsNaN(widthSetCheck))
            {
                _myWindow.Width = width;
            }
            if (!Double.IsNaN(heightSetCheck))
            {
                _myWindow.Height = height;
            }

            // Position _myWindow's Top Left to where it would be with the original Margin that was set
            transform_myWindow.TranslateX = top;
            transform_myWindow.TranslateY = left;

            // When "this" is loaded, we have to set the opacity slider to the initial opacity setting because we are going to bind to it.
            double initialOpacity = this.Opacity;
            if(initialOpacity < 0.25)
            {
                initialOpacity = 0.25;
            }
            _sliderOpacity.Value = initialOpacity * 100;

            // Here we set the binding between the opacity slider and the opacity of "this"
            Binding binding = new Binding();
            binding.Source = _sliderOpacity;
            binding.Path = new PropertyPath("Value");
            binding.Mode = BindingMode.TwoWay;
            binding.Converter = new Converters.OpacityBindingConverter();
            _contentPresenter.SetBinding(OpacityProperty, binding);
        }

        #endregion

        private void _closeButton_Click(object sender, RoutedEventArgs e)
        {
            ((Panel)this.Parent).Children.Remove(this);
        }

        private void _myWindow_PointerExited(object sender, PointerRoutedEventArgs e)
        {
            _contentPresenter.BorderBrush = new SolidColorBrush(Colors.Transparent);
            _gridTitleBar.Visibility = Visibility.Collapsed;
        }

        private void _myWindow_PointerEntered(object sender, PointerRoutedEventArgs e)
        {
            _contentPresenter.BorderBrush = GetColorFromHex("#B2007ACC");
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
            // Get the top left point of _myWindow in relationship to _myBoundary
            GeneralTransform gt = _myWindow.TransformToVisual(_myBoundary);
            Point TopLeftPoint = gt.TransformPoint(new Point(0, 0));

            // Set these variables to represent the edges of _myWindow
            double left = TopLeftPoint.X;
            double top = TopLeftPoint.Y;
            double right = left + _myWindow.ActualWidth;
            double bottom = top + _myWindow.ActualHeight;

            // Combine those edges with the movement value (When these are used in the next step, it keeps _myWindow from getting stuck at _myBoundary edges)
            double leftAdjust = left + e.Delta.Translation.X;
            double topAdjust = top + e.Delta.Translation.Y;
            double rightAdjust = right + e.Delta.Translation.X;
            double bottomAdjust = bottom + e.Delta.Translation.Y;

            // Allow _myWindow movement if within _myBoundary (Use two separate "if" statements here, so the movement isn't sticky at the boundary)
            if ((leftAdjust >= 0) && (rightAdjust <= _myBoundary.ActualWidth))
            {
                transform_myWindow.TranslateX += e.Delta.Translation.X;
            }

            if ((topAdjust >= 0) && (bottomAdjust <= _myBoundary.ActualHeight))
            {
                transform_myWindow.TranslateY += e.Delta.Translation.Y;
            }
        }

        private void _rectBottomRight_ManipulationDelta(object sender, ManipulationDeltaRoutedEventArgs e)
        {
            // Get top left point
            GeneralTransform gt = _myWindow.TransformToVisual(_myBoundary);
            Point TopLeftPoint = gt.TransformPoint(new Point(0, 0));

            // Set these to represent the edges
            double right = TopLeftPoint.X + _myWindow.ActualWidth;
            double bottom = TopLeftPoint.Y + _myWindow.ActualHeight;

            // Combine the adjustable edges with the movement value.
            double rightAdjust = right + e.Delta.Translation.X;
            double bottomAdjust = bottom + e.Delta.Translation.Y;

            // Set these to use for restricting the minimum size
            double yadjust = _myWindow.ActualHeight + e.Delta.Translation.Y;
            double xadjust = _myWindow.ActualWidth + e.Delta.Translation.X;

            // Allow adjustment within restrictions
            if ((rightAdjust <= _myBoundary.ActualWidth) && (xadjust >= 100))
            {
                _myWindow.Width = xadjust;
            }

            if ((bottomAdjust <= _myBoundary.ActualHeight) && (yadjust >= 100))
            {
                _myWindow.Height = yadjust;
            }
        }

        private void _rectBottom_ManipulationDelta(object sender, ManipulationDeltaRoutedEventArgs e)
        {
            //Get top left point
            GeneralTransform gt = _myWindow.TransformToVisual(_myBoundary);
            Point TopLeftPoint = gt.TransformPoint(new Point(0, 0));

            // Set this to represent the bottom edge
            double bottom = TopLeftPoint.Y + _myWindow.ActualHeight;

            // Combine the bottom edge with the movement value
            double bottomAdjust = bottom + e.Delta.Translation.Y;

            // Set this to use for restricting the minimum size
            double yadjust = _myWindow.ActualHeight + e.Delta.Translation.Y;

            // Allow adjustment within restrictions
            if ((bottomAdjust <= _myBoundary.ActualHeight) && (yadjust >= 100))
            {
                _myWindow.Height = yadjust;
            }
        }

        private void _rectBottomLeft_ManipulationDelta(object sender, ManipulationDeltaRoutedEventArgs e)
        {
            // Get top left point
            GeneralTransform gt = _myWindow.TransformToVisual(_myBoundary);
            Point TopLeftPoint = gt.TransformPoint(new Point(0, 0));

            // Set these to represent the edges
            double left = TopLeftPoint.X;
            double bottom = TopLeftPoint.Y + _myWindow.ActualHeight;

            // Combine the adjustable edges with the movement value
            double leftAdjust = left + e.Delta.Translation.X;
            double bottomAdjust = bottom + e.Delta.Translation.Y;

            // Set these to use for restricting the minimum size
            double yadjust = _myWindow.ActualHeight + e.Delta.Translation.Y;
            double xadjust = _myWindow.ActualWidth - e.Delta.Translation.X;

            // Allow adjustment within restrictions
            if ((leftAdjust >= 0) && (xadjust >= 100))
            {
                transform_myWindow.TranslateX += e.Delta.Translation.X;
                _myWindow.Width = xadjust;
            }

            if ((bottomAdjust <= _myBoundary.ActualHeight) && (yadjust >= 100))
            {
                _myWindow.Height = yadjust;
            }
        }

        private void _rectLeft_ManipulationDelta(object sender, ManipulationDeltaRoutedEventArgs e)
        {
            //Get top left point
            GeneralTransform gt = _myWindow.TransformToVisual(_myBoundary);
            Point TopLeftPoint = gt.TransformPoint(new Point(0, 0));

            // Set this to represent the left edge
            double left = TopLeftPoint.X;

            // Combine the left edge with the movement value
            double leftAdjust = left + e.Delta.Translation.X;

            // Set this variable to use for restricting the minimum size
            double xadjust = _myWindow.ActualWidth - e.Delta.Translation.X;

            // Allow adjustment within restrictions
            if ((leftAdjust >= 0) && (xadjust >= 100))
            {
                transform_myWindow.TranslateX += e.Delta.Translation.X;
                _myWindow.Width = _myWindow.ActualWidth - e.Delta.Translation.X;
            }
        }

        private void _rectTopRight_ManipulationDelta(object sender, ManipulationDeltaRoutedEventArgs e)
        {
            // Get top left point
            GeneralTransform gt = _myWindow.TransformToVisual(_myBoundary);
            Point TopLeftPoint = gt.TransformPoint(new Point(0, 0));

            // Set these variables to represent the edges
            double right = TopLeftPoint.X + _myWindow.ActualWidth;
            double top = TopLeftPoint.Y;

            // Combine the adjustable edges with the movement value
            double rightAdjust = right + e.Delta.Translation.X;
            double topAdjust = top + e.Delta.Translation.Y;

            // Set these to use for restricting the minimum size
            double yadjust = _myWindow.ActualHeight - e.Delta.Translation.Y;
            double xadjust = _myWindow.ActualWidth + e.Delta.Translation.X;

            // Allow adjustment within restrictions
            if ((rightAdjust <= _myBoundary.ActualWidth) && (xadjust >= 100))
            {
                _myWindow.Width = xadjust;
            }

            if ((topAdjust >= 0) && (yadjust >= 100))
            {
                transform_myWindow.TranslateY += e.Delta.Translation.Y;
                _myWindow.Height = yadjust;
            }
        }

        private void _rectTop_ManipulationDelta(object sender, ManipulationDeltaRoutedEventArgs e)
        {
            //Get top left point
            GeneralTransform gt = _myWindow.TransformToVisual(_myBoundary);
            Point TopLeftPoint = gt.TransformPoint(new Point(0, 0));

            // Set this to represent the top edge
            double top = TopLeftPoint.Y;

            // Combine the top edge with the movement value
            double topAdjust = top + e.Delta.Translation.Y;

            // Set this variable to use for restricting the minimum size
            double yadjust = _myWindow.ActualHeight - e.Delta.Translation.Y;

            // Allow adjustment within restrictions
            if ((topAdjust >= 0) && (yadjust >= 100))
            {
                transform_myWindow.TranslateY += e.Delta.Translation.Y;
                _myWindow.Height = _myWindow.ActualHeight - e.Delta.Translation.Y;
            }
        }

        private void _rectTopLeft_ManipulationDelta(object sender, ManipulationDeltaRoutedEventArgs e)
        {
            // Get top left point
            GeneralTransform gt = _myWindow.TransformToVisual(_myBoundary);
            Point TopLeftPoint = gt.TransformPoint(new Point(0, 0));

            // Set these to represent the edges
            double left = TopLeftPoint.X;
            double top = TopLeftPoint.Y;

            // Combine the adjustable edges with the movement value
            double leftAdjust = left + e.Delta.Translation.X;
            double topAdjust = top + e.Delta.Translation.Y;

            // Set these to use for restricting the minimum size
            double yadjust = _myWindow.ActualHeight - e.Delta.Translation.Y;
            double xadjust = _myWindow.ActualWidth - e.Delta.Translation.X;

            // Allow adjustment within restrictions
            if ((leftAdjust >= 0) && (xadjust >= 100))
            {
                transform_myWindow.TranslateX += e.Delta.Translation.X;
                _myWindow.Width = xadjust;
            }

            if ((topAdjust >= 0) && (yadjust >= 100))
            {
                transform_myWindow.TranslateY += e.Delta.Translation.Y;
                _myWindow.Height = yadjust;
            }
        }

        private void _rectRight_ManipulationDelta(object sender, ManipulationDeltaRoutedEventArgs e)
        {
            //Get top left point
            GeneralTransform gt = _myWindow.TransformToVisual(_myBoundary);
            Point TopLeftPoint = gt.TransformPoint(new Point(0, 0));

            // Set this variable to represent the right edge of myCustomWindow
            double right = TopLeftPoint.X + _myWindow.ActualWidth;

            // Combine the right edge with the movement value
            double rightAdjust = right + e.Delta.Translation.X;

            // Set this variable to use for restricting the minimum size
            double xadjust = _myWindow.ActualWidth + e.Delta.Translation.X;

            // Allow adjustment within restrictions
            if ((rightAdjust <= _myBoundary.ActualWidth) && (xadjust >= 100))
            {
                _myWindow.Width = xadjust;
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
