using System;
using Windows.Storage;
using Windows.Storage.FileProperties;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Markup;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;

namespace CustomWindowControl
{
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();
        }

        private async void btnAddWindow_Click(object sender, RoutedEventArgs e)
        {
            // Create the image
            Image image = new Image();
            image.Source = new BitmapImage(new Uri(BaseUri, "/Assets/Apicture.png"));
            image.Stretch = Stretch.Fill;

            // Get the image properties
            StorageFile file = await StorageFile.GetFileFromApplicationUriAsync(new Uri(BaseUri, "/Assets/Apicture.png"));
            ImageProperties imageProperties = await file.Properties.GetImagePropertiesAsync();

            // Create the window and set the image as it's content
            TemplatedWindowControl window = new TemplatedWindowControl();
            window.Width = imageProperties.Width;
            window.Height = imageProperties.Height;
            window.Content = image;

            gridForWindows.Children.Add(window);
        }

        private async void btnAddWindowB_Click(object sender, RoutedEventArgs e)
        {
            // Create the image
            Image image = new Image();
            image.Source = new BitmapImage(new Uri(BaseUri, "/Assets/Bpicture.jpg"));
            image.Stretch = Stretch.Fill;

            // Get the image properties
            StorageFile file = await StorageFile.GetFileFromApplicationUriAsync(new Uri(BaseUri, "/Assets/Bpicture.jpg"));
            ImageProperties imageProperties = await file.Properties.GetImagePropertiesAsync();

            // Create the window and set the image as it's content
            TemplatedWindowControl window = new TemplatedWindowControl();
            window.Width = imageProperties.Width;
            window.Height = imageProperties.Height;
            window.Margin = new Thickness(50);
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

            // Create the window and set the xaml file object as the content
            TemplatedWindowControl window = new TemplatedWindowControl();
            window.Width = gridForWindows.ActualWidth / 2;
            window.Height = gridForWindows.ActualHeight / 2;
            window.Content = myAdditionalContent;

            gridForWindows.Children.Add(window);
        }
    }
}
