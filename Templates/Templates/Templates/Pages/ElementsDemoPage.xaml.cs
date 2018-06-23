using System;
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
            var tappedImage = sender as Image;
            ViewModel.SelectedImage = tappedImage;
            await Navigation.PushPopupAsync(new FullSizeImageGallery(ViewModel.TestImages, ViewModel.SelectedImage));
        }
    }
}