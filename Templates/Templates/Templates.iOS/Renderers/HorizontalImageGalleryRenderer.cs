using Templates.Elements;
using Templates.iOS.Renderers;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(HorizontalImageGallery), typeof(HorizontalImageGalleryRenderer))]
namespace Templates.iOS.Renderers
{
    /// <summary>
    /// Renderer for <see cref="HorizontalImageGallery"/> for iOS.
    /// </summary>
    public class HorizontalImageGalleryRenderer : ScrollViewRenderer
    {
        protected override void OnElementChanged(VisualElementChangedEventArgs e)
        {
            base.OnElementChanged(e);

            var element = e.NewElement as HorizontalImageGallery;
            element?.Render();
        }
    }
}