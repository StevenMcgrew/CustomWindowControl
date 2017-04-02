using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Windows.Input;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Documents;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;

// The Templated Control item template is documented at https://go.microsoft.com/fwlink/?LinkId=234235

namespace CustomWindowControl
{
    public sealed class TemplatedWindowControl : ContentControl
    {
        public TemplatedWindowControl()
        {
            this.DefaultStyleKey = typeof(TemplatedWindowControl);
        }

        Button _closeButton;

        protected override void OnApplyTemplate()
        {
            _closeButton = GetTemplateChild("btnClose") as Button;
            if (_closeButton == null)
            {
                throw new NullReferenceException();
            }

            _closeButton.Click += click;

            //_closeButton.Click += _closeButton_Click;
        }

        //private void _closeButton_Click(object sender, RoutedEventArgs e)
        //{
        //    ((Panel)this.Parent).Children.Remove(this);
        //}

        //public ICommand CommandClose
        //{
        //    get { return (ICommand)GetValue(CommandCloseProperty); }
        //    set { SetValue(CommandCloseProperty, value); }
        //}

        //public static readonly DependencyProperty CommandCloseProperty =
        //    DependencyProperty.Register(nameof(CommandClose), typeof(ICommand),
        //        typeof(TemplatedWindowControl), new PropertyMetadata(null));

        private void click(object sender, RoutedEventArgs e)
        {
            ((Panel)this.Parent).Children.Remove(this);
        }

    }
}
