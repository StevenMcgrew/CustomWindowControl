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
        Grid _gridRoot;
        Grid _gridForContent;

        protected override void OnApplyTemplate()
        {
            _closeButton = GeneralizedGetTemplateChild<Button>("btnClose");

            _closeButton.Click += click;
            
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


        private void click(object sender, RoutedEventArgs e)
        {
            ((Panel)this.Parent).Children.Remove(this);
        }

    }
}
