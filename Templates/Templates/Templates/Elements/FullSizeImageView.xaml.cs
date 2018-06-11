using System;
using Rg.Plugins.Popup.Extensions;
using Rg.Plugins.Popup.Pages;
using Xamarin.Forms.Xaml;

namespace Templates.Elements
{
    /// <summary>
    /// This view show image in full size on a screen with backgroud with opacity.
    /// </summary>
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class FullSizeImageView : PopupPage
    {
        /// <summary>
        /// Create new instance of <see cref="FullSizeImageView"/>.
        /// </summary>
        public FullSizeImageView(ClickableImage clickableImage)
        {
            InitializeComponent();
            BindingContext = clickableImage;
        }

        /// <summary>
        /// Closing current popup page if user tap outside of image.
        /// </summary>
        private async void Background_OnClick(object sender, EventArgs e)
        {
            await Navigation.PopPopupAsync();
        }

        /// <summary>
        /// Calling when user tap on full size image.
        /// </summary>
        private void Image_OnTapped(object sender, EventArgs e)
        {
        }
    }
}