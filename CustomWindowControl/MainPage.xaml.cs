using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Storage;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Markup;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace CustomWindowControl
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();
        }

        private void btnAddWindow_Click(object sender, RoutedEventArgs e)
        {
            Image image = new Image();
            image.Source = new BitmapImage(new Uri(BaseUri, "/Assets/Apicture.png"));
            image.Stretch = Stretch.None;

            TemplatedWindowControl window = new TemplatedWindowControl();
            window.Content = image;

            gridForWindows.Children.Add(window);
        }

        private void btnAddWindowB_Click(object sender, RoutedEventArgs e)
        {
            Image image = new Image();
            image.Source = new BitmapImage(new Uri(BaseUri, "/Assets/Bpicture.jpg"));
            image.Stretch = Stretch.None;

            TemplatedWindowControl window = new TemplatedWindowControl();
            window.Content = image;

            gridForWindows.Children.Add(window);
        }

        private async void btnAddWindowC_Click(object sender, RoutedEventArgs e)
        {
            // Get your file
            StorageFile file = await StorageFile.GetFileFromApplicationUriAsync(new Uri(BaseUri, "/MyXamlTextFiles/SixGridXamlText.txt"));

            // Read your file and set it to a string variable
            string myXamlString = await FileIO.ReadTextAsync(file);

            // Create the object from that string
            object myAdditionalContent = XamlReader.Load(myXamlString);

            TemplatedWindowControl window = new TemplatedWindowControl();
            window.Content = myAdditionalContent;

            gridForWindows.Children.Add(window);
        }
    }
}
