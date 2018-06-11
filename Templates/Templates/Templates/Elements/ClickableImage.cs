using System.Linq;
using Rg.Plugins.Popup.Extensions;
using Xamarin.Forms;

namespace Templates.Elements
{
    /// <inheritdoc />
    /// <summary>
    /// Image with option to click on it and open image in full size (full page image).
    /// <para>
    /// Create custom renderer in Android and iOS projects and handle click event, where call ShowFullSizeImage() method.
    /// </para>
    /// </summary>
    public class ClickableImage : Image
    {
        /// <summary>
        /// Handle click on image in custom renderer in Android or iOS projects and call this method.
        /// </summary>
        public async void ShowFullSizeImage()
        {
            //Implement action here - push FullSizeImageView() to navigation stack, use PushPopupAsync();
            var fullSizeView = new FullSizeImageView(this);
            if (!App.Current.MainPage.Navigation.NavigationStack.Contains(fullSizeView))
            {
                await App.Current.MainPage.Navigation.PushPopupAsync(fullSizeView);
            }
        }

        public static readonly BindableProperty FullSizeImageSourceProperty = BindableProperty.Create(nameof(FullSizeImageSource), typeof(string), typeof(ClickableImage));

        /// <summary>
        /// Image that will be shown in full size.
        /// </summary>
        public string FullSizeImageSource
        {
            get => (string)GetValue(FullSizeImageSourceProperty);
            set => SetValue(FullSizeImageSourceProperty, value);
        }
    }
}