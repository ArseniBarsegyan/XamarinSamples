using Android.Content;
using MyDiary.Droid.Renderers;
using MyDiary.Elements;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(ClickableImage), typeof(ClickableImageRenderer))]
namespace MyDiary.Droid.Renderers
{
    public class ClickableImageRenderer : ImageRenderer
    {
        public ClickableImageRenderer(Context context) : base(context)
        {
        }

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