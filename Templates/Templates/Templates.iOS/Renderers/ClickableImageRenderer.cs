using Templates.Elements;
using Templates.iOS.Renderers;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(ClickableImage), typeof(ClickableImageRenderer))]
namespace Templates.iOS.Renderers
{
    /// <summary>
    /// Renderer for <see cref="ClickableImage" />.
    /// </summary>
    public class ClickableImageRenderer : ImageRenderer
    {
        private UIImageView _nativeElement;
        private ClickableImage _formsElement;

        /// <summary>
        /// Handle tap on element and call ShowFullSizeImage() method from <see cref="ClickableImage" /> class.
        /// </summary>
        protected override void OnElementChanged(ElementChangedEventArgs<Image> e)
        {
            base.OnElementChanged(e);
            if (e.NewElement != null)
            {
                _formsElement = e.NewElement as ClickableImage;
                _nativeElement = Control as UIImageView;
                _nativeElement.UserInteractionEnabled = true;
                var clickGestureRecognizer = new UITapGestureRecognizer(ShowFullSizeImage);
                _nativeElement.AddGestureRecognizer(clickGestureRecognizer);
            }
        }

        private void ShowFullSizeImage()
        {
            _formsElement.ShowFullSizeImage();
        }
    }
}