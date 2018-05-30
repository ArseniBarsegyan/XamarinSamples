using MyDiary.Elements;
using MyDiary.iOS.Renderers;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(ImageGallery), typeof(ImageGalleryRenderer))]
namespace MyDiary.iOS.Renderers
{
    public class ImageGalleryRenderer : ScrollViewRenderer
    {
        protected override void OnElementChanged(VisualElementChangedEventArgs e)
        {
            base.OnElementChanged(e);

            var element = e.NewElement as ImageGallery;
            element?.Render();
        }
    }
}