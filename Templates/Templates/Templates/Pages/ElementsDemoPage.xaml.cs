using System;
using System.Linq;
using Rg.Plugins.Popup.Extensions;
using Templates.Elements.ImageGallery;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Templates.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ElementsDemoPage : ContentPage
    {
        public ElementsDemoPage()
        {
            InitializeComponent();
        }

        private async void HorizontalImageGallery_OnItemTapped(object sender, EventArgs e)
        {
            if (sender is Image tappedImage)
            {
                var imgSource = (UriImageSource) tappedImage.Source;

                ViewModel.SelectedImage = ViewModel.TestImages.FirstOrDefault(x => ((UriImageSource)x.Source).Uri.AbsolutePath == imgSource.Uri.AbsolutePath);
                var images = ViewModel.TestImages;
                var currentImage = ViewModel.SelectedImage;
                await Navigation.PushPopupAsync(new FullSizeImageGallery(images, currentImage));
            }
        }
    }
}