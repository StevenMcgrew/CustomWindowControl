using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Windows.Input;
using Windows.UI;
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
        ContentPresenter _contentPresenter;

        protected override void OnApplyTemplate()
        {
            _closeButton = GeneralizedGetTemplateChild<Button>("btnClose");
            _gridRoot = GeneralizedGetTemplateChild<Grid>("gridRoot");
            _contentPresenter = GeneralizedGetTemplateChild<ContentPresenter>("ContentPresenter");

            _closeButton.Click += _closeButton_Click;
            _gridRoot.PointerEntered += _gridRoot_PointerEntered;
            _gridRoot.PointerExited += _gridRoot_PointerExited;
            
        }

        private void _gridRoot_PointerExited(object sender, PointerRoutedEventArgs e)
        {
            _contentPresenter.BorderBrush = new SolidColorBrush(Colors.Transparent);
        }

        private void _gridRoot_PointerEntered(object sender, PointerRoutedEventArgs e)
        {
            _contentPresenter.BorderBrush = new SolidColorBrush(Colors.DodgerBlue);
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

    }
}
