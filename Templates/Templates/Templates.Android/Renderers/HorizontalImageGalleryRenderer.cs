using Android.Content;
using Templates.Droid.Renderers;
using Templates.Elements;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(HorizontalImageGallery), typeof(HorizontalImageGalleryRenderer))]
namespace Templates.Droid.Renderers
{
    /// <summary>
    /// Renderer for <see cref="HorizontalImageGallery"/> for Android.
    /// </summary>
    public class HorizontalImageGalleryRenderer : ScrollViewRenderer
    {
        public HorizontalImageGalleryRenderer(Context context) : base(context)
        {
        }

        protected override void OnElementChanged(VisualElementChangedEventArgs e)
        {
            base.OnElementChanged(e);

            var element = e.NewElement as HorizontalImageGallery;
            element?.Render();
        }
    }
}