using Android.Content;
using MyDiary.Droid.Renderers;
using MyDiary.Elements;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(ImageGallery), typeof(ImageGalleryRenderer))]
namespace MyDiary.Droid.Renderers
{
    public class ImageGalleryRenderer : ScrollViewRenderer
    {
        public ImageGalleryRenderer(Context context) : base(context)
        {
        }

        protected override void OnElementChanged(VisualElementChangedEventArgs e)
        {
            base.OnElementChanged(e);

            var element = e.NewElement as ImageGallery;
            element?.Render();
        }
    }
}