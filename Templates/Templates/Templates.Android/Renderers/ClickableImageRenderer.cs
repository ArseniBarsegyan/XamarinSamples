using Android.Content;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using Templates.Droid.Renderers;
using Templates.Elements;

[assembly: ExportRenderer(typeof(ClickableImage), typeof(ClickableImageRenderer))]
namespace Templates.Droid.Renderers
{
    /// <summary>
    /// Renderer for <see cref="ClickableImage" />.
    /// </summary>
    public class ClickableImageRenderer : ImageRenderer
    {
        public ClickableImageRenderer(Context context) : base(context)
        {
        }

        /// <summary>
        /// Handle click on element and call ShowFullSizeImage() method from <see cref="ClickableImage" /> class.
        /// </summary>
        protected override void OnElementChanged(ElementChangedEventArgs<Image> e)
        {
            base.OnElementChanged(e);

            var img = e.NewElement as ClickableImage;

            Control.Click += (sender, args) =>
            {
                img?.ShowFullSizeImage();
            };
        }
    }
}